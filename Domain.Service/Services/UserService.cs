using AutoMapper;
using Domain.Model.Common.Data;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Collections;
using Moneyon.Common.Data;
using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Common.Extensions;
using Moneyon.PowerBi.Common.ObjectMapper;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Modeling;
using System.Threading;


namespace Moneyon.PowerBi.Domain.Service.Services;

[AutoRegister()]
public class UserService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public UserService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }



    public async Task<DataResult<ShortPersonDto>> GetPersons(DataRequest request)
    {
        var persons = await _uw.PersonRepository.ReadPagableAsync(request, include: i => i.Include(u => u.User!));
        return _mp.MapDataResult<Person, ShortPersonDto>(persons);
    }

    public async Task<PersonDto> GetPerson(Guid id)
    {
        var person = await _uw.PersonRepository.FirstOrDefaultAsync(filter: p => p.Code == id,
                                                                  include: i => i.Include(u => u.User!)
                                                                              .ThenInclude(r => r.Roles!)
                                                                              .Include(add => add.Addresses!)
                                                                              .ThenInclude(city => city.City!)
                                                                              .ThenInclude(prov => prov.Province!)
                                                                              .ThenInclude(country => country.Country!)
                                                                  );
        if (person is null) throw new BizException(BizExceptionCode.DataNotFound);
        return _mp.Map<PersonDto>(person);
    }

    public async Task EditPersonRole(EditPersonRoleDto dto)
    {
        var person = await _uw.PersonRepository.FirstOrDefaultAsync(filter: p => p.Code == dto.PersonId,
                                                                    include: i => i.Include(u => u.User!)
                                                                                   .ThenInclude(r => r.Roles!));

        if (person is null) throw new BizException(BizExceptionCode.DataNotFound);

        await DeleteRoles(person!.User, dto.Roles);

        await _uw.CommitAsync();
    }

    public async Task AdminUserPassReset(AdminResetPasswordDto dto)
    {
        var user = await _uw.UserRepository.FirstOrDefaultAsync(p => p.Person.Code == dto.PersonId);
        if (user is null) throw new BizException(BizExceptionCode.UserNotFound);

        user.Password = dto.Password.Hash();
        await _uw.CommitAsync();
    }

    public async Task<PersonDto> GetUserProfile(long? id)
    {
        if (id is null) throw new BizException(BizExceptionCode.UserNotFound);

        var person = await _uw.PersonRepository.FirstOrDefaultAsync(filter: p => p.Id == id,
                                                                  include: i => i.Include(p => p.User!)
                                                                               .ThenInclude(p => p.Roles!)
                                                                               .Include(p => p.Addresses!)
                                                                               .ThenInclude(p => p.City!)
                                                                               .ThenInclude(p => p.Province!)
                                                                               .ThenInclude(p => p.Country!)
                                                                  );
        if (person is null) throw new BizException(BizExceptionCode.UserNotFound);

        return _mp.Map<PersonDto>(person);
    }

    public async Task EditProfile(EditProfileDto dto, long? userId)
    {
        if (userId is null) throw new BizException(BizExceptionCode.UserNotFound);
        var person = await _uw.PersonRepository.FirstOrDefaultAsync(filter: p => p.User.PersonId == userId,
                                                                    include: i => i.Include(p => p.Addresses!)
                                                                    );
        _mp.Map(dto, person);
        var address = _mp.Map<Address>(dto.Addresses);

        if (person!.Addresses is null)
            person.Addresses = new List<Address>();

        person.Addresses.Add(address);

        await _uw.CommitAsync();
    }

    public async Task UserPassReset(ResetPasswordDto dto, long? id)
    {
        if (id is null) throw new BizException(BizExceptionCode.UserNotFound);

        var user = await _uw.UserRepository.FirstOrDefaultAsync(filter: p => p.Id == id && p.Password == dto.OldPassword.Hash());

        if (user is null) throw new BizException(BizExceptionCode.PasswordInvalid);

        user.Password = dto.Password.Hash();
        await _uw.CommitAsync();
    }

    private async Task DeleteRoles(User user)
    {

        user.Roles!.ForEach(role =>
        {
            user.Roles!.Remove(role);
        });
    }

    private async Task DeleteRoles(User? user, IEnumerable<long>? roles)
    {
        if (roles is null || roles.Count() == 0)
            return;

        List<Role> roleList = new List<Role>();
        foreach (var id in roles!)
        {
            var roleItem = await _uw.RoleRepository.FirstOrDefaultAsync(p => p.Id == id);
            if (roleItem is not null)
                roleList.Add(roleItem);
        }

        var deleteRoleList = user!.Roles!.Where(p => !roleList.Any(r => r.Id == p.Id));
        var insertRoleList = roleList.Where(p => !user!.Roles!.Any(r => r.Id == p.Id));

        deleteRoleList!.ForEach(role =>
        {
            user!.Roles!.Remove(role);
        });

        insertRoleList!.ForEach(role =>
        {
            user!.Roles!.Add(role);
        });

    }
}


[AutoRegister]
public class TicketDetailService
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork uw;

    public TicketDetailService(IMapper mapper,
                               IUnitOfWork unitOfWork)
    {
        this.mapper = mapper;
        this.uw = unitOfWork;

    }

    public async Task CreateTicketDetailsAsync(TicketDetailCreateDto ticketDetail, long personId, CancellationToken cancellationToken = default)
    {

        var ticket = await uw.TicketRepository.SingleOrDefaultAsync(p => p.Id == ticketDetail.TicketId, cancellationToken);
        if (ticket is null)
        {
            throw new BizException(BizExceptionCode.DataNotFound);
        }

        if (ticket.CreatedById != personId)
        {
            throw new BizException(BizExceptionCode.User_NotPermission);
        }

        if (ticket.Status == TicketStatus.Closed)
        {
            throw new BizException(BizExceptionCode.TicketHasCloseStatus);
        }

        ticket.LastUserModifiedId = personId;
        ticket.ModifiedOn = DateTime.UtcNow;
        ticket.Status = TicketStatus.AwaitingForReview;

        var entity = mapper.Map<TicketDetail>(ticketDetail);
        entity.PersonId = personId;

        if (ticket.CreatedById != personId)
        {
            entity.IsOwner = false;
        }

        if (ticketDetail.Document is not null)
        {

            var doc = await ticketDetail.Document.ToDocumentAsync();
            doc.TicketDetailId = entity.Id;

            if (entity.Documents == null || entity.Documents.Count() == 0)
                entity.Documents = new List<Document>();

            if (entity.Documents is null || entity.Documents.Count() == 0)
                entity.Documents = new List<Document>();

            entity.Documents.Append(doc);

        }


        await uw.TicketDetailRepository.InsertAsync(entity, cancellationToken);
        await uw.CommitAsync(cancellationToken);

    }
    public async Task CreateAdminTicketDetailsAsync(TicketDetailCreateDto ticketDetail, long userId, CancellationToken cancellationToken = default)
    {

        //var ticket = await uw.TicketRepository.SingleOrDefaultAsync(p => p.Id == ticketDetail.TicketId, cancellationToken);
        //if (ticket is null)
        //{
        //    throw new BizException(BizExceptionCode.DataNotFound);
        //}

        //if (ticket.Status == TicketStatus.Closed)
        //{
        //    throw new BizException(BizExceptionCode.TicketHasCloseStatus);
        //}

        //ticket.LastUserModifiedId = userId;
        //ticket.ModifiedOn = DateTime.UtcNow;
        //ticket.Status = TicketStatus.Answered;

        //var entity = mapper.Map<TicketDetail>(ticketDetail);
        //entity.PersonId = userId;

        //if (ticket.CreatedById != userId)
        //{
        //    entity.IsOwner = false;
        //}

        ////if (!(ticketDetail.Documents is null) && ticketDetail.Documents.Count() > 0)
        ////{
        ////    foreach (var item in ticketDetail.Documents)
        ////    {
        ////        var doc = await item.ToDocumentAsync();
        ////        doc.TicketDetailId = entity.Id;

        ////        if (entity.Documents == null || entity.Documents.Count() == 0)
        ////            entity.Documents = new List<Document>();

        ////        entity.Documents.Append(doc);
        ////    }
        ////}

        //if (ticketDetail.Documents is not null)
        //{
        //    var doc = await ticketDetail.Documents.ToDocumentAsync();
        //    doc.TicketDetailId = entity.Id;

        //    if (entity.Documents is null || entity.Documents.Count() == 0)
        //        entity.Documents = new List<Document>();

        //    entity.Documents.Append(doc);
        //}

        //await uw.TicketDetail.InsertAsync(entity, cancellationToken);
        //await uw.CommitAsync(cancellationToken);

    }
}

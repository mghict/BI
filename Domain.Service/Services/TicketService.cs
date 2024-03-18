using AutoMapper;
using Domain.Model.Common.Data;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;
using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Common.ObjectMapper;
using Moneyon.PowerBi.Domain.Model.Modeling;


namespace Moneyon.PowerBi.Domain.Service.Services;

[AutoRegister]
public class TicketService
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly DocumentService documentService;

    public TicketService(IMapper mapper, IUnitOfWork unitOfWork, DocumentService documentService)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.documentService = documentService;
    }

    public async Task<long> CreateTicketAsync(CreateTicketDto dto, long personId, CancellationToken cancellationToken = default)
    {

        var entity = mapper.Map<Ticket>(dto);

        entity.LastUserModifiedId = entity.CreatedById = personId;
        entity.ModifiedOn = entity.CreateOn;

        entity.TicketDetails = new List<TicketDetail>()
            {
                new TicketDetail()
                {
                    CreateOn= entity.CreateOn,
                    Description=dto.Description,
                    PersonId=personId,
                    TicketId=entity.Id
                }
            };

        await unitOfWork.TicketRepository.InsertAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return entity.TicketDetails.First().Id;
    }
    public async Task<DocumentDto> CreateTicketDocumentAsync(DocumentTicketCreateDto dto, long personId)
    {
        return await documentService.UpsertDocumentAsync(dto, personId);
    }
    public async Task CloseTicketAsync(TicketChangeDto dto, long userId, CancellationToken cancellationToken = default)
    {
        var ticket = await unitOfWork.TicketRepository.FirstOrDefaultAsync(p => p.Id == dto.Id, cancellationToken);

        if (ticket is null)
        {
            throw new BizException(BizExceptionCode.TicketNotFound);
        }

        if (ticket.Status == TicketStatus.Closed)
        {
            throw new BizException(BizExceptionCode.TicketHasCloseStatus);
        }

        if (ticket.CreatedById != userId)
        {
            throw new BizException(BizExceptionCode.User_NotPermission);
        }

        ticket.Status = TicketStatus.Closed;
        await unitOfWork.CommitAsync(cancellationToken);
    }
    public async Task DeleteTicket(TicketChangeDto dto, long userId, CancellationToken cancellationToken = default)
    {

        var entity = await unitOfWork.TicketRepository.SingleOrDefaultAsync(filter: p => p.TrackingCode == dto.Id,
                                                                   include: p => p.Include(i => i.TicketDetails!).ThenInclude(i => i.Documents).ThenInclude(i => i.Content)
                                                                 , cancellationToken);

        if (entity is null)
        {
            throw new BizException(BizExceptionCode.DataNotFound);
        }

        if (entity.CreatedById != userId)
        {
            throw new BizException(BizExceptionCode.User_NotPermission);
        }

        if (entity?.Status != TicketStatus.AwaitingForReview)
        {
            throw new BizException(BizExceptionCode.General_DeleteNotComplete);
        }

        foreach (var item in entity.TicketDetails!.ToList())
        {
            foreach (var doc in item.Documents)
            {
                await unitOfWork.DocumentContentRepository.DeleteAsync(doc.Content.Id);
                await unitOfWork.DocumentRepository.DeleteAsync(doc.Id);
            }

            await unitOfWork.TicketDetailRepository.DeleteAsync(item, cancellationToken);

        }


        await unitOfWork.TicketRepository.DeleteAsync(entity.Id, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

    }
    public async Task<TicketDto> GetTicketAsync(long id, CancellationToken cancellationToken = default)
    {

        var entity = await unitOfWork.TicketRepository.SingleOrDefaultAsync(filter: p => p.Id == id,
                                                                include: p => p.Include(i => i.TicketCategory)
                                                                                .Include(i => i.TicketDetails!)
                                                                                .ThenInclude(i => i.Person)
                                                                                .Include(i => i.TicketDetails!)
                                                                                .ThenInclude(i => i.Documents)
                                                                                .ThenInclude(i => i.Content),
                                                                orderBy: p => p.OrderByDescending(ord => ord.CreateOn),
                                                                cancellationToken);

        if (entity is null)
        {
            throw new BizException(BizExceptionCode.DataNotFound);
        }

        var result = mapper.Map<Ticket, TicketDto>(entity);
        result.TicketDetails = mapper.MapCollection<TicketDetail, TicketDetailDto>(entity.TicketDetails).ToList();

        return result;

    }
    public async Task<TicketDto> GetTicketAsync(long id, long userId, CancellationToken cancellationToken = default)
    {

        var entity = await unitOfWork.TicketRepository.SingleOrDefaultAsync(filter: p => p.Id == id,
                                                                include: p => p.Include(i => i.TicketCategory)
                                                                                .Include(i => i.TicketDetails!)
                                                                                .ThenInclude(i => i.Person)
                                                                                .Include(i => i.TicketDetails!)
                                                                                .ThenInclude(i => i.Documents)
                                                                                .ThenInclude(i => i.Content),
                                                                orderBy: p => p.OrderByDescending(ord => ord.CreateOn),
                                                                cancellationToken);

        if (entity is null)
        {
            throw new BizException(BizExceptionCode.DataNotFound);
        }

        if (entity.CreatedById != userId)
        {
            throw new Exception(BizExceptionCode.User_NotPermission);
        }



        var result = mapper.Map<Ticket, TicketDto>(entity);
        result.TicketDetails = mapper.MapCollection<TicketDetail, TicketDetailDto>(entity.TicketDetails).ToList();

        return result;

    }
    public async Task<IEnumerable<TicketShortDto>> GetUserTicketsAsync(long userId, CancellationToken cancellationToken = default)
    {

        var entity = await unitOfWork.TicketRepository.ReadAsync(filter: p => p.CreatedById == userId,
                                                                include: p => p.Include(i => i.TicketCategory),
                                                                orderBy: p => p.OrderByDescending(ord => ord.CreateOn), cancellationToken);


        return mapper.MapCollection<Ticket, TicketShortDto>(entity!);

    }
    public async Task<IEnumerable<TicketShortDto>> GetAdminTicketsAsync(TicketStatus ticketStatus, CancellationToken cancellationToken = default)
    {

        var entity = ticketStatus == TicketStatus.All ?
                (await unitOfWork.TicketRepository.ReadAsync(include: p => p.Include(i => i.TicketCategory),
                                                   orderBy: p => p.OrderByDescending(ord => ord.CreateOn), cancellationToken)) :
                (await unitOfWork.TicketRepository.ReadAsync(filter: p => p.Status == ticketStatus,
                                                   include: p => p.Include(i => i.TicketCategory),
                                                   orderBy: p => p.OrderByDescending(ord => ord.CreateOn), cancellationToken));

        return mapper.MapCollection<Ticket, TicketShortDto>(entity);

    }
    public async Task<IEnumerable<TicketShortDto>> GetAdminTicketsAsync(TicketStatus ticketStatus, int count, CancellationToken cancellationToken = default)
    {


        var entity = ticketStatus == TicketStatus.All ?
                (await unitOfWork.TicketRepository.ReadAsync(include: p => p.Include(i => i.TicketCategory),
                                                   orderBy: p => p.OrderByDescending(ord => ord.CreateOn),
                                                   recordCount: count,
                                                   cancellationToken)) :
                (await unitOfWork.TicketRepository.ReadAsync(filter: p => p.Status == ticketStatus,
                                                   include: p => p.Include(i => i.TicketCategory),
                                                   orderBy: p => p.OrderByDescending(ord => ord.CreateOn),
                                                   recordCount: count,
                                                   cancellationToken));

        return mapper.MapCollection<Ticket, TicketShortDto>(entity);

    }
    public async Task<DataResult<TicketShortDto>> GetAdminTicketsAsync(DataRequest request, TicketStatus ticketStatus, CancellationToken cancellationToken = default)
    {

        var entity = ticketStatus == TicketStatus.All ?
                (await unitOfWork.TicketRepository.ReadPagableAsync(request, include: p => p.Include(i => i.TicketCategory),
                                                   orderBy: p => p.OrderByDescending(ord => ord.CreateOn),
                                                   cancellationToken)) :
                (await unitOfWork.TicketRepository.ReadPagableAsync(request, filter: p => p.Status == ticketStatus,
                                                   include: p => p.Include(i => i.TicketCategory),
                                                   orderBy: p => p.OrderByDescending(ord => ord.CreateOn), cancellationToken));

        return mapper.MapDataResult<Ticket, TicketShortDto>(entity);

    }


}
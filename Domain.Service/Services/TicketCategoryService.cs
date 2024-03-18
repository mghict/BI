using AutoMapper;
using Domain.Model.Common.Data;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Common.ObjectMapper;
using Moneyon.PowerBi.Domain.Model.Modeling;


namespace Moneyon.PowerBi.Domain.Service.Services;

[AutoRegister]
public class TicketCategoryService
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public TicketCategoryService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async Task CreateCategoryAsync(CreateTicketCategoryDto dto, CancellationToken cancellationToken = default)
    {

        var entity = mapper.Map<TicketCategory>(dto);
        await unitOfWork.TicketCategoryRepository.InsertAsync(entity, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }

    public async Task UpsertCategoryAsync(UpdateTicketCategoryDto dto, CancellationToken cancellationToken = default)
    {

        var entity = await unitOfWork.TicketCategoryRepository.SingleOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);
        bool isExists = entity is null;

        mapper.Map(dto, entity);

        if (isExists)
        {
            await unitOfWork.TicketCategoryRepository.InsertAsync(entity, cancellationToken);
        }

        await unitOfWork.CommitAsync(cancellationToken);

    }

    public async Task<TicketCategoryDto> GetTicketCategoryAsync(int id, CancellationToken cancellationToken = default)
    {

        var entity = await unitOfWork.TicketCategoryRepository.FindAsync(id, cancellationToken);
        return mapper.Map<TicketCategoryDto>(entity);

    }

    public async Task<IEnumerable<TicketCategoryDto>> ReadAllTicketCategoriesAsync(CancellationToken cancellationToken = default)
    {

        var entities = await unitOfWork.TicketCategoryRepository.ReadAsync(cancellationToken);
        return mapper.MapCollection<TicketCategory, TicketCategoryDto>(entities);

    }
}
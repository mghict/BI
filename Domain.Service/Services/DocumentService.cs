using AutoMapper;
using Domain.Model.Common.Data;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.IOC;
using Moneyon.PowerBi.Domain.Model.Modeling;


namespace Moneyon.PowerBi.Domain.Service.Services;

[AutoRegister]
public class DocumentService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper mapper;

    public DocumentService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<DocumentWithContentDto> ReadDocumentAsync(Guid documentGuid, CancellationToken cancellationToken = default)
    {
        var doc = await _unitOfWork.DocumentRepository.SingleAsync(filter: x => x.Guid == documentGuid, include: s => s.Include(p => p.Content), cancellationToken);
        return mapper.Map<DocumentWithContentDto>(doc);
    }

    public async Task<DocumentDto> UpsertDocumentAsync(DocumentCreateDto dto,long personId, CancellationToken cancellationToken = default)
    {
        var docToReplaceOrInsert = await dto.ToDocumentAsync();
        docToReplaceOrInsert.PersonId=personId;

        await _unitOfWork.DocumentRepository.InsertAsync(docToReplaceOrInsert);
        await _unitOfWork.CommitAsync();
        return mapper.Map<DocumentDto>(docToReplaceOrInsert);
    }
}
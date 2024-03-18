using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moneyon.PowerBi.API.Controllers.Base;
using Moneyon.PowerBi.Domain.Service.Services;

namespace Moneyon.PowerBi.API.Controllers;

[ApiController]
[Route("api/documents")]
public class DocumentsController : AppBaseController
{
    private readonly DocumentService documentService;


    public DocumentsController(
        IHttpContextAccessor contextAccessor,
        DocumentService documentService
        )
        :base(contextAccessor)
    {
        this.documentService = documentService;
    }

    [HttpGet]
    [Route("{documentId}", Name = "userDocumentDownload")]
    //[JWTAuthorization()]
    public async Task<FileContentResult> DownloadDocument(string documentId, CancellationToken cancellationToken = default)
    {
        var guid=Guid.Parse(documentId);
        var result = await documentService.ReadDocumentAsync(guid, cancellationToken);
        return File(result.Content, result.ContentType, result.OriginalFileName, false);
    }

    //[HttpGet]
    //[Route("{documentGuid}/thumbnail")]
    ////[JWTAuthorization()]
    //public async Task<FileContentResult> DownloadDocumentThumbnail(Guid documentGuid, CancellationToken cancellationToken = default)
    //{
    //    var result = await documentService.ReadDocumentAsync(documentGuid, cancellationToken);
    //    return File(result.Content, result.ContentType, result.OriginalFileName, false);
    //}


}
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;
using Moneyon.Common.ExceptionHandling;
using Moneyon.PowerBi.API.Common.Extensions;
using Moneyon.PowerBi.API.Controllers.Base;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Modeling;
using Moneyon.PowerBi.Domain.Service.Services;

namespace Moneyon.PowerBi.API.Controllers
{
    [Route("api/purches")]
    [ApiController]
    public class PurchesController : AppBaseController
    {
        private readonly PurchesService _purchesService;
        private readonly DocumentService _documentService;
        public PurchesController(IHttpContextAccessor contextAccessor, PurchesService purchesService, DocumentService documentService) : base(contextAccessor)
        {
            _purchesService = purchesService;
            _documentService = documentService;
        }

        [HttpPost()]
        [Route("ipg-payment")]
        [JWTAuthorization()]
        public async Task CreateIPGPurches(PurchesIpgCreateDto dto)
        {

            CheckUser();
            await _purchesService.CreateIpgPurches(User!.Person,dto);
        }


        [HttpPost()]
        [Route("receipt-payment")]
        [JWTAuthorization()]
        public async Task CreateReceiptPurches(PurchesReceiptCreateDto dto)
        {

            CheckUser();
            await _purchesService.CreateReceiptPurches(User!.Person, dto);
        }

        [HttpPost()]
        [Route("receipt-image")]
        [JWTAuthorization()]
        public async Task<DocumentDto> CreateReceiptImagePurches([FromForm] DocumentCreateDto dto)
        {

            CheckUser();
            var doc = await _documentService.UpsertDocumentAsync(dto,User!.PersonId);
            doc.SetUserScopeUrls(Url);
            return doc;
        }

    }
}

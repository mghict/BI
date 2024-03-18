using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;
using Moneyon.Common.ExceptionHandling;
using Moneyon.PowerBi.API.Common.Extensions;
using Moneyon.PowerBi.API.Controllers.Base;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Modeling;
using Moneyon.PowerBi.Domain.Service.Services;

namespace Moneyon.PowerBi.API.Controllers;

[Route("api/payment")]
[ApiController]
public class PaymentController : AppBaseController
{
    private readonly PaymentService _paymentService;
    public PaymentController(IHttpContextAccessor contextAccessor, PaymentService paymentService) : base(contextAccessor)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    [Route("receipt-change-status")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.ReceiptPaymentConfirm })]
    public async Task ReceiptConfirm(ReceiptPaymentChangeStatus dto)
    {
        CheckUser();

        await _paymentService.ReceiptPaymentChangeStatusAsync(User!.Person, dto);

    }



    [HttpGet]
    [Route("pending-list")]
    [JWTAuthorization(new PermissionEnum[] {PermissionEnum.ReceiptPaymentView})]
    public async Task<DataResult<PaymentDto>> GetPendingList([FromQuery] DataRequest request)
    {
        return await _paymentService.PaymentReceiptPendingList(request);
    }


    [HttpGet]
    [Route("{trackingCode}/payment-item")]
    [JWTAuthorization(new PermissionEnum[] { PermissionEnum.ReceiptPaymentView })]
    public async Task<PaymentWithImageDto> GetReceiptPaymentItem(string trackingCode)
    {
        var result= await _paymentService.ReceiptPaymentItemAsync(trackingCode);
        result.Document?.SetUserScopeUrls(Url);

        return result;
    }

}
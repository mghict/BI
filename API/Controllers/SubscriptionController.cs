using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.Data;
using Moneyon.PowerBi.API.Controllers.Base;
using Moneyon.PowerBi.Domain.Model.Modeling;
using Moneyon.PowerBi.Domain.Service.Services;

namespace Moneyon.PowerBi.API.Controllers
{
    [Route("api/subscription")]
    [ApiController]
    public class SubscriptionController : AppBaseController
    {
        private readonly SubscriptionService _subscriptionService;

        public SubscriptionController(IHttpContextAccessor contextAccessor, SubscriptionService subscriptionService)
            :base(contextAccessor)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet()]
        [Route("")]
        [JWTAuthorization()]
        public async Task<DataResult<SubscriptionShortDto>> GetSubscriptionList([FromQuery] DataRequest request)
        {
            CheckUser();
            return await _subscriptionService.GetList(User!.PersonId,request);
        }

        [HttpGet()]
        [Route("show-list")]
        [JWTAuthorization(new PermissionEnum[] { PermissionEnum.SubscriptionView})]
        public async Task<DataResult<SubscriptionShortDto>> GetSubscriptionAllList([FromQuery] DataRequest request)
        {
            return await _subscriptionService.GetList(request);
        }

        [HttpGet()]
        [Route("{subscriptionId}")]
        [JWTAuthorization()]
        public async Task<SubscriptionDto> GetSubscriptionList(long subscriptionId)
        {
            CheckUser();
            return await _subscriptionService.GetItem(User!.PersonId,subscriptionId);
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using Moneyon.Common.ExceptionHandling;
using Moneyon.PowerBi.Common;
using Moneyon.PowerBi.Domain.Model.Modeling;

namespace Moneyon.PowerBi.API.Controllers.Base;

[ApiController]
public class AppBaseController : ControllerBase
{
    protected readonly User? User;
    protected HttpContext Context { get; set; }
    public AppBaseController(IHttpContextAccessor contextAccessor):base()
    {
        Context = contextAccessor!.HttpContext;
        var contextUser = Context!.Items["User"];
        if (contextUser is not null)
            User= contextUser as User;
    }


    protected void CheckUser()
    {
        if (User is null)
            throw new BizException(BizExceptionCode.UserNotFound);
    }
}

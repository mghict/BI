using Microsoft.AspNetCore.Mvc;
using Moneyon.PowerBi.Domain.Service.Services;

namespace Moneyon.PowerBi.API.Controllers.Security
{
    [Route("api/seed")]
    [ApiController]
    public class  SeedController:ControllerBase
    {
        private readonly SeedService _seedService;

        public SeedController(SeedService seedService)
        {
            _seedService = seedService;
        }

        [HttpGet]
        [Route("permission")]
        public async Task SeedPermission()
        {
            await _seedService.SeedPermission();
            Ok("Done!!!");
        }

        [HttpGet]
        [Route("role")]
        public async Task SeedRole()
        {
            await _seedService.SeedRoles();
            Ok("Done!!!");
        }

        [HttpGet]
        [Route("person")]
        public async Task SeedPerson()
        {
            await _seedService.SeedPerson();
            Ok("Done!!!");
        }
    }
}

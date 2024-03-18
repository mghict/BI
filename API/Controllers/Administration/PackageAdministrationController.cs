//using Microsoft.AspNetCore.Mvc;
//using Moneyon.Common.Data;
//using Moneyon.PowerBi.Domain.Model.Modeling;
//using Moneyon.PowerBi.Domain.Service.IServices;

//namespace Moneyon.PowerBi.API.Controllers.Administration
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class PackageAdministrationController : ControllerBase
//    {
//        private readonly IPackageService _packageService;

//        public PackageAdministrationController(IPackageService packageService)
//        {
//            _packageService = packageService;
//        }

//        [HttpPost]
//        public async Task CreatePackage([FromBody] PackageCreateReqDto req)
//        {
//            await _packageService.CreatePackage(req);
//        }

//        [HttpGet]
//        public async Task<DataResult<PackageDto>> PackageList([FromQuery]DataRequest req)
//        {
//            return await _packageService.PackageList(req);
//        }

//        [HttpGet("{packageId}")]
//        public async Task DeletePackage([FromRoute]int packageId)
//        {
//            await _packageService.DeletePackage(packageId);
//        }
//    }
//}

using API.DTOs.ListFasilities;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class EmployeeDashboardController : Controller
    {

        private readonly IRequestRepository _requestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IListFasilitasRepository _listFasilitasRepository;


        public EmployeeDashboardController(IRequestRepository requestRepository, IEmployeeRepository employeeRepository, IAccountRepository accountRepository, IListFasilitasRepository listFasilitasRepository)
        {
            _requestRepository = requestRepository;
            _employeeRepository = employeeRepository;
            _accountRepository = accountRepository;
            _listFasilitasRepository = listFasilitasRepository;
        }


        [HttpPost("ListFasility/GetListFasilityByRequestGuidAndFasilityGuid")]
        public async Task<JsonResult> GetListFasilityByRequestGuidAndFasilityGuid(FindListFasilityDto findListFasility)
        {
            var result = await _listFasilitasRepository.GetListFasilityByRequestGuidAndFasilityGuid(findListFasility);
            return Json(result.Data);
        }
        
        [HttpGet("ListFasility/GetListFasilityByRequestGuid/{guid}")]
        public async Task<JsonResult> GetListFasilityByRequestGuid(Guid guid)
        {
            var result = await _listFasilitasRepository.GetListFasilityByRequestGuid(guid);
            return Json(result.Data);
        }
        
        [HttpDelete("ListFasility/Delete")]
        public async Task<JsonResult> DeleteListFasilitas(Guid guid)
        {
            var result = await _listFasilitasRepository.Delete(guid);
            return Json(result.Data);
        }

        [HttpPost("ListFasility/UpdateStokFasility")]
        public async Task<JsonResult> UpdateStokFasility(ListFasilityDto listFasility)
        {
            var result = await _listFasilitasRepository.Put(new Guid(), listFasility);

            if (result.Code == 200)
            {
                return Json(result);
            }
            return Json(result);
        }


    }
}

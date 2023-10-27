
using API.DTOs.Fasilities;
using API.DTOs.ListFasilities;
using API.DTOs.Requests;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class EmployeeRequestController : Controller
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IListFasilitasRepository _listFasilitasRepository;

        public EmployeeRequestController(IRequestRepository requestRepository, IEmployeeRepository employeeRepository, IAccountRepository accountRepository, IListFasilitasRepository listFasilitasRepository)
        {
            _requestRepository = requestRepository;
            _employeeRepository = employeeRepository;
            _accountRepository = accountRepository;
            _listFasilitasRepository = listFasilitasRepository;
        }

        [HttpGet("request/getRequestbyRequestGuid")]
        public async Task<JsonResult> GetRequestByRequestGuid(Guid guid)
        {
            var result = await _requestRepository.GetByDetailRequestGuid(guid);
            var request = new ListRequestDto();

            if (result.Data != null)
            {
                //request = (ListRequestDto)result.Data;
            }
            return Json(result.Data);
        }

        [HttpGet("request/GetCountStatusRequestByEmployeeGuid")]
        public async Task<JsonResult> GetCountStatusRequestByEmployeeGuid()
        {
            var jwt = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
            Guid employee = new Guid(jwt.Data.UserGuid);
            var result = await _requestRepository.GetCountStatusRequestByEmployeeGuid(employee);
            return Json(result.Data);
        }

        [HttpPost("request/update")]
        public async Task<JsonResult> UpdataRequest(RequestDto requestDto)
        {
            var jwt = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
            Guid employee = new Guid(jwt.Data.UserGuid);

            requestDto.EmployeeGuid = employee;

            var result = await _requestRepository.Put(new Guid(), requestDto);
            return Json(result.Data);
        }

        [Route("employee/getEmployee/{guid}")]
        public async Task<JsonResult> GetEmployeeByGuid(Guid guid)
        {
            var result = await _employeeRepository.Get(guid);
            return Json(result.Data);
        }

        [HttpPost("request/insert")]
        public async Task<JsonResult> InsertRequest(CreateRequestDto createRequestDto)
        {
            var jwt = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
            Guid employee = new Guid(jwt.Data.UserGuid);

            createRequestDto.EmployeeGuid = employee;

            var result = await _requestRepository.Insert(createRequestDto);
            if (jwt == null)
            {
                return Json(result);
            }
            if (result.Code == 200)
            {
                return Json(result);
            }
            else if (result.Code == 400)
            {
                return Json(result);
            }
            return Json(result);
        }

        [HttpPost("listfasility/insert")]
        public async Task<JsonResult> InsertFasility(CreateListFasilityDto createListFasility)
        {
            var result = await _listFasilitasRepository.Insert(createListFasility);
            if (result.Code == 200)
            {
                return Json(result);
            }
            else if (result.Code == 400)
            {
                return Json(result);
            }
            return Json(result);
        }
        
        [HttpPost("listfasility/update")]
        public async Task<JsonResult> UpdateListFasility(ListFasilityDto listFasility)
        {
            var result = await _listFasilitasRepository.Put(new Guid(),listFasility);
            if (result.Code == 200)
            {
                return Json(result);
            }
            else if (result.Code == 400)
            {
                return Json(result);
            }
            return Json(result);
        }

        
    }
}

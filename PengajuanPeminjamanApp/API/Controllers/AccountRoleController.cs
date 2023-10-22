using API.Contracts;
using API.DTOs.AccountRoles;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleRepository _accountRoleRepository;

        public AccountRoleController(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        [HttpGet]//http request method
        //get All data
        public IActionResult GetAll()
        {
            var result = _accountRoleRepository.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }

            //Linq
            var data = result.Select(x => (AccountRolesDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<AccountRolesDto>>(data));
        }

        [HttpGet("{guid}")]
        /*
         * method dibawah digunakan untuk mendapatkan data berdasarkan guid
         * 
         * PHARAM :
         * - guid : primary key dari 1 baris data
         */
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountRoleRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }
            return Ok(new ResponseOKHandler<AccountRolesDto>((AccountRolesDto)result)); //konversi explisit
        }

        [HttpPost]
        /*
        * Method dibawah digunakan untuk memasukan data dengan menggunakan parameter dari method DTO
        * 
        * PHARAM :
        * - createAccountRoleDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
        */
        public IActionResult Create(CreateAccountRolesDto createAccountRoleDto)
        {
            try
            {
                var result = _accountRoleRepository.Create(createAccountRoleDto);
                return Ok(new ResponseOKHandler<AccountRolesDto>((AccountRolesDto)result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  new ResponseErrorHandler
                  {
                      Code = StatusCodes.Status500InternalServerError,
                      Status = HttpStatusCode.NotFound.ToString(),
                      Message = "FAILED TO CREATE DATA"
                  });

            }
        }

        [HttpPut]
        /*
        * Method dibawah digunakan untuk mengupdate data dengan menggunakan parameter dari method DTO
        * 
        * PHARAM :
        * - accountRoleDto : kumpulan parameter/method yang sudah ditentukan dari class/objek account
        */
        public IActionResult Update(AccountRolesDto accountRoleDto)
        {
            try
            {


                var existingAccountRole = _accountRoleRepository.GetByGuid(accountRoleDto.Guid); ;
                if (existingAccountRole is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data NOT FOUND"
                    });
                }

                AccountRole toUpdate = accountRoleDto;
                toUpdate.CreateDate = existingAccountRole.CreateDate;

                var result = _accountRoleRepository.Update(toUpdate);
                return Ok(new ResponseOKHandler<string>("DATA UPDATED"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed to update data"
                });
            }
        }

        [HttpDelete("{guid}")]
        /*
        * Method dibawah digunakan untuk menghapus data dengan menggunakan guid
        * 
        * PHARAM :
        * - guid : primary key dari 1 baris data
        */
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var existingAccount = _accountRoleRepository.GetByGuid(guid); ;
                if (existingAccount is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID NOT FOUND"
                    });
                }

                var result = _accountRoleRepository.Delete(existingAccount);
                return Ok(new ResponseOKHandler<string>("DATA DELETED"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed to delete data"
                });
            }
        }
    }
}

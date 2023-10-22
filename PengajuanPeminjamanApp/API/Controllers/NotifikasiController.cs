using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Notifications;
using API.DTOs.Roles;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Principal;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifikasiController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmailHandlerRepository _emailHandlerRepository;

        public NotifikasiController(INotificationRepository notificationRepository, IEmployeeRepository employeeRepository, IEmailHandlerRepository emailHandlerRepository)
        {
            _notificationRepository = notificationRepository;
            _employeeRepository = employeeRepository;
            _emailHandlerRepository = emailHandlerRepository;
        }

        [HttpGet] //http request method
        //get All data
        public IActionResult GetAll()
        {
            var result = _notificationRepository.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }

            var data = result.Select(x => (NotificationDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<NotificationDto>>(data));
        }

        [HttpGet("{unread}")]
        public IActionResult GetUnreead()
        {
            var result = _notificationRepository.GetUnreadNotification();
            if (result is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }
            return Ok(new ResponseOKHandler<NotificationDto>((NotificationDto)result));
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
            var result = _notificationRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data NOT FOUND"
                });
            }
            return Ok(new ResponseOKHandler<NotificationDto>((NotificationDto)result)); //konversi explisit
        }

        [HttpPost()]
        /*
         * Method dibawah digunakan untuk memasukan data dengan menggunakan parameter dari method DTO
         * 
         * PHARAM :
         * - createRoleDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
         */
        public IActionResult NotificationAdminSend(CreateNotificationDto createNotificationDto)
        {
            try
            {
                var employee = _employeeRepository.GetByGuid(createNotificationDto.AccountGuid);
                
                var result = _notificationRepository.Create(createNotificationDto); 
                _emailHandlerRepository.Send("Pengajuan Peminjaman",createNotificationDto.Message, employee.Email, "Admin@no-replay.com");
                return Ok(new ResponseOKHandler<IEnumerable<ForgotPasswordAccountDto>>("Success send Notification"));

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
      * - roleDto : kumpulan parameter/method yang sudah ditentukan dari suatu class/objek
      */
        public IActionResult Update(NotificationDto notificationDto)
        {
            try
            {
                var existingRole = _notificationRepository.GetByGuid(notificationDto.Guid);
                if (existingRole is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID NOT FOUND"
                    });
                }
                Notification toUpdate = existingRole;
                if (notificationDto.IsSeen == false)
                {
                    toUpdate.IsSeen = true ;
                }

                var result = _notificationRepository.Update(toUpdate);
                return Ok(new ResponseOKHandler<string>("DATA UPDATED"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 new ResponseErrorHandler
                 {
                     Code = StatusCodes.Status500InternalServerError,
                     Status = HttpStatusCode.NotFound.ToString(),
                     Message = "FAILED TO UPDATE DATA"
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


                var existingRole = _notificationRepository.GetByGuid(guid); ;
                if (existingRole is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "DATA NOT FOUND"
                    });
                }

                var result = _notificationRepository.Delete(existingRole);
                return Ok(new ResponseOKHandler<string>("DATA DELETED"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 new ResponseErrorHandler
                 {
                     Code = StatusCodes.Status500InternalServerError,
                     Status = HttpStatusCode.NotFound.ToString(),
                     Message = "FAILED TO DELETE DATA"
                 });
            }
        }
    }
}

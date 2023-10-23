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
using API.DTOs.Requests;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifikasiController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmailHandlerRepository _emailHandlerRepository;
        private readonly IRequestRepository _requestRepository;

        public NotifikasiController(INotificationRepository notificationRepository, IEmployeeRepository employeeRepository, IEmailHandlerRepository emailHandlerRepository, IRequestRepository requestRepository = null)
        {
            _notificationRepository = notificationRepository;
            _employeeRepository = employeeRepository;
            _emailHandlerRepository = emailHandlerRepository;
            _requestRepository = requestRepository;
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

        [HttpGet("unreadEmpNotification/{guid}")]
        public IActionResult GetUnreadEmpNotif(Guid guid)
        {
            var result = _notificationRepository.GetUnreadNotification(guid);
            if (result is null)
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

        [HttpGet("allEmpNotification/{guid}")]
        public IActionResult GetAllEmpNotif(Guid guid)
        {
            var result = _notificationRepository.GetAllNotification(guid);
            if (result is null)
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

        [HttpPost("sendEmpNotification")]
        public IActionResult AdminToEmployeeNotification(CreateNotificationDto createNotificationDto)
        {
            try
            {
                var employee = _employeeRepository.GetByGuid(createNotificationDto.AccountGuid);
                var request = _requestRepository.GetByGuid(createNotificationDto.RequestGuid);

                var message = "Pengajuan peminjaman anda" + request.Status.ToString();

                var result = _notificationRepository.Create(createNotificationDto);

                _emailHandlerRepository.Send("Pengajuan Peminjaman",createNotificationDto.Message, employee.Email, "Admin@no-replay.com");
                return Ok(new ResponseOKHandler<NotificationDto>("Success send Notification",(NotificationDto)result));

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

        [HttpPost("sendAdminNotification")]
        public IActionResult EmployeeToAdminNotification(CreateNotificationDto createNotificationDto)
        {
            try
            {
                var employee = _employeeRepository.GetByGuid(createNotificationDto.AccountGuid);

                var message = string.Concat("Pengajuan oleh ", employee.FirstName, " ", employee.LastName, " ",createNotificationDto.Message);

                var result = _notificationRepository.Create(createNotificationDto);

                _emailHandlerRepository.Send("Pengajuan Peminjaman", createNotificationDto.Message, "Admin@no-replay.com", employee.Email);
                return Ok(new ResponseOKHandler<NotificationDto>("Success send Notification", (NotificationDto)result));

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
        public IActionResult Update(Guid guid)
        {
            try
            {
                var existingNotification = _notificationRepository.GetByGuid(guid);
                if (existingNotification is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID NOT FOUND"
                    });
                }
                Notification toUpdate = existingNotification;
                if (existingNotification.IsSeen == false)
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

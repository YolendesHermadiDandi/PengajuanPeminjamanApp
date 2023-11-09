using API.Contracts;
using API.DTOs.ListFasilities;
using API.DTOs.Requests;
using API.DTOs.Rooms;
using API.Models;
using API.Repositories;
using API.Utilities.Enums;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("API/[controller]")]
public class RequestController : ControllerBase
{
    private readonly IRequestRepository _requestRespository;
    private readonly IFasilityRepository _fasilityRepository;
    private readonly IListFasilityRepository _listFasilityRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmailHandlerRepository _emailHandlerRepository;

    public RequestController(IRequestRepository requestRespository, IFasilityRepository fasilityRepository, IListFasilityRepository listFasilityRepository, IRoomRepository roomRepository, IEmployeeRepository employeeRepository, IEmailHandlerRepository emailHandlerRepository)
    {
        _requestRespository = requestRespository;
        _fasilityRepository = fasilityRepository;
        _listFasilityRepository = listFasilityRepository;
        _roomRepository = roomRepository;
        _employeeRepository = employeeRepository;
        _emailHandlerRepository = emailHandlerRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _requestRespository.GetAll();
        if (!result.Any())
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }
        var data = result.Select(x => (RequestDto)x);
        return Ok(new ResponseOKHandler<IEnumerable<RequestDto>>(data));
    }

    [HttpGet("GetCountingRequest")]
    public IActionResult GetRequestDuration(Guid guid)
    {
        var result = _requestRespository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        var view = new DurationRequestDto
        {
            Guid = result.Guid,
            RoomGuid = result.RoomGuid,
            EmployeeGuid = result.EmployeeGuid,
            Status = result.Status,
            RequestDuration = DurationCalculator(result.StartDate, result.EndDate)
        };

        return Ok(new ResponseOKHandler<DurationRequestDto>((DurationRequestDto)view));
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _requestRespository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        return Ok(new ResponseOKHandler<RequestDto>((RequestDto)result));
    }

    [HttpPost("GetStatusRequestRoomByDate/")]
    public IActionResult GetStatusRequestRoomByDate(StatusRequestRoomDto statusRequest)
    {
        var result = _requestRespository.GetStatusRequestRoomByDate(statusRequest);
        if (result is false)
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Ruangan Sudah Ada Yang Pinjam"
            });
        }

        return Ok(new ResponseOKHandler<string>("Rauangan Tersedia Untuk Di Pinjam"));
    }

    [HttpGet("GetRequestByEmployeeGuid")]
    public IActionResult GetRequestByEmployeeGuid(Guid guid)
    {
        var result = _requestRespository.GetRequestByEmployeeGuid(guid);
        var room = _roomRepository.GetAll();
        if (result is null)
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        List<ListRequestDto> data = (from req in result
                                     where req.EmployeeGuid == guid
                                     select new ListRequestDto
                                     {
                                         Guid = req.Guid,
                                         EmployeeGuid = req.EmployeeGuid,
                                         rooms = (from roo in room
                                                  join re in result on roo.Guid equals re.RoomGuid
                                                  where roo.Guid == req.RoomGuid
                                                  select new RoomDto
                                                  {
                                                      Guid = roo.Guid,
                                                      Name = roo.Name,
                                                      Floor = roo.Floor
                                                  }).FirstOrDefault(),
                                         Status = req.Status,
                                         StartDate = req.StartDate,
                                         EndDate = req.EndDate
                                     })
                                     .OrderByDescending(req => req.StartDate)
                                     .ToList();

        return Ok(new ResponseOKHandler<IEnumerable<ListRequestDto>>(data));
    }

    [HttpGet("GetAllDetailRequest")]
    public IActionResult GetAllDetailRequest()
    {
        var request = _requestRespository.GetAll();
        var listFasility = _listFasilityRepository.GetAll();
        var fasilities = _fasilityRepository.GetAll();
        var room = _roomRepository.GetAll();
        if (request is null)
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        List<ListRequestDto> result = (from req in request
                                       select new ListRequestDto
                                       {
                                           Guid = req.Guid,
                                           EmployeeGuid = req.EmployeeGuid,
                                           rooms = (from roo in room
                                                    join re in request on roo.Guid equals re.RoomGuid
                                                    where roo.Guid == req.RoomGuid
                                                    select new RoomDto
                                                    {
                                                        Guid = roo.Guid,
                                                        Name = roo.Name,
                                                        Floor = roo.Floor
                                                    }).FirstOrDefault(),
                                           fasilities = from fas in fasilities
                                                        join li in listFasility on fas.Guid equals li.FasilityGuid
                                                        where li.RequestGuid == req.Guid
                                                        select new ListDetailFasilityDto
                                                        {
                                                            Guid = li.Guid,
                                                            FasilityGuid = li.FasilityGuid,
                                                            Name = fas.Name,
                                                            TotalFasility = li.TotalFasility
                                                        },
                                           Status = req.Status,
                                           StartDate = req.StartDate,
                                           EndDate = req.EndDate,
                                       }).ToList();

        return Ok(new ResponseOKHandler<IEnumerable<ListRequestDto>>(result));
    }

    [HttpGet("GetDetailRequestByGuid/{guid}")]
    public IActionResult GetDetailRequestByGuid(Guid guid)
    {
        var request = _requestRespository.GetAll();
        var listFasility = _listFasilityRepository.GetAll();
        var fasilities = _fasilityRepository.GetAll();
        var room = _roomRepository.GetAll();
        if (request is null)
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        List<ListRequestDto> result = (from req in request
                                       where req.Guid == guid
                                       select new ListRequestDto
                                       {
                                           Guid = req.Guid,
                                           EmployeeGuid = req.EmployeeGuid,
                                           rooms = (from roo in room
                                                    join re in request on roo.Guid equals re.RoomGuid
                                                    where roo.Guid == req.RoomGuid
                                                    select new RoomDto
                                                    {
                                                        Guid = roo.Guid,
                                                        Name = roo.Name,
                                                        Floor = roo.Floor
                                                    }).FirstOrDefault(),
                                           fasilities = from fas in fasilities
                                                        join li in listFasility on fas.Guid equals li.FasilityGuid
                                                        where li.RequestGuid == req.Guid
                                                        select new ListDetailFasilityDto
                                                        {
                                                            Guid = li.Guid,
                                                            FasilityGuid = li.FasilityGuid,
                                                            Name = fas.Name,
                                                            TotalFasility = li.TotalFasility
                                                        },
                                           Status = req.Status,
                                           StartDate = req.StartDate,
                                           EndDate = req.EndDate,
                                       }).ToList();


        return Ok(new ResponseOKHandler<IEnumerable<ListRequestDto>>(result));
    }
    [HttpPut]
    public IActionResult Update(RequestDto requestDto)
    {
        try
        {
            var check = _requestRespository.GetByGuid(requestDto.Guid);
            if (check is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            Request toUpdate = (Request)requestDto;
            toUpdate.CreateDate = check.CreateDate;
            var result = _requestRespository.Update(toUpdate);
            if(toUpdate.Status == StatusLevel.Completed)
            {
                var employee = _employeeRepository.GetByGuid(toUpdate.EmployeeGuid);
                var detailEmail = new DetailEmailDto();
                detailEmail.Name = string.Concat(employee.FirstName, " ", employee.LastName);
                detailEmail.TanggalMulai = toUpdate.StartDate.ToString("dd-MM-yyyy");
                detailEmail.TanggalAkhir = toUpdate.EndDate.ToString("dd-MM-yyyy");
                detailEmail.QrMassage = "";
                detailEmail.TipeEmail = "CompleteAdmin";
                _emailHandlerRepository.Send("Peminjaman Ruangan/Fasilitas Telah Selesai", detailEmail, "Admin@no-replay.com", employee.Email);
                detailEmail.TipeEmail = "CompleteEmployee";
                _emailHandlerRepository.Send("Peminjaman Ruangan/Fasilitas Telah Selesai", detailEmail, employee.Email, "Admin@no-replay.com");

            }

            if (requestDto.Status == Utilities.Enums.StatusLevel.Deleted || requestDto.Status == Utilities.Enums.StatusLevel.Completed || requestDto.Status == Utilities.Enums.StatusLevel.Canceled || requestDto.Status == Utilities.Enums.StatusLevel.Rejected)
            {
                var listFasility = _listFasilityRepository.GetAllListFasilityByReqGuid(requestDto.Guid);
                if (listFasility is null)
                {
                    return Ok(new ResponseOKHandler<String>("Updated Data Success"));
                }

                foreach (var data in listFasility)
                {
                    var fasility = _fasilityRepository.GetByGuid(data.FasilityGuid);
                    fasility.Stock = fasility.Stock + data.TotalFasility;
                    _fasilityRepository.Update(fasility);
                }

            }

            return Ok(new ResponseOKHandler<String>("Updated Data Success"));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to Update data",
                Error = ex.Message
            });
        }
    }

    [HttpPut("updateStatus")]
    public IActionResult UpdateStatus(UpdateStatusDto updateStatusDto)
    {
        try
        {
            var check = _requestRespository.GetByGuid(updateStatusDto.Guid);
            if (check is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            Request toUpdate = check;
            toUpdate.Status = (StatusLevel)Enum.Parse(typeof(StatusLevel), updateStatusDto.Status, true);
            var result = _requestRespository.Update(toUpdate);

            if (toUpdate.Status == Utilities.Enums.StatusLevel.Deleted || toUpdate.Status == Utilities.Enums.StatusLevel.Completed || toUpdate.Status == Utilities.Enums.StatusLevel.Canceled || toUpdate.Status == Utilities.Enums.StatusLevel.Rejected)
            {
                var listFasility = _listFasilityRepository.GetAllListFasilityByReqGuid(updateStatusDto.Guid);
                if (listFasility is null)
                {
                    return Ok(new ResponseOKHandler<String>("Updated Data Success"));
                }

                foreach (var data in listFasility)
                {
                    var fasility = _fasilityRepository.GetByGuid(data.FasilityGuid);
                    fasility.Stock = fasility.Stock + data.TotalFasility;
                    _fasilityRepository.Update(fasility);
                }

            }

            return Ok(new ResponseOKHandler<String>("Updated Data Success"));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to Update data",
                Error = ex.Message
            });
        }
    }
    
    [HttpPut("UpdateComplateRequest")]
    public IActionResult UpdateComplateRequest(Guid guid)
    {
        try
        {
            var check = _requestRespository.GetByGuid(guid);
            if (check is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            Request toUpdate = check;
            toUpdate.Status = StatusLevel.Completed;
            toUpdate.EndDate = DateTime.Now;
            var result = _requestRespository.Update(toUpdate);

            return Ok(new ResponseOKHandler<String>("Updated Data Success"));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to Update data",
                Error = ex.Message
            });
        }
    }

    [HttpPost]
    public IActionResult Create(CreateRequestDto requestDto)
    {
        if (requestDto.RoomGuid.Equals(""))
        {
            requestDto.RoomGuid = null;
        }
        try
        {
            var result = _requestRespository.Create(requestDto);

            try
            {
                var employee = _employeeRepository.GetByGuid(result.EmployeeGuid);
                var detailEmail = new DetailEmailDto();
                detailEmail.Name = string.Concat(employee.FirstName," ", employee.LastName);
                detailEmail.TanggalMulai = result.StartDate.ToString("dd-MM-yyyy");
                detailEmail.TanggalAkhir = result.EndDate.ToString("dd-MM-yyyy");
                detailEmail.QrMassage = "";
                detailEmail.TipeEmail = "Admin";
                _emailHandlerRepository.Send("Peminjaman Ruangan/Fasilitas", detailEmail, "Admin@no-replay.com", employee.Email);
                return Ok(new ResponseOKHandler<RequestDto>((RequestDto)result));

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
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to Create data",
                Error = ex.Message
            });
        }
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            var entity = _requestRespository.GetByGuid(guid);
            if (entity is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            var result = _requestRespository.Delete(entity);
            return Ok(new ResponseOKHandler<String>("Success to Delete data"));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to Deleted data",
                Error = ex.Message
            });
        }

    }


    public static int DurationCalculator(DateTime startDate, DateTime endDate)
    {
        int totalDays = (endDate - startDate).Days;
        //int weekendDays = 0;

        /*for (int i = 0; i < totalDays; i++)
		{
			DateTime currentDate = startDate.AddDays(i);
			if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday)
			{
				weekendDays++;
			}
		}*/
        //int weekDays = totalDays - weekendDays;
        return totalDays;
    }


    [HttpGet("GetCountStatusRequestByEmployeeGuid/{guid}")]
    public IActionResult GetCountStatusRequestByEmployeeGuid(Guid guid)
    {
        var result = _requestRespository.GetRequestByEmployeeGuid(guid);
        var room = _roomRepository.GetAll();
        if (result is null)
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        IEnumerable<CountRequestStatusDto> data = (from req in result
                                                   where req.EmployeeGuid == guid
                                                   group req by req.Status into grouped
                                                   select new CountRequestStatusDto
                                                   {
                                                       status = grouped.Key,
                                                       count = grouped.Count()
                                                   }).ToList();

        return Ok(new ResponseOKHandler<IEnumerable<CountRequestStatusDto>>(data));
    }
    
    [HttpGet("GetCountRequestMonth/")]
    public IActionResult GetCountRequestMonth()
    {
        var result = _requestRespository.GetAll();
        var room = _roomRepository.GetAll();
        if (result is null)
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        var startDate = DateTime.Now.AddMonths(-12);
        var endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

        // Generate a list of dates for each month within the date range
        var allMonths = new List<DateTime>();
        var currentDate = startDate;
        while (currentDate <= endDate)
        {
            allMonths.Add(currentDate);
            currentDate = currentDate.AddMonths(1);
        }

        var data = (
            from r in result
            where r.StartDate >= startDate && r.StartDate <= endDate
            group r by new { Year = r.StartDate.Year, Month = r.StartDate.Month } into grouped
            select new RequestCountMonthDto
            {
                Category = $"{grouped.Key.Year}-{grouped.Key.Month}",
                Data = grouped.Count()
            }
        ).ToList();

        foreach (var month in allMonths)
        {
            if (result.All(d => $"{d.StartDate.Year}-{d.StartDate.Month}" != $"{month.Year}-{month.Month}"))
            {
                data.Add(new RequestCountMonthDto
                {
                    Category = $"{month.Year}-{month.Month}",
                    Data = 0
                });
            }
        }
        data = data.OrderByDescending(d => int.Parse(d.Category.Split('-')[0]))
            .ThenByDescending(d => int.Parse(d.Category.Split('-')[1]))
            .ToList();

        return Ok(new ResponseOKHandler<IEnumerable<RequestCountMonthDto>>(data));

    }

    [HttpGet("GetCountStatusRequestAll/")]
    public IActionResult GetCountStatusRequestAll()
    {
        var result = _requestRespository.GetAll();
        var room = _roomRepository.GetAll();
        if (result is null)
        {
            return NotFound(new ResponseErrorHandler
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data Not Found"
            });
        }

        IEnumerable<CountRequestStatusDto> data = (from req in result
                                                   group req by req.Status into grouped
                                                   select new CountRequestStatusDto
                                                   {
                                                       status = grouped.Key,
                                                       count = grouped.Count()
                                                   }).ToList();

        return Ok(new ResponseOKHandler<IEnumerable<CountRequestStatusDto>>(data));
    }

    [HttpPost("sendEmail")]
    public IActionResult SendEmail(SendEmailDto sendEmailDto)
    {
        try
        {
            var detailEmail = new DetailEmailDto();
            var message = "";
            var url = "";
            detailEmail.QrMassage = "";
            detailEmail.TipeEmail = "Admin";
            var request = _requestRespository.GetByGuid(sendEmailDto.RequestGuid);
            switch (sendEmailDto.Message)
            {
                case "Pengajuan Peminjaman Anda Rejected":
                    message = "<h1>" + sendEmailDto.Message + "</h1>";
                    detailEmail.TipeEmail = "Employee";
                    break;
                case "Pengajuan Peminjaman Anda OnProssesed":
                    detailEmail.TipeEmail = "Employee";
                    break;
                case "Pengajuan Peminjaman Anda OnGoing":
                    url = "https://chart.googleapis.com/chart?cht=qr&chl=" + sendEmailDto.RequestGuid + "&chs=160x160&chld=L|0";
                    detailEmail.QrMassage = $"<br><div align=\"center\">\r\n      <p>Silahkan Lakukan Scan QR yang tersedia untuk melakukan pengambilan atau pemakaian peminjaman yang kamu pinjam :</p>\r\n      <br>\r\n      <img src='{url}' img-thumbnail img-responsive/>\r\n      <br>\r\n    </div>";
                    detailEmail.TipeEmail = "Employee";
                    break;
                default:
                    message = $"<h1>{sendEmailDto.Message}</h1><p>QR Dibawah digunakan untuk masuk kedalam ruangan</P><img src='{url}' img-thumbnail img-responsive/>";
                    break;
            }
            var employee = _employeeRepository.GetByGuid(request.EmployeeGuid);
            detailEmail.Name = string.Concat(employee.FirstName, " ", employee.LastName);
            detailEmail.TanggalMulai = request.StartDate.ToString("dd-MM-yyyy");
            detailEmail.TanggalAkhir = request.EndDate.ToString("dd-MM-yyyy");
            

            _emailHandlerRepository.Send(sendEmailDto.Message, detailEmail, sendEmailDto.RecipientEmail, sendEmailDto.FromEmail);
            return Ok(new ResponseOKHandler<SendEmailDto>("Success send Email", sendEmailDto));

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


}

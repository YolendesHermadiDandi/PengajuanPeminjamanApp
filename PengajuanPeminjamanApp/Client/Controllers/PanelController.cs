using API.DTOs.Requests;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Client.Controllers;

public class PanelController : Controller
{
    private readonly IAccountRepository _accountRepository;
    private readonly IRequestRepository _requestRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IFasilityRepository _fasilityRepository;

    public PanelController(IAccountRepository accountRepository, IRequestRepository requestRepository, IFasilityRepository fasilityRepository, IEmployeeRepository employeeRepository)
    {
        _accountRepository = accountRepository;
        _requestRepository = requestRepository;
        _fasilityRepository = fasilityRepository;
        _employeeRepository = employeeRepository;
    }

    [Authorize(Roles = "Admin, Employee")]
    public async Task<IActionResult> IndexAsync()
    {
        var result = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
        if (result == null)
        {
            return RedirectToAction("Login", "Auth");
        }
        var roles = result.Data.Role;

        var getRole = "";
        foreach (var role in roles)
        {
            if (role == "Admin")
            {
                getRole = role;
            }
        }
        Guid employee = new Guid(result.Data.UserGuid);
        var listRequest = await _requestRepository.GetByEmployeeGuid(employee);
        var dataEmployee = await _employeeRepository.Get(employee);
        ViewBag.NameEmployee = string.Concat(dataEmployee.Data.FirstName, " ", dataEmployee.Data.LastName);
        var data = new List<ListRequestDto>();
        if (result != null)
        {
            if (getRole == "Admin")
            {
                return RedirectToAction("index", "AdminDashboard");
            }
            data = listRequest.Data?.ToList();
            return View(data);
        }

        return View(data);
    }

    [Authorize(Roles = "Admin, Employee")]
    public async Task<IActionResult> PeminjamanFasilitasAsync()
    {
        var result = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
        if (result == null)
        {
            return RedirectToAction("Login", "Auth");
        }
        ViewBag.NameEmployee = result.Data.Name;
        return View();
    }

    [Authorize(Roles = "Admin, Employee")]
    public async Task<IActionResult> KalenderPeminjamanAsync()
    {
        var result = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
        if (result == null)
        {
            return RedirectToAction("Login", "Auth");
        }
        ViewBag.NameEmployee = result.Data.Name;
        return View();
    }
    [Authorize(Roles = "Admin, Employee")]
    public async Task<IActionResult> ListFasilitasAsync()
    {
        var result = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
        if (result == null)
        {
            return RedirectToAction("Login", "Auth");
        }
        ViewBag.NameEmployee = result.Data.Name;
        return View();
    }
    [Authorize(Roles = "Admin, Employee")]
    public async Task<IActionResult> ListPeminjamanAsync()
    {

        var result = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
        if (result == null)
        {
            return RedirectToAction("Login", "Auth");
        }
        ViewBag.NameEmployee = result.Data.Name;
        Guid employee = new Guid(result.Data.UserGuid);
        var listRequest = await _requestRepository.GetByEmployeeGuid(employee);
        var data = new List<ListRequestDto>();
        if (result != null)
        {
            data = listRequest.Data.ToList();
            return View(data);
        }
        return View(data);
    }

    [Authorize(Roles = "Admin, Employee")]
    public async Task<IActionResult> ListRuanganAsync()
    {
        var result = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
        if (result == null)
        {
            return RedirectToAction("Login", "Auth");
        }
        ViewBag.NameEmployee = result.Data.Name;
        return View();
    }
    [Authorize(Roles = "Admin, Employee")]
    public async Task<IActionResult> ProfilAsync()
    {
        var result = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
        if (result == null)
        {
            return RedirectToAction("Login", "Auth");
        }
        ViewBag.NameEmployee = result.Data.Name;
        return View();
    }
    [Route("/GetFasility")]
    public async Task<JsonResult> GetFasility()
    {
        var dataFasility = await _fasilityRepository.Get();
        return Json(dataFasility.Data);
    }

    [Route("/GetFasility/{guid}")]
    public async Task<JsonResult> GetFasility(Guid guid)
    {
        var dataFasility = await _fasilityRepository.Get(guid);
        return Json(dataFasility.Data);
    }
    
    [Route("/Employee/GetProfileImage")]
    public async Task<JsonResult> GetProfileImgEmployee()
    {
        var result = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
        if (result == null)
        {
            return Json(null);
        }
        var getAccount = _accountRepository.Get(new Guid(result.Data.UserGuid));
        if (getAccount == null)
        {
            return Json(null);
        }
        return Json(getAccount.Result.Data.ImgProfile);
    }

    [Route("/Employee/GetProfileName")]
    public async Task<JsonResult> GetProfileGetProfileName()
    {
        var result = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
        if (result == null)
        {
            return Json(null);
        }
        return Json(result.Data.Name);
    }


}

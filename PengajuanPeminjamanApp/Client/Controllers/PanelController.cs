using API.DTOs.Requests;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class PanelController : Controller
{
    private readonly IAccountRepository _accountRepository;
    private readonly IRequestRepository _requestRepository;
    private readonly IFasilityRepository _fasilityRepository;

    public PanelController(IAccountRepository accountRepository, IRequestRepository requestRepository, IFasilityRepository fasilityRepository)
    {
        _accountRepository = accountRepository;
        _requestRepository = requestRepository;
        _fasilityRepository = fasilityRepository;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var result = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
        if (result == null)
        {
            return RedirectToAction("Login", "Auth");
        }
        ViewBag.NameEmployee = result.Data.Name;

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
        var data = new List<ListRequestDto>();
        if (result != null)
        {
            if (getRole == "Admin")
            {
                return RedirectToAction("index", "AdminDashboard");
            }
            data = listRequest.Data?.ToList();
            ViewBag.NameEmployee = result.Data.Name;
            return View(data);
        }
        return View(data);
    }

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


}

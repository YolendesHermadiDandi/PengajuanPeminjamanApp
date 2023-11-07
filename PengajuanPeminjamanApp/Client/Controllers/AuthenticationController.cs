
using API.DTOs.Accounts;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class AuthenticationController : Controller
{

    private readonly IAccountRepository _accountRepository;

    public AuthenticationController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpGet("Auth/Login")]
    public IActionResult Index()
    {
        ViewBag.Alert = "alert";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginAccountDto login)
    {
        var result = await _accountRepository.Login(login);
        if(result.Data == null)
        {
            TempData["Alerts"] = "Email atau Password Anda Salah";
            return RedirectToAction("Login", "Auth");
        }
        if (result.Data != null)
        {

            HttpContext.Session.SetString("JWToken", result.Data.Token);

            return RedirectToAction("Index", "Panel");
        }
        TempData["Alerts"] = "Akun Tidak Ditemukan";
        return RedirectToAction("Login", "Auth");
    }

    [HttpGet("Auth/Signup")]
    public IActionResult Signup()
    {
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Auth");
    }


    [HttpGet("unauthorized/")]
    public IActionResult unauthorized()
    {
        return View();
    }
    [HttpGet("Notfound/")]
    public IActionResult Notfound()
    {
        return View();
    }
    [HttpGet("Forbidden/")]
    public IActionResult Forbidden()
    {
        return View();
    }
}

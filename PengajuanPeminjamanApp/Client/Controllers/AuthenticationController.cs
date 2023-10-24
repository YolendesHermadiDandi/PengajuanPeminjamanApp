
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
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginAccountDto login)
    {
        var result = await _accountRepository.Login(login);

        if (result.Status == "OK")
        {

            HttpContext.Session.SetString("JWToken", result.Data.Token);
            
            return RedirectToAction("Index", "Panel");
        }
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
}

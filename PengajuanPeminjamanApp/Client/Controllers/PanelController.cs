using API.DTOs.Requests;
using Client.Contracts;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;
using System.Linq;

namespace Client.Controllers;

public class PanelController : Controller
{
    private readonly IAccountRepository _accountRepository;
    private readonly IRequestRepository _requestRepository;

    public PanelController(IAccountRepository accountRepository, IRequestRepository requestRepository)
    {
        _accountRepository = accountRepository;
        _requestRepository = requestRepository;
    }

    public async Task<IActionResult> IndexAsync()
	{
        var result = await _accountRepository.GetClaims(HttpContext.Session.GetString("JWToken"));
		if (result == null)
		{
            return RedirectToAction("Login", "Auth");
        }
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
	
	public IActionResult PeminjamanFasilitas()
	{
		return View();
	}
	public IActionResult ListFasilitas()
	{
		return View();
	}
	public IActionResult ListPeminjaman()
	{
		return View();
	}
	
	public IActionResult ListRuangan()
	{
		return View();
	}
	public IActionResult Profil()
	{
		return View();
	}
	public IActionResult Notification()
	{
		return View();
	}
}

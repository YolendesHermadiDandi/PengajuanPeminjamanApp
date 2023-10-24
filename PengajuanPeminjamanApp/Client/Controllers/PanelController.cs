using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers;

public class PanelController : Controller
{
	private readonly ILogger<PanelController> _logger;

	public PanelController(ILogger<PanelController> logger)
	{
		_logger = logger;
	}

	public IActionResult Index()
	{
		return View();
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

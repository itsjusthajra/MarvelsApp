using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MarvelsApp.Models;
using MarvelsApp.Services;

namespace MarvelsApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;


    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var heroList = _context.Characters.
            Where(c => c.Category == "Hero").
            ToList();
        var villainList = _context.Characters.
            Where(c => c.Category == "Villain").
            ToList();

        var viewModel = new ViewModel
        {
            Heroes = heroList,
            Villains = villainList
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

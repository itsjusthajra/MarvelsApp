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
        var heroList = _context.Characters
    .Where(c => c.Category.Name == "Hero")
    .ToList();

        var antiheroList = _context.Characters
            .Where(c => c.Category.Name == "Anti-Hero")
            .ToList();

        var villainList = _context.Characters
            .Where(c => c.Category.Name == "Villain")
            .ToList();

        var superVillainList = _context.Characters
            .Where(c => c.Category.Name == "Super-Villain")
            .ToList();
        var viewModel = new ViewModel
        {
            Heroes = heroList,
            AntiHeroes = antiheroList,
            Villains = villainList,
            SuperVillains = superVillainList
        };

        return View(viewModel);
    }

    public IActionResult Search(string query)
    {
        if (string.IsNullOrEmpty(query))
            return RedirectToAction("Index", "Home");

        var results = _context.Characters
            .Where(c => c.Name.Contains(query))
            .ToList();
        ViewBag.SearchQuery = query;
        return View(results);
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

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;


    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("addPlayer")]
    public IActionResult Create(Player newPlayer)
    {
        if (!ModelState.IsValid)
        {
            return View("Index");
        }

        HttpContext.Session.SetString("PlayerName", newPlayer.Name);

        string? sessionName = HttpContext.Session.GetString("PlayerName");
        Console.WriteLine($"{sessionName} has logged in!");
        return RedirectToAction("Dashboard");
    }

    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {

        string? playerName = HttpContext.Session.GetString("PlayerName");
        if (string.IsNullOrEmpty(playerName))
        {
            return RedirectToAction("Index");
        }

        if (HttpContext.Session.GetInt32("Value") == null)
        {
            HttpContext.Session.SetInt32("Value", 22);
        }
        ViewBag.Value = HttpContext.Session.GetInt32("Value");
        return View("Dashboard");
    }

    [HttpPost("performMath")]
    public IActionResult DoMath(string operation)
    {
        int value = HttpContext.Session.GetInt32("Value") ?? 22;

        if (operation == "+")
        {
            value += 1;
        }
        else if (operation == "-")
        {
            value -= 1;
        }
        else if (operation == "x")
        {
            value *= 2;
        }
        else if (operation == "Random")
        {
            Random random = new Random();
            int randomValue = random.Next(1, 11);
            value += randomValue;
        }

        HttpContext.Session.SetInt32("Value", value);

        return RedirectToAction("Dashboard");
    }

    [HttpPost("clearSession")]
    public IActionResult ClearSession()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
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

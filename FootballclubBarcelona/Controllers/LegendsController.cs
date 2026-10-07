using Microsoft.AspNetCore.Mvc;

namespace FootballclubBarcelona.Controllers;

public class LegendsController : Controller
{
    public IActionResult legends()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
using Microsoft.AspNetCore.Authorization;

namespace FootballclubBarcelona.Controllers;
using System.Security.Claims;
using FootballclubBarcelona.Data;
using FootballclubBarcelona.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
public class UserProfileController : Controller
{
    private readonly ApplicationDbContext _context;
    public UserProfileController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // Get /UserProfile/Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    // Post /UserProfile/Login
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _context.UserProfiles.FirstOrDefaultAsync(x => x.Username
                                                                   == request.Username &&
                                                                   x.Password == request.Password);
        if (user == null)
        {
            ModelState.AddModelError("","Invalid username or password");
            return View(request);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        return RedirectToAction("Index", "Home");
    }
    
    // Get: /UserProfile/Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    // Post: /UserProfile/Register
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (await _context.UserProfiles.AnyAsync(x => x.Username == request.Username))
        {
            ModelState.AddModelError("Username", "Username already exists");
            return View(request);
        }

        var user = new UserProfile()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            Password = request.Password,
            Role = "Admin", 
            Email = request.Email,
        };
        _context.UserProfiles.Add(user);
        await _context.SaveChangesAsync();
        return RedirectToAction("Login");
    }

    // Get: /UserProfile/Logout
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return RedirectToAction("Login");
        }

        var user = await _context.UserProfiles.Include(x=>x.TicketsPurchasings)
            .ThenInclude(x=>x.Ticket).Include(x=>x.TicketsPurchasings).ThenInclude(x=>x.Match)
            .Include(x=>x.Products).ThenInclude(x=>x.Product)
            .FirstOrDefaultAsync(x => x.Id == int.Parse(userId));
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return RedirectToAction("Login");
        }

        var purchasing = await _context.TicketPurchasings
            .FirstOrDefaultAsync(x => x.Id == id
                                      && x.UserProfileId == int.Parse(userId));
        if (purchasing == null)
        {
            return NotFound();
        }

        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(x => x.Id == purchasing.TicketId);
        if (ticket != null)
        {
            ticket.IsSold = false;
        }

        _context.TicketPurchasings.Remove(purchasing);
        await _context.SaveChangesAsync();
        return RedirectToAction("Profile");
    }
    
    // ova trebit da se namestit
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return RedirectToAction("Login");
        }

        var purchasing = await _context.ProductPurchasings
            .FirstOrDefaultAsync(x => x.Id == id
                                      && x.UserProfileId == int.Parse(userId));
        if (purchasing == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == purchasing.ProductId);
        if (product != null)
        {
            product.IsBought = false;
        }

        _context.ProductPurchasings.Remove(purchasing);
        await _context.SaveChangesAsync();
        return RedirectToAction("Profile");
    }
}
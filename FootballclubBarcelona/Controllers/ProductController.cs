using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FootballclubBarcelona.Data;
using FootballclubBarcelona.Models;
using Microsoft.AspNetCore.Authorization;

namespace FootballclubBarcelona.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index(string? name, string? category)
        {
            ViewBag.NameFilter = name;
            ViewBag.CategoryFilter = category;
            return View(await _context.Products.Where(
                x=>(x.Name.Contains(name) || name==null) && (x.CategoryName.Contains(category) || category==null)).Where(x=>x.IsBought!=true).ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Image,Size,Price,CategoryName")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Image,Size,Price,CategoryName")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [Authorize]
        public async Task<IActionResult> Productdetailsorder(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "UserProfile");
            }
            var product = await _context.Products.Include(x=>x.Players)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.ProductName = product.Name;
            ViewBag.ProductCategory = product.CategoryName;
            ViewBag.ProductPrice = product.Price;
            ViewBag.ProductImage = product.Image;

            var AllPlayers = await _context.TeamAndPlayers.ToListAsync();
            ViewBag.ALLPLAYERS = new SelectList(AllPlayers, "name", "name");
            return View(product);
        }
        
        [Authorize]
        public async Task<IActionResult> Orderr(int id,string size,string playername)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "UserProfile");
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.ProductName = product.Name;
            ViewBag.ProductCategory = product.CategoryName;
            ViewBag.ProductPrice = product.Price;
            ViewBag.ProductImage = product.Image;
            ViewBag.PlayerName = playername;
            ViewBag.Size = size;

            var purchasing = new ProductPurchasing
            {
                ProductId = product.Id,
                PlayerName = playername
            };

            return View(purchasing);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Order(ProductPurchasing purchasing)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "UserProfile");
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == purchasing.ProductId);

            if (product == null)
            {
                return NotFound();
            }

            var user = await _context.UserProfiles
                .FirstOrDefaultAsync(x => x.Id == int.Parse(userId));

            if (user == null)
            {
                return NotFound("User does not exist");
            }

            purchasing.UserProfileId = user.Id;

            _context.ProductPurchasings.Add(purchasing);

            await _context.SaveChangesAsync();

            return RedirectToAction("Profile", "UserProfile");
        }
        
        [Authorize]
        public async Task<IActionResult> Card(int id,string playername)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "UserProfile");
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.ProductName = product.Name;
            ViewBag.ProductCategory = product.CategoryName;
            ViewBag.ProductPrice = product.Price;
            ViewBag.ProductImage = product.Image;

            var purchasing = new ProductPurchasing
            {
                ProductId = product.Id,
                PlayerName = playername
            };

            return View(purchasing);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}

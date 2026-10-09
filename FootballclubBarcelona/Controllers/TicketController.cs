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
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ticket
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tickets.Include(t=>t.Match).Where(x=>x.IsSold!=true).ToListAsync());
        }

        // GET: Ticket/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Ticket/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Matches = new SelectList(_context.Matches, "Id", "Opponent");
            return View();
        }

        // POST: Ticket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Side,Block,Seat,price,MatchId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Matches = new SelectList(_context.Matches, "Id", "Opponent",ticket.MatchId);
            return View(ticket);
        }

        // GET: Ticket/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Ticket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Side,Block,Seat,price")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            var existingTicket = await _context.Tickets.FindAsync(id);

            if (existingTicket == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                existingTicket.Side = ticket.Side;
                existingTicket.Block = ticket.Block;
                existingTicket.Seat = ticket.Seat;
                existingTicket.price = ticket.price;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(ticket);
        }

        public async Task<IActionResult> Purchasee(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "UserProfile");
            }

            var ticket = await _context.Tickets.Include(x=>x.Match)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            var purchasing = new TicketPurchasing
            {
                TicketId = id,
                MatchId = ticket.MatchId,
                UserProfileId = int.Parse(userId),
            };
            ViewBag.Side = ticket.Side;
            ViewBag.Block = ticket.Block;
            ViewBag.Seat = ticket.Seat;
            ViewBag.Match = ticket.Match.Opponent;
            ViewBag.price = ticket.price;
            return View(purchasing);
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Purchase(TicketPurchasing ticketPurchasings)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "UserProfile");
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(x => x.Id == ticketPurchasings.TicketId);
            if (ticket == null)
            {
                return NotFound();
            }

            var purchasing = new TicketPurchasing
            {
                TicketId = ticketPurchasings.TicketId,
                MatchId = ticket.MatchId,
                UserProfileId = int.Parse(userId),
            };
            ticket.IsSold = true;
            _context.TicketPurchasings.Add(purchasing);
            await _context.SaveChangesAsync();
            return RedirectToAction("Profile","UserProfile");
        }

        // GET: Ticket/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Ticket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}

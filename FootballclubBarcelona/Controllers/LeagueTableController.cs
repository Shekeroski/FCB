using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FootballclubBarcelona.Data;
using FootballclubBarcelona.Models;

namespace FootballclubBarcelona.Controllers
{
    public class LeagueTableController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeagueTableController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeagueTable
        public async Task<IActionResult> Index()
        {
            return View(await _context.LeagueTable.ToListAsync());
        }

        // GET: LeagueTable/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueTable = await _context.LeagueTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leagueTable == null)
            {
                return NotFound();
            }

            return View(leagueTable);
        }

        // GET: LeagueTable/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeagueTable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rank,Name,Logo,points,Wins,Draws,Losses,GD")] LeagueTable leagueTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leagueTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leagueTable);
        }

        // GET: LeagueTable/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueTable = await _context.LeagueTable.FindAsync(id);
            if (leagueTable == null)
            {
                return NotFound();
            }
            return View(leagueTable);
        }

        // POST: LeagueTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rank,Name,Logo,points,Wins,Draws,Losses,GD")] LeagueTable leagueTable)
        {
            if (id != leagueTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leagueTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeagueTableExists(leagueTable.Id))
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
            return View(leagueTable);
        }

        // GET: LeagueTable/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leagueTable = await _context.LeagueTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leagueTable == null)
            {
                return NotFound();
            }

            return View(leagueTable);
        }

        // POST: LeagueTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leagueTable = await _context.LeagueTable.FindAsync(id);
            if (leagueTable != null)
            {
                _context.LeagueTable.Remove(leagueTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeagueTableExists(int id)
        {
            return _context.LeagueTable.Any(e => e.Id == id);
        }
    }
}

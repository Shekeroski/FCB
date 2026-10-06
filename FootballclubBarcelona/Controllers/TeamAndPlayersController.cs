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
    public class TeamAndPlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamAndPlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TeamAndPlayers
        public async Task<IActionResult> Index()
        {
            return View(await _context.TeamAndPlayers.ToListAsync());
        }

        // GET: TeamAndPlayers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamAndPlayers = await _context.TeamAndPlayers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamAndPlayers == null)
            {
                return NotFound();
            }

            return View(teamAndPlayers);
        }

        // GET: TeamAndPlayers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeamAndPlayers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,years,position,image")] TeamAndPlayers teamAndPlayers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamAndPlayers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teamAndPlayers);
        }

        // GET: TeamAndPlayers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamAndPlayers = await _context.TeamAndPlayers.FindAsync(id);
            if (teamAndPlayers == null)
            {
                return NotFound();
            }
            return View(teamAndPlayers);
        }

        // POST: TeamAndPlayers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,years,position,image")] TeamAndPlayers teamAndPlayers)
        {
            if (id != teamAndPlayers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamAndPlayers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamAndPlayersExists(teamAndPlayers.Id))
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
            return View(teamAndPlayers);
        }

        // GET: TeamAndPlayers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamAndPlayers = await _context.TeamAndPlayers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamAndPlayers == null)
            {
                return NotFound();
            }

            return View(teamAndPlayers);
        }

        // POST: TeamAndPlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teamAndPlayers = await _context.TeamAndPlayers.FindAsync(id);
            if (teamAndPlayers != null)
            {
                _context.TeamAndPlayers.Remove(teamAndPlayers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamAndPlayersExists(int id)
        {
            return _context.TeamAndPlayers.Any(e => e.Id == id);
        }
    }
}

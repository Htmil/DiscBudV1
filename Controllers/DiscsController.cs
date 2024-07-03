using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiscBudV1.Data;
using DiscBudV1.Models;
using DiscBudV1.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace DiscBudV1.Controllers
{
    public class DiscsController : Controller
    {
        private readonly DiscBudV1Context _context;

        private readonly ILogger<DiscsController> _logger;
        private readonly UserManager<DiscBudV1User> _userManager;

        public DiscsController(DiscBudV1Context context, ILogger<DiscsController> logger, UserManager<DiscBudV1User> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        // GET: Discs
        public async Task<IActionResult> Index()
        {
            var discBudV1Context = _context.Discs.Include(d => d.User);
            return View(await discBudV1Context.ToListAsync());
        }

        // GET: Discs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disc = await _context.Discs
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disc == null)
            {
                return NotFound();
            }

            return View(disc);
        }

        // GET: Discs/Create
        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/AccessDenied");
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Discs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Manufacturer,Name,Type,Speed,Glide,Turn,Fade,Characteristics")] Disc disc)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            disc.User = user;
            
            if (!ModelState.IsValid)
            {
                _context.Add(disc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", disc.UserId);
            return View(disc);
        }

        // GET: Discs/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/AccessDenied");
            }
            if (id == null || _context.Discs == null)
            {
                return NotFound();
            }

            var disc = await _context.Discs.FindAsync(id);
            if (disc == null)
            {
                return NotFound();
            }
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", disc.UserId);
            return View(disc);
        }

        // POST: Discs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Manufacturer,Name,Type,Speed,Glide,Turn,Fade,Characteristics")] Disc disc)
        {
            if (id != disc.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var existingDisc = await _context.Discs.FindAsync(id);
                if (existingDisc == null)
                {
                    return NotFound();
                }
                existingDisc.Turn = disc.Turn;
                existingDisc.Manufacturer = disc.Manufacturer;
                existingDisc.Glide = disc.Glide;
                existingDisc.Fade = disc.Fade;
                existingDisc.Characteristics = disc.Characteristics;
                existingDisc.Type = disc.Type;
                existingDisc.Speed = disc.Speed;
                existingDisc.Name = disc.Name;

                try
                {
                    _context.Update(existingDisc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscExists(disc.Id))
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
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", disc.UserId);
            return View(disc);
        }

        // GET: Discs/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disc = await _context.Discs
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disc == null)
            {
                return NotFound();
            }

            return View(disc);
        }

        // POST: Discs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disc = await _context.Discs.FindAsync(id);
            if (disc != null)
            {
                _context.Discs.Remove(disc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscExists(int id)
        {
            return _context.Discs.Any(e => e.Id == id);
        }
    }
}

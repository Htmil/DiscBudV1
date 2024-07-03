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
    public class InvdiscsController : Controller
    {
        private readonly DiscBudV1Context _context;
        private readonly UserManager<DiscBudV1User> _userManager;

        public InvdiscsController(DiscBudV1Context context, UserManager<DiscBudV1User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //public async Task<List<Invdisc>> GetAllDiscsUserAsync()
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var user = await _userManager.FindByIdAsync(userId);

        //    List<Invdisc> invdiscs;

        //    if (await _userManager.IsInRoleAsync(user, "Admin"))
        //    {
        //        invdiscs = await _context.invdiscs.ToListAsync();
        //    }
        //    else
        //    {
        //        invdiscs = await _context.invdiscs
        //            .Where(i => i.UserId == userId)
        //            .ToListAsync();
        //    }

        //    foreach (var invdisc in invdiscs)
        //    {
        //        invdisc.User = user;
        //    }

        //    return invdiscs;
        //}

        // GET: Invdiscs
        //public async Task<IActionResult> Index()
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var user = await _userManager.FindByIdAsync(userId);

        //    List<Invdisc> invdiscs;

        //    invdiscs = await _context.invdiscs
        //          .Where(i => i.UserId == userId)
        //          .ToListAsync();
        //    //var invdiscs = await GetAllDiscsUserAsync();

        //    //return View(invdiscs);
        //    var discBudV1Context = _context.invdiscs.Include(i => i.Disc).Include(i => i.User);
        //    return View(await discBudV1Context.ToListAsync());

        //}
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var invdiscs = await _context.invdiscs
                .Where(i => i.UserId == userId)
                .Include(i => i.Disc)
                .Include(i => i.User)
                .ToListAsync();
            return View(invdiscs);
        }

        public async Task<IActionResult> AddDiscToInv(int discId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

                var invDiscId = new Invdisc
                {
                    UserId = userId,
                    DiscId = discId
                };

                _context.invdiscs.Add(invDiscId);
                _context.SaveChanges();

            //return View("Index");
            return RedirectToAction(nameof(Index));
        }

        // GET: Invdiscs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invdisc = await _context.invdiscs
                .Include(i => i.Disc)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invdisc == null)
            {
                return NotFound();
            }

            return View(invdisc);
        }

        // GET: Invdiscs/Create
        public IActionResult Create()
        {
            ViewData["DiscId"] = new SelectList(_context.Discs, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Invdiscs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,DiscId")] Invdisc invdisc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invdisc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscId"] = new SelectList(_context.Discs, "Id", "Id", invdisc.DiscId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", invdisc.UserId);
            return View(invdisc);
        }

        // GET: Invdiscs/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invdisc = await _context.invdiscs.FindAsync(id);
            if (invdisc == null)
            {
                return NotFound();
            }
            ViewData["DiscId"] = new SelectList(_context.Discs, "Id", "Id", invdisc.DiscId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", invdisc.UserId);
            return View(invdisc);
        }

        // POST: Invdiscs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,UserId,DiscId")] Invdisc invdisc)
        {
            if (id != invdisc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invdisc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvdiscExists(invdisc.Id))
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
            ViewData["DiscId"] = new SelectList(_context.Discs, "Id", "Id", invdisc.DiscId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", invdisc.UserId);
            return View(invdisc);
        }

        // GET: Invdiscs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invdisc = await _context.invdiscs
                .Include(i => i.Disc)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invdisc == null)
            {
                return NotFound();
            }

            return View(invdisc);
        }

        // POST: Invdiscs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var invdisc = await _context.invdiscs.FindAsync(id);
            if (invdisc != null)
            {
                _context.invdiscs.Remove(invdisc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvdiscExists(int? id)
        {
            return _context.invdiscs.Any(e => e.Id == id);
        }
    }
}

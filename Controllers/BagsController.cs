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
using DiscBudV1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DiscBudV1.Controllers
{
    public class BagsController : Controller
    {
        private readonly DiscBudV1Context _context;
        private readonly UserManager<DiscBudV1User> _userManager;

        public BagsController(DiscBudV1Context context, UserManager<DiscBudV1User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //GET: Bags

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var bags = await _context.Bags
                .Where(b => b.UserId == userId)
                .Include(b => b.Invdisc)
                    .ThenInclude(i => i.Disc)
                .Include(b => b.User)
                .ToListAsync();


            var viewModel = bags.Select(b => new BagDashboardViewModel
            {
                Bag = b,
                Invdisc = b.Invdisc,
                Disc = b.Invdisc?.Disc
            }).ToList();

            return View(viewModel);
        }


    //public async Task<IActionResult> Index()
    //{
    //    var invdiscs = _context.invdiscs.ToList();
    //    var discs = _context.Discs.ToList();
    //    var bags = _context.Bags.ToList();
    //    var models = new List<Tuple<Invdisc, Disc, Bag>>();

    //    var discBudV1Context = _context.Bags.Include(b => b.Invdisc).Include(b => b.User);
    //    return View(await discBudV1Context.ToListAsync());


    //}
    //    public IActionResult Index()
    //    {

    //        var invdiscs = _context.Invdiscs.ToList();
    //        var discs = _context.Discs.ToList();
    //        var bags = _context.Bags.ToList();
    //        var models = new List<Tuple<Invdisc, Disc, Bag>>();

    //        foreach (var invdisc in invdiscs)
    //        {
    //            var disc = discs.FirstOrDefault(d => d.Id == invdisc.DiscId);
    //            var bag = bags.FirstOrDefault(b => b.Id == invdisc.BagId);

    //            if (disc != null && bag != null)
    //            {
    //                models.Add(Tuple.Create(invdisc, disc, bag));
    //            }
    //        }

    //        return View(models);
    //    }
    //}


    public async Task<IActionResult> AddDiscToBag(int invdiscId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            var bagId = new Bag
            {
                UserId = userId,
                InvdiscId = invdiscId
            };

            _context.Bags.Add(bagId);
            _context.SaveChanges();

            //return View("Index");
            return RedirectToAction(nameof(Index));
        }

        // GET: Bags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bag = await _context.Bags
                .Include(b => b.Invdisc)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BagId == id);
            if (bag == null)
            {
                return NotFound();
            }

            return View(bag);
        }

        // GET: Bags/Create
        public IActionResult Create()
        {
            ViewData["InvdiscId"] = new SelectList(_context.invdiscs, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Bags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BagId,UserId,InvdiscId")] Bag bag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvdiscId"] = new SelectList(_context.invdiscs, "Id", "Id", bag.InvdiscId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bag.UserId);
            return View(bag);
        }

        // GET: Bags/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bag = await _context.Bags.FindAsync(id);
            if (bag == null)
            {
                return NotFound();
            }
            ViewData["InvdiscId"] = new SelectList(_context.invdiscs, "Id", "Id", bag.InvdiscId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bag.UserId);
            return View(bag);
        }

        // POST: Bags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id, [Bind("BagId,UserId,InvdiscId")] Bag bag)
        {
            if (id != bag.BagId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BagExists(bag.BagId))
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
            ViewData["InvdiscId"] = new SelectList(_context.invdiscs, "Id", "Id", bag.InvdiscId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bag.UserId);
            return View(bag);
        }

        // GET: Bags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bag = await _context.Bags
                .Include(b => b.Invdisc)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BagId == id);
            if (bag == null)
            {
                return NotFound();
            }

            return View(bag);
        }

        // POST: Bags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var bag = await _context.Bags.FindAsync(id);
            if (bag != null)
            {
                _context.Bags.Remove(bag);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BagExists(int? id)
        {
            return _context.Bags.Any(e => e.BagId == id);
        }
    }
}

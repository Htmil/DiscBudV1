using DiscBudV1.Data;
using DiscBudV1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DiscBudV1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DiscBudV1Context _context;
        private readonly List<Disc> _disc;

        public HomeController(ILogger<HomeController> logger, DiscBudV1Context context)
        {
            _logger = logger;
            _context = context;
            _disc = new List<Disc>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> AllDiscs()
        {
            var discs = from n in _context.Discs select n;
            List<Disc> discList = await discs.ToListAsync();
            return View(discList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

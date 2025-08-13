using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using YAKBAKv2.Data;
using YAKBAKv2.Models;
using System.Linq;

namespace YakBak.Controllers
{
    public class ClansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LIST ALL CLANS - PUBLIC
        [AllowAnonymous]
        public IActionResult Index()
        {
            var clans = _context.Clans.ToList();
            return View(clans);
        }

        // VIEW DETAILS OF A CLAN - PUBLIC
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var clan = _context.Clans.Find(id);
            if (clan == null)
            {
                return NotFound();
            }

            return View(clan);
        }

        // SHOW CREATE FORM - PRIVATE
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // HANDLE CREATE POST - PRIVATE
        [HttpPost]
        [Authorize]
        public IActionResult Create([Bind("ClanName,ClanDescription,ClanType,NumberOfMembers,ClanStreak")] Clan newClan)
        {
            if (ModelState.IsValid)
            {
                _context.Clans.Add(newClan);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(newClan);
        }

        // SHOW EDIT FORM - PRIVATE
        [Authorize]
        public IActionResult Edit(int id)
        {
            var clan = _context.Clans.Find(id);
            if (clan == null)
            {
                return NotFound();
            }

            return View(clan);
        }

        // HANDLE EDIT POST - PRIVATE
        [HttpPost]
        [Authorize]
        public IActionResult Edit(Clan updatedClan)
        {
            if (ModelState.IsValid)
            {
                _context.Clans.Update(updatedClan);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(updatedClan);
        }

        // SHOW DELETE CONFIRMATION - PRIVATE
        [Authorize]
        public IActionResult Delete(int id)
        {
            var clan = _context.Clans.Find(id);
            if (clan == null)
            {
                return NotFound();
            }

            return View(clan);
        }

        // HANDLE DELETE CONFIRMATION - PRIVATE
        [HttpPost, ActionName("Delete")]
        [Authorize]
        public IActionResult DeleteConfirmed(int id)
        {
            var clan = _context.Clans.Find(id);
            if (clan != null)
            {
                _context.Clans.Remove(clan);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

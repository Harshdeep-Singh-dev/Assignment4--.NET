using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using YAKBAKv2.Data;
using YAKBAKv2.Models;
using System.Linq;

namespace YakBak.Controllers
{
    public class ClanMembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClanMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // SHOW ALL MEMBERS OR FILTERED BY CLAN - PUBLIC
        [AllowAnonymous]
        public IActionResult Index(int? clanId)
        {
            var members = _context.ClanMembers
                .Include(m => m.Clan)
                .AsQueryable();

            if (clanId.HasValue)
            {
                members = members.Where(m => m.ClanId == clanId.Value);
            }

            return View(members.ToList());
        }

        // DETAILS FOR A SINGLE MEMBER - PUBLIC
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var member = _context.ClanMembers.Find(id);

            if (member == null)
            {
                return NotFound();
            }

            member.Clan = _context.Clans.Find(member.ClanId);

            return View(member);
        }

        // SHOW CREATE FORM - PRIVATE
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.Clans = _context.Clans.ToList();
            return View();
        }

        // HANDLE CREATE SUBMIT - PRIVATE
        [HttpPost]
        [Authorize]
        public IActionResult Create([Bind("MemberName,Role,TimeOfJoin,ClanId")] ClanMember newMember)
        {
            if (ModelState.IsValid)
            {
                _context.ClanMembers.Add(newMember);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Clans = _context.Clans.ToList();
            return View(newMember);
        }

        // SHOW EDIT FORM - PRIVATE
        [Authorize]
        public IActionResult Edit(int id)
        {
            var member = _context.ClanMembers.Find(id);
            if (member == null)
            {
                return NotFound();
            }

            ViewBag.Clans = _context.Clans.ToList();
            return View(member);
        }

        // HANDLE EDIT SUBMIT - PRIVATE
        [HttpPost]
        [Authorize]
        public IActionResult Edit(ClanMember updatedMember)
        {
            if (ModelState.IsValid)
            {
                _context.ClanMembers.Update(updatedMember);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Clans = _context.Clans.ToList();
            return View(updatedMember);
        }

        // SHOW DELETE CONFIRMATION - PRIVATE
        [Authorize]
        public IActionResult Delete(int id)
        {
            var member = _context.ClanMembers.Find(id);

            if (member == null)
            {
                return NotFound();
            }

            member.Clan = _context.Clans.Find(member.ClanId);

            return View(member);
        }

        // HANDLE DELETE CONFIRMATION - PRIVATE
        [HttpPost, ActionName("Delete")]
        [Authorize]
        public IActionResult DeleteConfirmed(int id)
        {
            var member = _context.ClanMembers.Find(id);
            if (member != null)
            {
                _context.ClanMembers.Remove(member);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}

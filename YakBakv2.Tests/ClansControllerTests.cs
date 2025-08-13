using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using YakBak.Controllers;        // your controller namespace
using YAKBAKv2.Data;            // ApplicationDbContext
using YAKBAKv2.Models;          // Clan model

namespace YAKBAKv2.Tests
{
    public class ClansControllerTests
    {
        private ApplicationDbContext BuildContextWithData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var ctx = new ApplicationDbContext(options);

            ctx.Clans.AddRange(
                new Clan
                {
                    ClanId = 1,
                    ClanName = "Alpha",
                    ClanDescription = "A",
                    ClanType = "Casual",
                    NumberOfMembers = 10,
                    ClanStreak = 2
                },
                new Clan
                {
                    ClanId = 2,
                    ClanName = "Bravo",
                    ClanDescription = "B",
                    ClanType = "Competitive",
                    NumberOfMembers = 20,
                    ClanStreak = 5
                },
                new Clan
                {
                    ClanId = 3,
                    ClanName = "Charlie",
                    ClanDescription = "C",
                    ClanType = "Mixed",
                    NumberOfMembers = 15,
                    ClanStreak = 1
                }
            );

            ctx.SaveChanges();
            return ctx;
        }

        [Fact]
        public void Create_Get_ReturnsView()
        {
            var ctx = BuildContextWithData();
            var controller = new ClansController(ctx);

            var result = controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_Post_Valid_RedirectsToIndex()
        {
            var ctx = BuildContextWithData();
            var controller = new ClansController(ctx);

            var newClan = new Clan
            {
                ClanName = "Delta",
                ClanDescription = "D",
                ClanType = "Casual",
                NumberOfMembers = 8,
                ClanStreak = 0
            };

            var result = controller.Create(newClan);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Create_Post_Invalid_ReturnsView()
        {
            var ctx = BuildContextWithData();
            var controller = new ClansController(ctx);

            // Force model validation failure
            controller.ModelState.AddModelError("ClanName", "Required");

            var newClan = new Clan
            {
                ClanName = "", // invalid
                ClanDescription = "X",
                ClanType = "Casual",
                NumberOfMembers = 5,
                ClanStreak = 0
            };

            var result = controller.Create(newClan);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Edit_Get_Found_ReturnsView()
        {
            var ctx = BuildContextWithData();
            var controller = new ClansController(ctx);

            var result = controller.Edit(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Edit_Get_NotFound_ReturnsNotFound()
        {
            var ctx = BuildContextWithData();
            var controller = new ClansController(ctx);

            var result = controller.Edit(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Post_Valid_RedirectsToIndex()
        {
            var ctx = BuildContextWithData();
            var controller = new ClansController(ctx);

            var updated = ctx.Clans.First(c => c.ClanId == 2);
            updated.ClanName = "Bravo+";

            var result = controller.Edit(updated);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Edit_Post_Invalid_ReturnsView()
        {
            var ctx = BuildContextWithData();
            var controller = new ClansController(ctx);

            var updated = ctx.Clans.First(c => c.ClanId == 3);
            controller.ModelState.AddModelError("ClanName", "Required");
            updated.ClanName = ""; // invalid

            var result = controller.Edit(updated);

            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public void Delete_Get_Found_ReturnsView()
        {
            var ctx = BuildContextWithData();
            var controller = new ClansController(ctx);

            var result = controller.Delete(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Delete_Get_NotFound_ReturnsNotFound()
        {
            var ctx = BuildContextWithData();
            var controller = new ClansController(ctx);

            var result = controller.Delete(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_Post_RedirectsToIndex()
        {
            var ctx = BuildContextWithData();
            var controller = new ClansController(ctx);

            var result = controller.DeleteConfirmed(2);

            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}

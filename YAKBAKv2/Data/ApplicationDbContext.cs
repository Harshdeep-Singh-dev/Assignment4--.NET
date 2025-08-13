using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace YAKBAKv2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<YAKBAKv2.Models.ClanMember> ClanMembers { get; set; } = default!;
        public DbSet<YAKBAKv2.Models.Clan> Clans { get; set; } = default!;
    }
}

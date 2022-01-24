using Ethereal_Onepage_Template_Asp.Net.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ethereal_Onepage_Template_Asp.Net.Data
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<CustomUser> customUsers { get; set; }
        public DbSet<Message> messages { get; set; }
        public DbSet<Service> services { get; set; }
        public DbSet<Settings> settings { get; set; }
        public DbSet<SosialMedia> sosialMedias { get; set; }
        public DbSet<Testimonal> testimonals { get; set; }
        public DbSet<Work> works { get; set; }
    }
}

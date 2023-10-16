using Microsoft.EntityFrameworkCore;
using PudgeSMomom.Models;

namespace PudgeSMomom.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

    }
}

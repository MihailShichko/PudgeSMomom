using Microsoft.EntityFrameworkCore;
using PudgeSMomom.Models;
using PudgeSMomom.Models.AdvertModels;

namespace PudgeSMomom.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //public DbSet<User> Users { get; set; }
        public DbSet<Advert> Adverts { get; set; }
    }
}

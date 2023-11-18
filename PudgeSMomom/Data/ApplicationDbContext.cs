using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PudgeSMomom.Models;
using PudgeSMomom.Models.AdvertModels;

namespace PudgeSMomom.Data
{
    public class ApplicationDbContext:IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Advert> Adverts { get; set; }
    }
}

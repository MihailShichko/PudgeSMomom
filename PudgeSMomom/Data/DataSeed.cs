using PudgeSMomom.Models.AdvertModels;
using System.Net;

namespace PudgeSMomom.Data
{
    public static class DataSeed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Adverts.Any())
                {
                    context.Adverts.AddRange(new List<Advert>()
                    {
                        new Advert()
                        {
                            Title = "Advert 1",
                            Image = "a",
                            Description = "description advert 1",
                        },
                        new Advert()
                        {
                            Title = "Advert 2",
                            Image = "a",
                            Description = "description advert 2",
                        },
                        new Advert()
                        {
                            Title = "Advert 3",
                            Image = "a",
                            Description = "description advert 3",
                        },
                        new Advert()
                        {
                            Title = "Advert 4",
                            Image = "a",
                            Description = "description advert 4",
                        },
                        new Advert()
                        {
                            Title = "Advert 5",
                            Image = "a",
                            Description = "description advert 5",
                        }
                    });
                    context.SaveChanges();
                }
               
            }
        }


    }
}

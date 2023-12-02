using Microsoft.AspNetCore.Identity;
using PudgeSMomom.Models;
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

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
               
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "admin@gmail.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        UserName = "MIHAIL VELESOV",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        steamId = "76561198264162501"
                    };
                    await userManager.CreateAsync(newAdminUser, "AdminTest123");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string UserEmail = "user@gmail.com";
                var appUser = await userManager.FindByEmailAsync(UserEmail);
                if (appUser == null)
                {
                    var newAppUser = new User()
                    {
                        UserName = "USER",
                        Email = UserEmail,
                        EmailConfirmed = true,
                        steamId = "000000000"
                    };
                    await userManager.CreateAsync(newAppUser, "UserTest123");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }

    }
}

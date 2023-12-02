using AspNet.Security.OpenId.Steam;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using PudgeSMomom.Data;
using PudgeSMomom.Helpers;
using PudgeSMomom.Hubs;
//using PudgeSMomom.Migrations;
using PudgeSMomom.Models;
using PudgeSMomom.Services.Cloudinary;
using PudgeSMomom.Services.Repository.AdvertRepository;
using PudgeSMomom.Services.Repository.UserRepository;
using PudgeSMomom.Services.Steam;

namespace PudgeSMomom
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddSignalR();
            builder.Services.Configure<CloudinaryConfig>(builder.Configuration.GetSection("CloudinarySettings"));
            builder.Services.Configure<SteamConfig>(builder.Configuration.GetSection("SteamAPISettings"));
            builder.Services.AddScoped<IAdvertRepository, AdvertRepository>();
            builder.Services.AddScoped<IPhotoService, PhotoService>();
            builder.Services.AddScoped<ISteamService, SteamService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
            /*builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = SteamAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddSteam(options =>
            {
                options.ApplicationKey = "9CF743B10F3FE10AC9D37133571407DA";
            });*/
            
            var app = builder.Build();
            
            if (args.Length == 1 && args[0].ToLower() == "seeddata")
            {
                //DataSeed.SeedData(app);
                await DataSeed.SeedUsersAndRolesAsync(app);
            }

            //Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.MapHub<ChatHub>("/Chat/ChatRoom");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            
            app.Run();
        }
    }
}
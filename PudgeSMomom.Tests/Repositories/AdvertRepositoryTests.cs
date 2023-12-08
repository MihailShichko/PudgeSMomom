using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PudgeSMomom.Data;
using PudgeSMomom.Models.AdvertModels;
using PudgeSMomom.Services.Repository.AdvertRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PudgeSMomom.Tests.Repositories
{
    public class AdvertRepositoryTests
    {
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureCreated();
            if(await dbContext.Adverts.CountAsync() > 0)
            {
                for (int i = 0; i < 10; i++) {
                    dbContext.Add(new Advert()
                    {
                        Title = "Advert 1",
                        Image = "a",
                        Description = "description advert 1"
                    });

                    await dbContext.SaveChangesAsync();
                }
            }

            return dbContext;
        }

        [Fact]
        public async void AdvertRepository_Add_ReturnsBool()
        {
            #region Arrange
            var advert = new Advert()
            {
                Title = "1",
                Image = "1",
                Description = "1"
            };

            var dbContext = await GetDbContext();
            var adverts = new AdvertRepository(dbContext);
            #endregion

            #region Act
            var result = adverts.AddAdvert(advert);
            #endregion

            #region Assert
            result.Should().BeTrue();
            #endregion
        }
    }
}

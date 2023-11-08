using Microsoft.EntityFrameworkCore;
using PudgeSMomom.Data;
using PudgeSMomom.Models.AdvertModels;

namespace PudgeSMomom.Services.Repository.AdvertRepository
{
    public class AdvertRepository : IAdvertRepository
    {
        private ApplicationDbContext _dbContext;
        public AdvertRepository(ApplicationDbContext dbContext) 
        {
            this._dbContext = dbContext;
        }
        public bool AddAdvert(Advert advert)
        {
            _dbContext.Add(advert);
            return Save();
        }

        public bool DeleteAdvert(int id)
        {
            _dbContext.Remove(id);
            return Save();
        }

        public async Task<Advert> GetAdvertByIdAsync(int id)
        {
            return await _dbContext.Adverts.Include(advert => advert.User).FirstOrDefaultAsync(advert => advert.Id == id);
        }

        public async Task<IEnumerable<Advert>> GetAdverts()
        {
            return await _dbContext.Adverts.ToListAsync();
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public bool UpdateAdvert(Advert advertNew)
        {
            var advertOld = _dbContext.Adverts.FirstOrDefault(advert => advert.Id == advertNew.Id);
            advertOld.Title = advertNew.Title;
            advertOld.Description = advertNew.Description;
            advertOld.Image = advertNew.Image;
            return Save();
        }
    }
}

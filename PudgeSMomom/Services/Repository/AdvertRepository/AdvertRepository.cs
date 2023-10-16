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
            throw new NotImplementedException();
        }

        public Task<Advert> GetAdvertById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Advert>> GetAdverts()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UpdateAdvert(Advert advert)
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}

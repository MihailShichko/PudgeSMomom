using PudgeSMomom.Models.AdvertModels;

namespace PudgeSMomom.Services.Repository.AdvertRepository
{
    public interface IAdvertRepository
    {
        public Task<IEnumerable<Advert>> GetAdverts();
        public Task<Advert> GetAdvertById(int id);
        public Task<bool> AddAdvert(Advert advert);
        public Task<bool> UpdateAdvert(Advert advert);    
        public Task<bool> DeleteAdvert(int id);
        public bool Save();


    }
}

using PudgeSMomom.Models.AdvertModels;

namespace PudgeSMomom.Services.Repository.AdvertRepository
{
    public interface IAdvertRepository
    {
        public Task<IEnumerable<Advert>> GetAdverts();
        public Task<Advert> GetAdvertByIdAsync(int id);
        public bool AddAdvert(Advert advert);
        public bool UpdateAdvert(Advert advert);    
        public bool DeleteAdvert(int id);
        public bool Save();


    }
}

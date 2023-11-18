using PudgeSMomom.ViewModels.AccountVMs;

namespace PudgeSMomom.Services.Steam
{
    public interface ISteamService
    {
        public Task<AccountVM> GetPlayerData(string apiKey, long steamId);
        
    }
}

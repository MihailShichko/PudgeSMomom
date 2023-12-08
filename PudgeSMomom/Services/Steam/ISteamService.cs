using PudgeSMomom.Models;
using PudgeSMomom.ViewModels.AccountVMs;

namespace PudgeSMomom.Services.Steam
{
    public interface ISteamService
    {
        public Task<ProfileData> GetPlayerData(string apiKey, long steamId);
        
    }
}

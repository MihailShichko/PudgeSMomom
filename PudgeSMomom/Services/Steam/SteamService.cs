using AutoMapper;
using Humanizer;
using NuGet.Protocol;
using OpenDotaDotNet;
using OpenDotaDotNet.Endpoints;
using PudgeSMomom.ViewModels.AccountVMs;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Mappings;
using SteamWebAPI2.Utilities;

namespace PudgeSMomom.Services.Steam
{
    public class SteamService : ISteamService
    {
        public async Task<AccountVM> GetPlayerData(string apiKey, long steamId)
        {      
            var openDota = new OpenDotaApi();
            var player = await openDota.Players.GetPlayerByIdAsync(steamId);
            var matches = await openDota.Players.GetPlayerMatchesAsync(steamId);
            
            var accountVM = new AccountVM()
            {
                Mmr = player.CompetitiveRank.ToString(),
                Name = player.Profile.Name,
                AvatarURL = player.Profile.Avatarfull.ToString(),
                ProfileURL = player.Profile.Profileurl.ToString(),
                lastMatches = matches.Take(5).ToList()
            };
            

            return accountVM;
        }


    }
}

using AutoMapper;
using CloudinaryDotNet;
using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            var player = await openDota.Players.GetPlayerByIdAsync(steamId);//ничо не получает
            var matches = await openDota.Players.GetPlayerMatchesAsync(steamId);//ничо не получает

            var accountVM = new AccountVM()
            {
                Mmr = player.CompetitiveRank.ToString(),
                Name = player.Profile.Name,
                AvatarURL = player.Profile.Avatarfull.ToString(),
                ProfileURL = player.Profile.Profileurl.ToString(),
            };
            var webInterfaceFactory = new SteamWebInterfaceFactory(apiKey);

            var steamInterface = webInterfaceFactory.CreateSteamWebInterface<SteamUser>(new HttpClient());

            var playerSummaryResponse = await steamInterface.GetPlayerSummaryAsync((ulong)steamId);
            var playerSummaryData = playerSummaryResponse.Data;
            var playerSummaryLastModified = playerSummaryResponse.LastModified;

            return accountVM;
        }

        static async Task<string> SendRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}

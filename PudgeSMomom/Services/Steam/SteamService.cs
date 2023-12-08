using AutoMapper;
using CloudinaryDotNet;
using Humanizer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol;
using OpenDotaDotNet;
using OpenDotaDotNet.Endpoints;
using PudgeSMomom.Models;
using PudgeSMomom.ViewModels.AccountVMs;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Mappings;
using SteamWebAPI2.Utilities;

namespace PudgeSMomom.Services.Steam
{
    public class SteamService : ISteamService
    {
        public async Task<ProfileData> GetPlayerData(string apiKey, long steamId)
        {
            var webInterfaceFactory = new SteamWebInterfaceFactory(apiKey);

            var steamInterface = webInterfaceFactory.CreateSteamWebInterface<SteamUser>(new HttpClient());

            var playerSummaryResponse = await steamInterface.GetPlayerSummaryAsync((ulong)steamId);
            var profile = new ProfileData
            {
                RealName = playerSummaryResponse.Data.RealName,
                AvatarUrl = playerSummaryResponse.Data.AvatarFullUrl,
                Nickname = playerSummaryResponse.Data.Nickname,
                CountryCode = playerSummaryResponse.Data.CountryCode
            };

            return profile;
        }

    }

    
}

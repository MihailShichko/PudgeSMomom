using OpenDotaDotNet.Models.Players;

namespace PudgeSMomom.ViewModels.AccountVMs
{
    public class AccountVM
    {
        public string? AvatarURL { get; set; } 
        public string? ProfileURL { get; set; }
        public string? Mmr { get;set; }
        public string? Name { get; set; }
        public List<PlayerMatch> lastMatches { get; set; }
    }
}

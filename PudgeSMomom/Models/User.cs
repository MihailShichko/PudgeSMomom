using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PudgeSMomom.Models
{
    public class User:IdentityUser
    {
        public string? steamId { get; set; }
    }
}

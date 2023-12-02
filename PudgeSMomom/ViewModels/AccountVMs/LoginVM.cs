using System.ComponentModel.DataAnnotations;

namespace PudgeSMomom.ViewModels.AccountVMs
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Login is required")]
        [Display(Name = "Login")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required(ErrorMessage = "SteamId is required")]
        //[DataType(DataType.Text)]
        //public string SteamId { get; set; }
    }
}

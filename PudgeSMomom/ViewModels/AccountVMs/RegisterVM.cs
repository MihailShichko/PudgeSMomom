using System.ComponentModel.DataAnnotations;

namespace PudgeSMomom.ViewModels.AccountVMs
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "Confitm Password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Pasword do not match")]
        public string? ConfirmPassword  { get; set; }

        [Display(Name = "Steam Id")]
        [Required(ErrorMessage = "Steam Id is required")]
        public string? SteamId { get; set; }

    }
}

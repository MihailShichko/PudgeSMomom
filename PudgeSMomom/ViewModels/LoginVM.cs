using System.ComponentModel.DataAnnotations;

namespace PudgeSMomom.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Email Address")]
        public string UserName { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

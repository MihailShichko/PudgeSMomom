using System.ComponentModel.DataAnnotations;

namespace PudgeSMomom.Models
{
    public class User
    {
        [Key]
        public string? Id { get; set; }
        public string? Name { get; set; }

    }
}

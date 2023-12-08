using System.ComponentModel.DataAnnotations;

namespace PudgeSMomom.Models
{
    public class UserMessage
    {
        [Key]
        public int Id { get; set; }

        public string Data { get; set; }
    }
}

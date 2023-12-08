using Microsoft.AspNet.SignalR.Messaging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace PudgeSMomom.Models
{
    public class Dialogue
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string InitiatiorId { get; set; }

        [ForeignKey("User")]
        public string RecieverId { get; set; }
        public List<UserMessage> Messages{ get; set; }
    }
}

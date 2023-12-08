using PudgeSMomom.Models;
using PudgeSMomom.Models.AdvertModels;

namespace PudgeSMomom.Services.Repository.DialogueRepository
{
    public interface IDialogueRepository
    {
        public Task<IEnumerable<Dialogue>> GetDialogues();
        public Task<Dialogue> GetDialoguesByIdAsync(int id);
        public bool Contains(string InitiatorId, string ReceiverId);
        public bool AddDialogue(Dialogue dialogue);
        public bool UpdateDialogue(Dialogue dialogueNew);
        public bool DeleteDialogue(int id);
        public bool Save();
    }
}

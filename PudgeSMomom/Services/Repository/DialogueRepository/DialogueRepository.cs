using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PudgeSMomom.Data;
using PudgeSMomom.Models;

namespace PudgeSMomom.Services.Repository.DialogueRepository
{
    public class DialogueRepository : IDialogueRepository
    {
        private readonly ApplicationDbContext _context;
        public DialogueRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public bool AddDialogue(Dialogue dialogue)
        {
            _context.Dialogues.Add(dialogue);
            return Save();
        }

        public bool Contains(string InitiatorId, string ReceiverId)
        {
            return _context.Dialogues.Where(dialogue => dialogue.InitiatiorId == InitiatorId && dialogue.RecieverId == ReceiverId)
                .ToList()
                .Count != 0;
        }

        public bool DeleteDialogue(int id)
        {
            var dialogue = _context.Dialogues.FirstOrDefault(dial => dial.Id == id);
            _context.Dialogues.Remove(dialogue);
            return Save();
        }

        public async Task<IEnumerable<Dialogue>> GetDialogues()
        {
            return await _context.Dialogues.ToListAsync();
        }

        public async Task<Dialogue> GetDialoguesByIdAsync(int id)
        {
            return await _context.Dialogues.FirstOrDefaultAsync(dialogue => dialogue.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> UpdateDialogue(Dialogue dialogueNew)
        {
            var dialogueOld = await _context.Dialogues.FirstOrDefaultAsync(dialoge => dialogueNew.Id == dialoge.Id);
            dialogueOld.InitiatiorId = dialogueNew.InitiatiorId;
            dialogueOld.RecieverId = dialogueNew.InitiatiorId;
            dialogueOld.Messages.Clear();
            foreach(var dialogue in dialogueNew.Messages)
            {
                dialogueOld.Messages.Add(dialogue);
            }

            return this.Save();

        }

        bool IDialogueRepository.UpdateDialogue(Dialogue dialogueNew)
        {
            throw new NotImplementedException();
        }
    }
}

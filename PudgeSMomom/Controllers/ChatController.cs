using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PudgeSMomom.Models;
using PudgeSMomom.Services.Repository.DialogueRepository;

namespace PudgeSMomom.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {

        IDialogueRepository _dialogueRepository;
        private readonly UserManager<User> _userManager;
        public ChatController(IDialogueRepository dialogueRepository, UserManager<User> userManager)
        {
            _userManager = userManager;
            _dialogueRepository = dialogueRepository;
        }

        public IActionResult ChatRoom(Dialogue dialogue)
        {
            return View(dialogue);
        }
        public async Task<IActionResult> DeleteDialogue(int id)
        {
            _dialogueRepository.DeleteDialogue(id);
            return RedirectToAction("ChatBox");
        }
        public async Task<IActionResult> Contact(string SourceId, string DestinationId)
        {
            Dialogue dialogue;

            if(_dialogueRepository.Contains(SourceId, DestinationId))
            {
                var dialogues = await _dialogueRepository.GetDialogues();
                dialogue = dialogues.FirstOrDefault(dial => dial.InitiatiorId == SourceId && dial.RecieverId == DestinationId);
                if(dialogue.Messages == null)
                {
                    dialogue.Messages = new List<UserMessage>();
                }
            }
            else
            {
                dialogue = new Dialogue
                {
                    InitiatiorId = SourceId,
                    RecieverId = DestinationId,
                    Messages = new List<UserMessage>()
                };

                _dialogueRepository.AddDialogue(dialogue);
            }

            return RedirectToAction("ChatRoom", dialogue);
        }

        public async Task<ActionResult> ChatBox() 
        {
            var user = await _userManager.GetUserAsync(User);
            var dialogues = await _dialogueRepository.GetDialogues();
            return View(dialogues.Where(dialogue => dialogue.InitiatiorId == user.Id || dialogue.RecieverId == user.Id).ToList());
        }
    }
}

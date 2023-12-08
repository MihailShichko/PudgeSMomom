using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;
using PudgeSMomom.Models;
using PudgeSMomom.Services.Repository.DialogueRepository;

namespace PudgeSMomom.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {

        IDialogueRepository _dialogueRepository;
        public ChatController(IDialogueRepository dialogueRepository)
        {
            _dialogueRepository = dialogueRepository;
        }

        public IActionResult ChatRoom(Dialogue dialogue)
        {
            return View(dialogue);
        }

        public async Task<IActionResult> Contact(string SourceId, string DestinationId)
        {

            Dialogue dialogue;

            if(_dialogueRepository.Contains(SourceId, DestinationId))
            {
                var dialogues = await _dialogueRepository.GetDialogues();
                dialogue = dialogues.FirstOrDefault(dial => dial.InitiatiorId == SourceId && dial.RecieverId == DestinationId);
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
    }
}

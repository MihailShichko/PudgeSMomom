using Microsoft.AspNetCore.Mvc;

namespace PudgeSMomom.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult ChatRoom()
        {
            return View();
        }
    }
}

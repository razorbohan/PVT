using Microsoft.AspNetCore.Mvc;
using L6_P2_4_TagHelper.Logic;

namespace L6_P2_4_TagHelper.Controllers
{
    public class HomeController : Controller
    {
        public IPartyService PartyService { get; set; }

        public HomeController(IPartyService partyService)
        {
            PartyService = partyService;
        }

        public ActionResult Index()
        {
            var parties = PartyService.GetIncomingParties();

            return View("Index", parties);
        }
    }
}

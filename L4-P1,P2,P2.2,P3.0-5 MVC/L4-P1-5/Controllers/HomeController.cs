using System.Web.Mvc;
using L4_P1_5.Logic;

namespace L4_P1_5.Controllers
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
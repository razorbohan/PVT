using L6_P2_4_TagHelper.Filters;
using Microsoft.AspNetCore.Mvc;
using L6_P2_4_TagHelper.Logic;
using Microsoft.AspNetCore.Authorization;

namespace L6_P2_4_TagHelper.Controllers
{
    public class HomeController : Controller
    {
        public IPartyService PartyService { get; set; }

        public HomeController(IPartyService partyService)
        {
            PartyService = partyService;
        }

        [Authorize]
        [CustomCache(30)]
        public ActionResult Index()
        {
            var parties = PartyService.GetIncomingParties();

            return View("Index", parties);
        }

        [Authorize(Policy = "Plus18")]
        public ActionResult Adult()
        {
            var parties = PartyService.GetAdultParties();

            return View("Adult", parties);
        }
    }
}

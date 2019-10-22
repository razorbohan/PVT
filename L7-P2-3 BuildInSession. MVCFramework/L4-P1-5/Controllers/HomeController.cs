using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using L4_P1_5.Logic;
using L4_P1_5.Models;

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

        [ChildActionOnly]
        public ActionResult GetLastViewedParties()
        {
            if (Session["last_viewed_parties"] is Queue<int> lastParties)
            {
                var parties = PartyService.GetIncomingParties().Where(x => lastParties.Contains(x.Id));

                return PartialView("_GetLastViewedParties", parties);
            }

            return PartialView("_GetLastViewedParties", new List<Party>());
        }
    }
}
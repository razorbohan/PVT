using System.Linq;
using System.Web.Mvc;
using L4_P1_5.Logic;
using L4_P1_5.ViewModel;

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
            var model = parties.Select(x => new PartyViewModel { Id = x.Id, Date = x.Date, Title = x.Title });

            return View("Index", model);
        }
    }
}
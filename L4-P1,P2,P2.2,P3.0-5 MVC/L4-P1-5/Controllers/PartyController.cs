using System.Linq;
using System.Web.Mvc;
using L4_P1_5.Infrastructure;
using L4_P1_5.Logic;
using L4_P1_5.ViewModel;

namespace L4_P1_5.Controllers
{
    public class PartyController : Controller
    {
        private IPartyService PartyService { get; }
        private ILogger Logger { get; }

        public PartyController(IPartyService partyService, ILogger logger)
        {
            PartyService = partyService;
            Logger = logger;
        }

        public ActionResult Index(int id)
        {
            var party = PartyService.GetParty(id);
            var attendants = PartyService.ListAttendants(party.Id).Select(x => x.Name).ToList();
            var model = new PartyDetailsViewModel { PartyId = party.Id, Date = party.Date, Title = party.Title, Location = party.Location, Attendants = attendants };

            return View(model);
        }

        [HttpPost]
        public ActionResult Vote(int partyId, string name, string isAttend)
        {
            PartyService.Vote(partyId, name, isAttend == "on");

            Logger.Info($"{name} -> {PartyService.GetParty(partyId).Title} : {isAttend}");

            return RedirectToAction("Index", new { id = partyId });
        }

        //public ActionResult Participants()
        //{
        //    return View();
        //}
    }
}
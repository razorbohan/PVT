using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using L4_P1_5.Infrastructure;
using L4_P1_5.Logic;
using L4_P1_5.Models;
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
            var attendants = PartyService.ListAttendants(party.Id).Select(x => (x.Name, x.Photo)).ToList();
            var model = new PartyDetailsViewModel { PartyId = party.Id, Date = party.Date, Title = party.Title, Location = party.Location, Attendants = attendants };

            MemoriseParty(party);

            return View(model);
        }

        [HttpPost]
        public ActionResult Vote(VoteViewModel model, HttpPostedFileBase photo)
        {
            if (!string.IsNullOrEmpty(model.Name) && !string.IsNullOrEmpty(model.IsAttend) && photo != null)
            {
                var ext = System.IO.Path.GetExtension(photo.FileName);
                var filename = $@"{Guid.NewGuid()}{ext}";
                var path = System.IO.Path.Combine(Server.MapPath("~/Content/images"), filename);
                photo.SaveAs(path);

                PartyService.Vote(model.PartyId, model.Name, model.IsAttend == "on", filename);

                Logger.Info($"{model.Name} -> {PartyService.GetParty(model.PartyId).Title} : {model.IsAttend == "on"}");
            }

            return RedirectToAction("Index", new { id = model.PartyId });
        }

        private void MemoriseParty(Party party)
        {
            if (Session["last_viewed_parties"] is Queue<int> lastParties)
            {
                if (!lastParties.Contains(party.Id))
                {
                    if (lastParties.Count >= 5)
                        lastParties.Dequeue();

                    lastParties.Enqueue(party.Id);
                    Session["last_viewed_parties"] = lastParties;
                }
            }
            else
            {
                lastParties = new Queue<int>();
                lastParties.Enqueue(party.Id);
                Session["last_viewed_parties"] = lastParties;
            }
        }

        [ChildActionOnly]
        public ActionResult GetFirst10Parties()
        {
            var parties = PartyService.GetFirst10Parties();

            return PartialView("_GetFirst10Parties", parties);
        }
    }
}
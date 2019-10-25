using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using L6_P2_4_TagHelper.Infrastructure;
using L6_P2_4_TagHelper.Logic;
using L6_P2_4_TagHelper.Models;
using L6_P2_4_TagHelper.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace L6_P2_4_TagHelper.Controllers
{
    public class PartyController : Controller
    {
        private IPartyService PartyService { get; }
        private ILogger Logger { get; }
        private readonly IHostingEnvironment _env;

        public PartyController(IHostingEnvironment env, IPartyService partyService, ILogger logger)
        {
            _env = env;
            PartyService = partyService;
            Logger = logger;
        }

        public ActionResult Index(int id)
        {
            var party = PartyService.GetParty(id);
            var attendants = PartyService.ListAttendants(party.Id).Select(x => (x.Name, x.Avatar)).ToList();
            var model = new PartyDetailsViewModel { PartyId = party.Id, Date = party.Date, Title = party.Name, Location = party.Location, Attendants = attendants };

            MemoriseParty(party);

            return View(model);
        }

        [HttpPost]
        public ActionResult Vote(VoteViewModel model, IFormFile photo)
        {
            if (!string.IsNullOrEmpty(model.Name) && !string.IsNullOrEmpty(model.IsAttend) && photo != null)
            {
                var ext = Path.GetExtension(photo.FileName);
                var filename = $@"{Guid.NewGuid()}{ext}";
                var path = Path.Combine(_env.WebRootPath, "images", filename);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }

                PartyService.Vote(model.PartyId, model.Name, model.IsAttend == "on", filename);

                Logger.Info($"{model.Name} -> {PartyService.GetParty(model.PartyId).Name} : {model.IsAttend == "on"}");
            }

            return RedirectToAction("Index", new { id = model.PartyId });
        }

        private void MemoriseParty(Party party)
        {
            if (HttpContext.Session.Keys.Contains("last_viewed_parties"))
            {
                var lastPartiesString = HttpContext.Session.GetString("last_viewed_parties");
                var lastParties = JsonConvert.DeserializeObject<Queue<int>>(lastPartiesString);

                if (!lastParties.Contains(party.Id))
                {
                    if (lastParties.Count >= 5)
                        lastParties.Dequeue();

                    lastParties.Enqueue(party.Id);
                    var json = JsonConvert.SerializeObject(lastParties);
                    HttpContext.Session.SetString("last_viewed_parties", json);
                }
            }
            else
            {
                var lastParties = new Queue<int>();

                lastParties.Enqueue(party.Id);
                var json = JsonConvert.SerializeObject(lastParties);
                HttpContext.Session.SetString("last_viewed_parties", json);
            }
        }
    }
}
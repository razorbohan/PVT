using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using L6_P2_4_TagHelper.Infrastructure;
using L6_P2_4_TagHelper.Logic;
using L6_P2_4_TagHelper.DAL.Models;
using L6_P2_4_TagHelper.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace L6_P2_4_TagHelper.Controllers
{
    [Authorize]
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

        [HttpGet]
        public ActionResult Index(int id)
        {
            var party = PartyService.GetParty(id);
            if (party.IsPlus18 && !HttpContext.User.Claims.Any(c => c.Type == "Plus18"))
            {
                return Forbid();
            }

            var attendants = PartyService.ListAttendants(party.Id).Select(x => (x.Name, x.Avatar)).ToList();
            var model = new PartyDetailsViewModel { PartyId = party.Id, Date = party.Date, Title = party.Name, Location = party.Location, Attendants = attendants };

            MemoriseParty(party);

            return View(model);
        }

        [HttpPost]
        public ActionResult Vote(VoteViewModel model, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Name) && model.IsAttend)
                {
                    string filename = null;
                    if (photo != null)
                    {
                        var ext = Path.GetExtension(photo.FileName);
                        filename = $@"{Guid.NewGuid()}{ext}";
                        var path = Path.Combine(_env.WebRootPath, "images", filename);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            photo.CopyTo(fileStream);
                        }
                    }

                    PartyService.Vote(model.PartyId, model.Name, model.IsAttend, filename);

                    Logger.Info($"{model.Name} -> {PartyService.GetParty(model.PartyId).Name} : {model.IsAttend}");
                }

                return RedirectToAction("Index", new { id = model.PartyId });
            }

            var party = PartyService.GetParty(model.PartyId);
            var attendants = PartyService.ListAttendants(party.Id).Select(x => (x.Name, x.Avatar)).ToList();
            var partyModel = new PartyDetailsViewModel { PartyId = party.Id, Date = party.Date, Title = party.Name, Location = party.Location, Attendants = attendants };

            return View("Index", partyModel);
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
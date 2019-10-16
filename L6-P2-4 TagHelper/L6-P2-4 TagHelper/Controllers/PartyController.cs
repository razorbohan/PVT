using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using L6_P2_4_TagHelper.Infrastructure;
using L6_P2_4_TagHelper.Logic;
using L6_P2_4_TagHelper.Models;
using L6_P2_4_TagHelper.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

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
            var attendants = PartyService.ListAttendants(party.Id).Select(x => (x.Name, x.Photo)).ToList();
            var model = new PartyDetailsViewModel { PartyId = party.Id, Date = party.Date, Title = party.Title, Location = party.Location, Attendants = attendants };

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

                Logger.Info($"{model.Name} -> {PartyService.GetParty(model.PartyId).Title} : {model.IsAttend == "on"}");
            }

            return RedirectToAction("Index", new { id = model.PartyId });
        }

        //[ChildActionOnly]
        //public ActionResult GetFirst10Parties()
        //{
        //    var parties = PartyService.GetFirst10Parties();

        //    return PartialView("_GetFirst10Parties", parties);
        //}

        //public ActionResult Participants()
        //{
        //    return View();
        //}
    }
}
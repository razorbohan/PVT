using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using L4_P1_5.Infrastructure;
using L4_P1_5.Logic;

namespace L4_P1_5.Controllers
{
    public class PartyController : Controller
    {
        private IPartyService PartyService { get; set; }
        private ILogger Logger { get; set; }

        public PartyController(IPartyService partyService, ILogger logger)
        {
            PartyService = partyService;
            Logger = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Vote()
        {
            return View();
        }

        public ActionResult Participants()
        {
            return View();
        }
    }
}
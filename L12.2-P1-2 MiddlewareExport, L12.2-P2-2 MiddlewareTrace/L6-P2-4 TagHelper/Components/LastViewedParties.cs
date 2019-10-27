using System.Collections.Generic;
using System.Linq;
using L6_P2_4_TagHelper.Logic;
using L6_P2_4_TagHelper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace L6_P2_4_TagHelper.Components
{
    public class LastViewedParties : ViewComponent
    {
        private IPartyService PartyService { get; }

        public LastViewedParties(IPartyService partyService)
        {
            PartyService = partyService;
        }

        public IViewComponentResult Invoke()
        {
            var partiesString = HttpContext.Session.GetString("last_viewed_parties");
            if (partiesString != null)
            {
                var partiesIds = JsonConvert.DeserializeObject<Queue<int>>(partiesString);
                var parties = PartyService.GetIncomingParties().Where(x => partiesIds.Contains(x.Id));

                return View(parties);
            }

            return View(new List<Party>());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L6_P2_4_TagHelper.Logic;
using Microsoft.AspNetCore.Mvc;

namespace Memorizer.Web.Components
{
    public class First10PartiesViewComponent: ViewComponent
    {
        private IPartyService PartyService { get; }

        public First10PartiesViewComponent(IPartyService partyService)
        {
            PartyService = partyService;
        }

        public IViewComponentResult Invoke()
        {
            var parties = PartyService.GetFirst10Parties();

            return View(parties);
        }
    }
}

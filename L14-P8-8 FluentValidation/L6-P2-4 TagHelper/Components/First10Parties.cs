using L6_P2_4_TagHelper.Logic;
using Microsoft.AspNetCore.Mvc;

namespace L6_P2_4_TagHelper.Components
{
    public class First10Parties: ViewComponent
    {
        private IPartyService PartyService { get; }

        public First10Parties(IPartyService partyService)
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

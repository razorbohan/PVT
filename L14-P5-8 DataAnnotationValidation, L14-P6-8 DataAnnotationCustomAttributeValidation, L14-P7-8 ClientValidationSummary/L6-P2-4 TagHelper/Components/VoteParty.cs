using L6_P2_4_TagHelper.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace L6_P2_4_TagHelper.Components
{
    public class VoteParty : ViewComponent
    {
        public IViewComponentResult Invoke(int partId)
        {
            return View(new VoteViewModel { PartyId = partId });
        }
    }
}

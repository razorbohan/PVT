using L2._2_P2_2_RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace L2._2_P2_2_RazorPages.Pages
{
    public class VoteModel : PageModel
    {
        private IParticipantRepository Repository;

        public VoteModel(IParticipantRepository repository)
        {
            Repository = repository;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost(string name, string attend)
        {
            if (!string.IsNullOrEmpty(name) && attend == "on")
            {
                Repository.Add(new Participant(name));

                return new RedirectToPageResult("/Participants");
            }

            return new RedirectToPageResult("/Vote");
        }
    }
}

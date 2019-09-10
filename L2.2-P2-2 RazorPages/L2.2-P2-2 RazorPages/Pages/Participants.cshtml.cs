using System.Collections.Generic;
using L2._2_P2_2_RazorPages.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace L2._2_P2_2_RazorPages.Pages
{
    public class ParticipantsModel : PageModel
    {
        public List<Participant> Participants;

        public ParticipantsModel(IParticipantRepository repository)
        {
            Participants = repository.GetAll();
        }

        public void OnGet()
        {
            
        }
    }
}
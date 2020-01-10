using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L6_P2_4_TagHelper.Logic;
using L6_P2_4_TagHelper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace L6_P2_4_TagHelper.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        public IPartyService PartyService { get; set; }

        public ApiController(IPartyService partyService)
        {
            PartyService = partyService;
        }

        // GET: api/GetParties
        [HttpGet("GetParties")]
        public IEnumerable<Party> GetParties()
        {
            return PartyService.GetIncomingParties();
        }

        // GET: api/GetParty/5
        [HttpGet("GetParty/{id}")]
        public Party GetParty(int id)
        {
            return PartyService.GetParty(id);
        }

        // POST: api/DeleteParty
        [HttpPost("DeleteParty")]
        public ActionResult DeleteParty(int id)
        {
            PartyService.DeleteParty(id);

            return Ok();
        }

        // POST: api/AddParty
        [HttpPost("AddParty")]
        public ActionResult AddParty(Party party)
        {
            PartyService.AddParty(party);

            return Ok();
        }

        // POST: api/UpdateParty
        [HttpPost("UpdateParty")]
        public ActionResult UpdateParty(Party party)
        {
            PartyService.UpdateParty(party);

            return Ok();
        }

        // GET: api/GetParticipants/5
        [HttpGet("GetParticipants/{id}")]
        public IEnumerable<Participant> GetParticipants(int id)
        {
            return PartyService.ListAttendants(id);
        }

        // POST: api/Vote
        [HttpPost("Vote")]
        public ActionResult Vote(int partyId, string name, bool isAttend, string photo)
        {
            PartyService.Vote(partyId, name, isAttend, photo);

            return Ok();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L6_P2_4_TagHelper.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using L6_P2_4_TagHelper.DAL.Models;

namespace L6_P2_4_TagHelper.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        public IPartyService PartyService { get; set; }
        private SignInManager<User> SignInManager { get; }
        private UserManager<User> UserManager { get; }
        private IOptions<JwtAuthentication> JwtAuthentication { get; }

        public ApiController(IPartyService partyService, SignInManager<User> signInManager, UserManager<User> userManager, IOptions<JwtAuthentication> jwtAuthentication)
        {
            PartyService = partyService;
            SignInManager = signInManager;
            UserManager = userManager;
            JwtAuthentication = jwtAuthentication;
        }

        // POST: api/Authenticate
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(string login, string password)
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception("Wrong login data!");

                var result = await SignInManager.PasswordSignInAsync(login, password, false, false);
                if (!result.Succeeded) throw new Exception("Failed to authenticate!");

                var user = await UserManager.FindByEmailAsync(login);

                var tokenString = GenerateToken(user);
                return Ok(new { Username = user.UserName, Token = tokenString });
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        // GET: api/GetParties
        [AllowAnonymous]
        [HttpGet("GetParties")]
        public IEnumerable<Party> GetParties()
        {
            return PartyService.GetIncomingParties();
        }

        // GET: api/GetParty/5
        [AllowAnonymous]
        [HttpGet("GetParty/{id}")]
        public Party GetParty(int id)
        {
            return PartyService.GetParty(id);
        }

        // POST: api/DeleteParty
        [HttpPost("DeleteParty")]
        public IActionResult DeleteParty(int id)
        {
            PartyService.DeleteParty(id);

            return Ok();
        }

        // POST: api/AddParty
        [HttpPost("AddParty")]
        public IActionResult AddParty(Party party)
        {
            PartyService.AddParty(party);

            return Ok();
        }

        // POST: api/UpdateParty
        [HttpPost("UpdateParty")]
        public IActionResult UpdateParty(Party party)
        {
            PartyService.UpdateParty(party);

            return Ok();
        }

        // GET: api/GetParticipants/5
        [AllowAnonymous]
        [HttpGet("GetParticipants/{id}")]
        public IEnumerable<Participant> GetParticipants(int id)
        {
            return PartyService.ListAttendants(id);
        }

        // POST: api/Vote
        [AllowAnonymous]
        [HttpPost("Vote")]
        public IActionResult Vote(int partyId, string name, bool isAttend, string photo)
        {
            PartyService.Vote(partyId, name, isAttend, photo);

            return Ok();
        }

        private string GenerateToken(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = JwtAuthentication.Value.ValidIssuer,
                Audience = JwtAuthentication.Value.ValidAudience,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = JwtAuthentication.Value.SigningCredentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
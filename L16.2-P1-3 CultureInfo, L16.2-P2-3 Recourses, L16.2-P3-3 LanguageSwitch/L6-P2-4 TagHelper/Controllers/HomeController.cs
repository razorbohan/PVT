using L6_P2_4_TagHelper.Filters;
using Microsoft.AspNetCore.Mvc;
using L6_P2_4_TagHelper.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System;

namespace L6_P2_4_TagHelper.Controllers
{
    public class HomeController : Controller
    {
        public IPartyService PartyService { get; set; }

        public HomeController(IPartyService partyService)
        {
            PartyService = partyService;
        }

        [Authorize]
        [CustomCache(30)]
        public IActionResult Index()
        {
            var parties = PartyService.GetIncomingParties();

            return View("Index", parties);
        }

        [Authorize(Policy = "Plus18")]
        public IActionResult Adult()
        {
            var parties = PartyService.GetAdultParties();

            return View("Adult", parties);
        }

        [HttpGet]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}

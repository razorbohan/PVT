using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITNews.Logic;
using ITNews.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITNews.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        public INewsService NewsService { get; set; }
        private SignInManager<ApplicationUser> SignInManager { get; }
        private UserManager<ApplicationUser> UserManager { get; }

        public ApiController(INewsService newsService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            NewsService = newsService;
            SignInManager = signInManager;
            UserManager = userManager;
        }

        // GET: api/GetNews
        [AllowAnonymous]
        [HttpGet("GetNews")]
        public IEnumerable<News> GetNews()
        {
            return NewsService.GetAllNews();
        }

        // GET: api/GetNews/5
        [AllowAnonymous]
        [HttpGet("GetNews/{id}")]
        public News GetNews(int id)
        {
            return NewsService.GetNews(id);
        }

        // POST: api/DeleteNews
        [HttpPost("DeleteNews")]
        public IActionResult DeleteNews(int id)
        {
            NewsService.DeleteNews(id);

            return Ok();
        }

        // POST: api/AddNews
        [HttpPost("AddNews")]
        public IActionResult AddNews(News news)
        {
            NewsService.AddNews(news);

            return Ok();
        }

        // POST: api/UpdateNews
        [HttpPost("UpdateNews")]
        public IActionResult UpdateNews(News news)
        {
            NewsService.UpdateNews(news);

            return Ok();
        }
    }
}

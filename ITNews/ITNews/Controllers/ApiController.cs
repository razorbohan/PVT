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

        public ApiController(INewsService newsService)
        {
            NewsService = newsService;
        }

        // GET: api/GetNews
        [AllowAnonymous]
        [HttpGet("GetNews")]
        public IEnumerable<News> GetNews()
        {
            return NewsService.GetAllNews();
        }

        // GET: api/GetNewsByCategory/java
        [AllowAnonymous]
        [HttpGet("GetNewsByCategory/{category}")]
        public IEnumerable<News> GetNewsByCategory(string category)
        {
            return NewsService.GetNewsByCategory(category);
        }

        // GET: api/GetNewsByTag/programming
        [AllowAnonymous]
        [HttpGet("GetNewsByTag/{tag}")]
        public IEnumerable<News> GetNewsByTag(string tag)
        {
            return NewsService.GetNewsByTag(tag);
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

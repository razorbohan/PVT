using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITNews.Logic;
using ITNews.Models;
using ITNews.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITNews.Controllers
{
    [Authorize(Roles = "Administrator")]
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

        // GET: api/GetNewsByDate/21-01-2020:14-10-33
        [AllowAnonymous]
        [HttpGet("GetNewsByDate/{date}")]
        public IEnumerable<News> GetNewsByDate(DateTime date)
        {
            return NewsService.GetNewsByDate(date);
        }

        // GET: api/SearchNews/php
        [AllowAnonymous]
        [HttpGet("SearchNews/{search}")]
        public IEnumerable<News> GetNews(string search)
        {
            return NewsService.SearchNews(search);
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
        public IActionResult DeleteNews([FromBody] int id)
        {
            NewsService.DeleteNews(id);

            return Ok();
        }

        // POST: api/AddNews
        [HttpPost("AddNews")]
        public IActionResult AddNews(NewsViewModel newsModel)
        {
            var category = NewsService.GetCategories().FirstOrDefault(x => x.Name == newsModel.Category);
            var tags = NewsService.GetTags().Where(x => newsModel.Tags.Contains(x.Name)).ToList();

            var news = new News
            {
                Name = newsModel.Name,
                //Created = newsModel.Created,
                ShortDescription = newsModel.ShortDescription,
                Description = newsModel.Description,
                Category = category,
                Tags = new List<NewsTags>()
            };

            tags.ForEach(tag => news.Tags.Add(new NewsTags
            {
                News = news,
                Tag = tag
            }));

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

        // GET: api/GetTags
        [AllowAnonymous]
        [HttpGet("GetTags")]
        public IEnumerable<Tag> GetTags()
        {
            return NewsService.GetTags();
        }

        // GET: api/GetCategories
        [AllowAnonymous]
        [HttpGet("GetCategories")]
        public IEnumerable<Category> GetCategories()
        {
            return NewsService.GetCategories();
        }

        // GET: api/GetNewsComments/2
        [AllowAnonymous]
        [HttpGet("GetNewsComments/{id}")]
        public IEnumerable<Comment> GetNewsComments(int id)
        {
            return NewsService.GetNewsComments(id);
        }

        // POST: api/AddNewsComments
        [AllowAnonymous]
        [HttpPost("AddNewsComments")]
        public IActionResult AddNewsComments(CommentViewModel commentModel)
        {
            var news = NewsService.GetNews(commentModel.NewsId);

            var comment = new Comment
            {
                Body = commentModel.Body,
                UserId = commentModel.UserId,
                News = news
            };

            //news.Comments.Add(comment);

            NewsService.AddNewsComments(comment);

            return Ok();
        }
    }
}

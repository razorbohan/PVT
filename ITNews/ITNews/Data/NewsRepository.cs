using ITNews.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITNews.Data
{
    public interface INewsRepository
    {
        List<News> GetAll();
        News Get(int id);
        void Create(News news);
        void Delete(int id);
        void Edit(News news);

        List<Category> GetAllCategories();
        List<Tag> GetAllTags();
        List<Comment> GetNewsComments(int id);
        void AddNewsComments(Comment comment);
    }

    public class NewsRepository : INewsRepository
    {
        private NewsContext Context { get; }

        public NewsRepository(NewsContext context)
        {
            Context = context;
        }

        public News Get(int id)
        {
            return GetAll().FirstOrDefault(news => news.Id == id);
        }

        public News GetByName(string name)
        {
            return Context.News.FirstOrDefault(news => news.Name == name);
        }

        public List<News> GetAll()
        {
            return Context.News
                .Include(x => x.Category)
                .Include(x => x.Tags)
                    .ThenInclude(x => x.Tag)
                .ToList();
        }

        public List<Category> GetAllCategories()
        {
            return Context.Categories.ToList();
        }

        public List<Tag> GetAllTags()
        {
            return Context.Tags
                 .Include(x => x.News)
                    .ThenInclude(x => x.News)
                        .ThenInclude(x => x.Category)
                 .ToList();
        }

        public List<Comment> GetNewsComments(int id)
        {
            return Context.Comments
                .Where(comment => comment.News.Id == id)
                .ToList();
        }

        public void AddNewsComments(Comment comment)
        {
            Context.Comments.Add(comment);
            Context.SaveChanges();
        }

        public void Create(News news)
        {
            Context.News.Add(news);
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var news = Get(id);

            Context.News.Remove(news);
            Context.SaveChanges();
        }

        public void Edit(News news)
        {
            Context.News.Update(news);
            Context.SaveChanges();
        }
    }
}

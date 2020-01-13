using ITNews.Models;
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
            return Context.News.FirstOrDefault(news => news.Id == id);
        }

        public News GetByName(string name)
        {
            return Context.News.FirstOrDefault(news => news.Name == name);
        }

        public List<News> GetAll()
        {
            return Context.News.ToList();
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

using ITNews.Data;
using ITNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITNews.Logic
{
    public interface INewsService
    {
        IEnumerable<News> GetAllNews();
        IEnumerable<News> GetNewsByCategory(string category);
        IEnumerable<News> GetNewsByTag(string tag);
        IEnumerable<News> GetNewsByDate(DateTime date);
        IEnumerable<News> SearchNews(string search);
        News GetNews(int newsId);
        void AddNews(News news);
        void UpdateNews(News news);
        void DeleteNews(int newsId);
        IEnumerable<Tag> GetTags();
        IEnumerable<Category> GetCategories();
        IEnumerable<Comment> GetNewsComments(int newsId);
        void AddNewsComments(Comment comment);
    }
    public class NewsService : INewsService
    {
        public INewsRepository NewsRepository { get; set; }

        public NewsService(INewsRepository newsRepository)
        {
            NewsRepository = newsRepository;
        }

        public IEnumerable<News> GetAllNews()
        {
            return NewsRepository.GetAll();
        }

        public IEnumerable<News> GetNewsByCategory(string category)
        {
            return NewsRepository.GetAll().Where(x => x.Category.Name == category);
        }

        public IEnumerable<News> GetNewsByTag(string tag)
        {
            return NewsRepository.GetAllTags()
                .First(x => x.Name == tag)
                .News
                .Select(x => x.News);
        }

        public IEnumerable<News> GetNewsByDate(DateTime date)
        {
            return NewsRepository.GetAll().Where(x => x.Created.Date == date.Date);
        }

        public IEnumerable<News> SearchNews(string search)
        {
            return NewsRepository.GetAll()
                .Where(x => x.Description.Contains(search));
        }

        public News GetNews(int newsId)
        {
            return NewsRepository.Get(newsId);
        }

        public void AddNews(News news)
        {
            NewsRepository.Create(news);
        }
        public void UpdateNews(News news)
        {
            NewsRepository.Edit(news);
        }

        public void DeleteNews(int newsId)
        {
            NewsRepository.Delete(newsId);
        }

        public IEnumerable<Tag> GetTags()
        {
            return NewsRepository.GetAllTags();
        }

        public IEnumerable<Category> GetCategories()
        {
            return NewsRepository.GetAllCategories();
        }

        public IEnumerable<Comment> GetNewsComments(int newsId)
        {
            return NewsRepository.GetNewsComments(newsId);
        }

        public void AddNewsComments(Comment comment)
        {
            NewsRepository.AddNewsComments(comment);
        }
    }
}

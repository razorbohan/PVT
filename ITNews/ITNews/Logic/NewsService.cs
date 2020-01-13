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
        List<News> GetAllNews();
        News GetNews(int newsId);
        void AddNews(News news);
        void UpdateNews(News news);
        void DeleteNews(int newsId);
    }
    public class NewsService : INewsService
    {
        public INewsRepository NewsRepository { get; set; }

        public NewsService(INewsRepository newsRepository)
        {
            NewsRepository = newsRepository;
        }

        public List<News> GetAllNews()
        {
            return NewsRepository.GetAll();
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
    }
}

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
        IEnumerable<News> SearchNews(string search);
        News GetNews(int newsId);
        void AddNews(News news);
        void UpdateNews(News news);
        void DeleteNews(int newsId);
        IEnumerable<Tag> GetTags();
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
            //            var categories = NewsRepository.GetAllCategories();
            //            var tags = NewsRepository.GetAllTags();

            //            var news = new News
            //            {
            //                Name = "JetBrains bringing iOS device support to Android Studio",
            //                Created = DateTime.Now,
            //                ShortDescription = "JetBrains plug-in for Android Studio will allow developers to run, test, and debug Kotlin on iOS devices and simulators ",
            //                Description = @"Kotlin language inventor JetBrains is developing a plug-in for the Android Studio IDE that will support the development of Kotlin applications for Apple iOS devices. Due to preview in 2020, the Android Studio plug-in will allow developers to run, test, and debug Kotlin code on iOS devices and simulators.

            //Android Studio is Google’s free development tool for building Android mobile applications. The JetBrains plug-in will allow Kotlin developers to target the rival iOS platform as well. Many companies already run Kotlin on their production iOS apps as well as on Android apps. By leveraging Kotlin skills across Android and iOS, and sharing Kotlin business logic between the platforms, mobile application developers can reduce training, development, and maintenance costs.
            //[ Keep up with the latest developments in software development, cloud computing, data analytics, and machine learning with the InfoWorld Daily newsletter ]

            //The Android Studio plug-in will use proprietary code from JetBrains’ IntelliJ development platform, so it will be closed-source. The plug-in will not provide language support for Swift and Objective-C. Some operations such as deployment to the Apple App Store might require running Apple’s Xcode IDE. 
            //[ Learn Java from beginning concepts to advanced design patterns in this comprehensive 12-part course! ]

            //Kotlin runs on iOS via Kotlin/Native technology, which combines an LLVM-based back-end and a native implementation of the Kotlin standard library. Kotlin/Native is designed to allow compilation where virtual machines are not desirable or possible, such as on iOS or embedded devices.

            //This story, ""JetBrains bringing iOS device support to Android Studio"" was originally published by InfoWorld."
            //            };
            //            news.Category = categories.First(x => x.Id == 3);
            //            news.NewsTags = new List<NewsTags>
            //            {
            //              new NewsTags {
            //                News = news,
            //                Tag = tags.First(x => x.Id == 1),
            //              }
            //            };

            //NewsRepository.Create(news);

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
    }
}

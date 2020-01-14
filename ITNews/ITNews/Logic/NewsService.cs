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
//            var categories = NewsRepository.GetAllCategories();
//            var tags = NewsRepository.GetAllTags();

//            var news = new News
//            {
//                Name = "Win-win: Open source .Net pays off for devs",
//                Created = DateTime.Now,
//                ShortDescription = "By open-sourcing .Net Core, Microsoft is reaping the rewards -- as are the developers. Two years ago Microsoft did the unthinkable: It declared it would open-source its .Net server-side cloud stack with the introduction of .Net Core. The announcement was surprising, thanks to Microsoft’s long-running feuds with open source projects, as well as its history of portraying open source as a threat to the software economy.",
//                Description = @"Two years ago Microsoft did the unthinkable: It declared it would open-source its .Net server-side cloud stack with the introduction of .Net Core. The announcement was surprising, thanks to Microsoft’s long-running feuds with open source projects, as well as its history of portraying open source as a threat to the software economy.

//But Microsoft may have seen an opportunity. Perhaps in an effort to sell more tools and cloud services, as well as to attract more developers to its platform, it might have to open up. Thus far, the move has paid off.
//[ Find out how to get ahead with our career development guide for developers. | The art of programming is changing rapidly. We help you navigate what’s hot in programming and what’s going cold. | Keep up with hot topics in programming with InfoWorld’s App Dev Report newsletter. ]

//Microsoft has positioned .Net Core as a means for taking .Net beyond Windows. The cross-platform version extends .Net’s reach to MacOS and Linux. .Net Core RC1 was first made available on GitHub in November 2015, with .Net Core hitting Version 1.0 status this past June. Developers are buying in, says Scott Hunter, Microsoft partner director program manager for .Net.

//“Forty percent of our .Net Core customers are brand-new developers to the platform, which is what we want with .Net Core,” Hunter says. “We want to bring new people in.”

//Thanks in considerable part to .Net Core, .Net has seen a 61 percent uptick in the number of developers engaged with the platform in the past year. There has been dramatic growth in GitHub activity related to .Net of late as well, Hunter noted during an online presentation in November.

//While .Net Core does not generate revenue directly for Microsoft, it still could positively impact the bottom line, says Rob Sanfilippo, analyst at Directions on Microsoft. “It could be argued that the technology generates indirect revenue by incenting the use of Azure services or Microsoft developer tools,” he explains.

//Programmers appear to be active and benefiting too.

//.Net coder and blogger Matt Warren calls open-source .Net a success. The data clearly shows significant community involvement across multiple Microsoft-owned GitHub repositories, he says.

//“The community has been creating issues and has been sending pull-requests—actually contributing code that has then been included into the product itself—for a sustained period of time and the level of contributions has grown,” Warren says. “I actively follow and take part in the discussions in some repositories—such as CoreCLR and .Net Core Lab. I see firsthand the community contributing.”

//The success of Microsoft’s move was never assured. And there have been periods of uncertainty, Warren says, with efforts that would have been done in private now moving to the open. He cites changes in tooling, including moving from project.json back to an MSBuild-based project system.

//“Communicating these changes wasn’t always done in the best way, and there were many people who invested time in something that is now out-of-date, so they were upset,” he says. But Microsoft has succeeded by going “all-in,” he adds. “What I mean is that they haven’t just made the source available and left it there; they’ve done their best to allow the community to take part.”

//Components of .Net Core include the ASP.Net Core framework, for building Web and cloud applications; the .Net Core runtime; and the .Net Entity Framework, for data access. ASP.Net as part of .Net Core Version 1.1 features response caching, improved Azure integration, and view recompilation. Hunter says .Net Core has been built for speed, noting it has been eight times faster than Node.js and three times faster than Go in some benchmarks.

//Microsoft’s recent release of Visual Studio for Mac could also bode well for .Net Core.

//“It’s the first time the Visual Studio IDE—not counting Visual Studio Code, which is different technology and arguably not an IDE—has been released on a non-Windows platform, and it’s based on Xamarin technology with a stress on .Net Core development,” Sanfillippo says. “This release could drive a further bit of momentum toward .Net Core.”

//Microsoft also recently made enhancements to .Net Core tools in the planned Visual Studio 2017 IDE, including simplifying the syntax for .Net Core project files.

//Perceptions of Microsoft have changed, thanks to open source .Net, says Warren. Developers “now see [Microsoft] as a lot more open and approachable.”

//Microsoft also has gained in community expertise, he adds.

//It’s a win-win thus far for sure."
//            };
//            news.Category = categories.First(x => x.Id == 1);
//            news.NewsTags = new List<NewsTags>
//            {
//              new NewsTags {
//                News = news,
//                Tag = tags.First(x => x.Id == 1),
//              }
//            };

//            NewsRepository.Create(news);

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

using System.Collections.Generic;

namespace ITNews.ViewModels
{
    public class NewsViewModel
    {
        public string Name { get; set; }
        //public DateTime Created { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public List<string> Tags { get; set; }
    }
}

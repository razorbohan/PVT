using ITNews.Models;
using System;
using System.Collections.Generic;

namespace ITNews.ViewModels
{
    public class NewsModel
    {
        public string Name { get; set; }
        //public DateTime Created { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public List<string> Tags { get; set; }
    }
}

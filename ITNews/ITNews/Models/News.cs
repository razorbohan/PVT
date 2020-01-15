using System;
using System.Collections.Generic;

namespace ITNews.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public List<NewsTags> Tags { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace ITNews.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<NewsTags> News { get; set; }
    }
}

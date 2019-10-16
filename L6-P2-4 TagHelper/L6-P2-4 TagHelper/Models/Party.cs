using System;

namespace L6_P2_4_TagHelper.Models
{
    public class Party
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }

        public Party() { }

        public Party(int id, string title, string location, DateTime date)
        {
            Id = id;
            Title = title;
            Location = location;
            Date = date;
        }
    }
}
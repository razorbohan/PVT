using System;
using System.Collections.Generic;

namespace L6_P2_4_TagHelper.Models
{
    public class Party
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }

        //public ICollection<Participant> Participants { get; set; }
    }
}

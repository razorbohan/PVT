using System;

namespace L6_P2_4_TagHelper.DAL.Models
{
    public class Party
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public bool IsPlus18 { get; set; }

        //public ICollection<Participant> Participants { get; set; }
    }
}

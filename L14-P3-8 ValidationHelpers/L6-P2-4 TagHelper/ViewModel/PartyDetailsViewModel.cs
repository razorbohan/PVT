using System;
using System.Collections.Generic;

namespace L6_P2_4_TagHelper.ViewModel
{
    public class PartyDetailsViewModel
    {
        public int PartyId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public List<(string Name, string Photo)> Attendants { get; set; }
    }
}
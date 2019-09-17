using System;
using System.Collections.Generic;

namespace L4_P1_5.ViewModel
{
    public class PartyDetailsViewModel
    {
        public int PartyId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public List<string> Attendants { get; set; }
    }
}
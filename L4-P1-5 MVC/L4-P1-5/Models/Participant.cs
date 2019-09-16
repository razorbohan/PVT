namespace L4_P1_5.Models
{
    public class Participant
    {
        public int PartyId { get; set; }
        public string Name { get; set; }
        public bool IsAttend { get; set; }
        public string Reason { get; set; }

        public Participant(int partyId, string name, bool isAttend, string reason)
        {
            PartyId = partyId;
            Name = name;
            IsAttend = isAttend;
            Reason = reason;
        }
    }
}

namespace L4_P1_5.Models
{
    public class Participant
    {
        public int PartyId { get; set; }
        public string Name { get; set; }
        public bool IsAttend { get; set; }

        public Participant(int partyId, string name, bool isAttend)
        {
            PartyId = partyId;
            Name = name;
            IsAttend = isAttend;
        }
    }
}

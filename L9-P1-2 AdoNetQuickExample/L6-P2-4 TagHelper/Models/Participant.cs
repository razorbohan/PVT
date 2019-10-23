namespace L6_P2_4_TagHelper.Models
{
    public class Participant
    {
        public int PartyId { get; set; }
        public string Name { get; set; }
        public bool IsAttend { get; set; }
        public string Photo { get; set; }

        public Participant(int partyId, string name, bool isAttend, string photo)
        {
            PartyId = partyId;
            Name = name;
            IsAttend = isAttend;
            Photo = photo;
        }
    }
}

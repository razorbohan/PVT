namespace L6_P2_4_TagHelper.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAttend { get; set; }
        public string Avatar { get; set; }
        public string Reason { get; set; }
        public int PartyId { get; set; }

        public Party Party { get; set; }
    }
}

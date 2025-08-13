namespace YAKBAKv2.Models
{
    public class Clan
    {
        public int ClanId { get; set; }

        public string ClanName { get; set; }

        public string ClanDescription { get; set; }

        public string ClanType { get; set; }

        public int NumberOfMembers { get; set; }

        public int ClanStreak { get; set; }

        //CHILD REFERENCE
        public List<ClanMember> ClanMember { get; set; } = new List<ClanMember>();


    }
}

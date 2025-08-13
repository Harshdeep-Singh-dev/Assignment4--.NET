using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using YAKBAKv2.Models;

namespace YAKBAKv2.Models
{
    public class ClanMember
    {
        [Key]
        public int MemberId { get; set; }

        public string MemberName { get; set; }

        public string Role { get; set; }

        public string TimeOfJoin { get; set; }

        // FOREIGN KEY
        public int ClanId { get; set; }

        // PARENT REFERENCE
        public Clan Clan { get; set; }
    }
}

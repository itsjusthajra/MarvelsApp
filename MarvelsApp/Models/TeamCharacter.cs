using System.ComponentModel.DataAnnotations.Schema;

namespace MarvelsApp.Models
{
    public class TeamCharacter
    {
        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public Team? Team { get; set; }

        public int CharacterId { get; set; }
        [ForeignKey("CharacterId")]
        public Character? Character { get; set; }
    }
}

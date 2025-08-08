using System.ComponentModel.DataAnnotations;

namespace MarvelsApp.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Team Name")]
        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        // Navigation property for many-to-many relationship
        public virtual ICollection<TeamCharacter> TeamCharacters { get; set; } = new List<TeamCharacter>();
    }
}

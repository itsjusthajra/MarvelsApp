using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarvelsApp.Models
{
    public class CharacterDto
    {
        [Required]
        [Display(Name = "Character Name")]
        public string Name { get; set; } = "";

        [Display(Name = "Real Name")]
        public string RealName { get; set; } = "";

        public string Alias { get; set; } = "";
        public string Gender { get; set; } = "";
        public string Species { get; set; } = "";
        public string Origin { get; set; } = "";

        // Category relationship (one-to-many: one character has one category)
        public string Category { get; set; } = "";
        

        [Display(Name = "First Appearance")]
        public string FirstAppearance { get; set; } = "";

        public string Creator { get; set; } = "";
        public string Description { get; set; } = "";

        [Display(Name = "Image URL")]
        public IFormFile? Image { get; set; } 
    }
}

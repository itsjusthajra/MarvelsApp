using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarvelsApp.Models
{
    public class Character
    {
        [Key]
        public int Id { get; set; }

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

        public String Category { get; set; } = "";
        public string FirstAppearance { get; set; } = "";

        public string Creator { get; set; } = "";
        public string Description { get; set; } = "";

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = "";
    }
}
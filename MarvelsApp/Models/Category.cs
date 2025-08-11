using System.ComponentModel.DataAnnotations;

namespace MarvelsApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = "";

        // Navigation property (one-to-many relationship)
        public ICollection<Character> Characters { get; set; } = new List<Character>();
    }
}


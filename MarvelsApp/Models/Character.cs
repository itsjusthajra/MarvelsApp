using MarvelsApp.Models;
using System.ComponentModel.DataAnnotations;

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

    // Foreign key
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    // Navigation property
    public Category Category { get; set; }

    public string FirstAppearance { get; set; } = "";
    public string Creator { get; set; } = "";
    public string? Description { get; set; } = "";

    [Display(Name = "Image URL")]
    public string ImageUrl { get; set; } = "";
}

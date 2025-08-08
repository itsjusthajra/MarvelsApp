namespace MarvelsApp.Models
{
    public class TeamWithCharactersViewModel
    {
        public string TeamName { get; set; }
        public List<Character> Characters { get; set; } = new();
    }
}

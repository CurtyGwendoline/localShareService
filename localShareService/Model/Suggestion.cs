namespace localShareService.Model
{
    public class Suggestion
    {
        public int SuggestionId { get; set; }
        public required string Name { get; set; }
        public required string Username { get; set; }
        public int UserPhoto { get; set; }
        public required string Room { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreationDate { get { return DateTime.Now; } }
        public int OfferId { get; set; }
        public required string UserId { get; set; }
    }
}

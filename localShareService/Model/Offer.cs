namespace localShareService.Model
{
    public enum Type
    {
        Pret,     // 0
        Achat,    // 1
        Service   // 2
    }

    public class Offer
    {
        public int OfferId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Image { get; set; }
        public Type Type { get; set; }
        public ICollection<Suggestion> Suggestions { get; set; } = new List<Suggestion>();

        public required string AzureId { get; set; }
    }
}
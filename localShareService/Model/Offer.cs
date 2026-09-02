namespace localShareService.Model
{
    public enum Type
    {
        Pret,
        Achat,
        Service
    }

    public class Offer
    {
       
        public int OfferId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Image { get; set; }
        public Type Type { get; set; }

        // Navigation properties
        public  User Owner { get; set; }
    }
}

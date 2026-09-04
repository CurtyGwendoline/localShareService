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
        public int UserId { get; set; }
        public User? Owner { get; set; } 
    }
}

//{
//  "offerId": 0,
//  "name": "Nom de l'offre",
//  "description": "Description détaillée de l'offre",
//  "image": "url_de_image",
//  "type": 1,
//  "userId": 1
//}

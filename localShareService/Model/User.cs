using System.ComponentModel.DataAnnotations;

namespace localShareService.Model
{
    public class User
    {
        [Key]
        public required string AzureId { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
    }
}
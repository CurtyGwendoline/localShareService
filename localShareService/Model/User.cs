namespace localShareService.Model
{
    public class User
    {
        public int UserId { get; set; }
        public required string AzureId { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
    }
}

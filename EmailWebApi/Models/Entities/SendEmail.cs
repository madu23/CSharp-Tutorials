namespace EmailWebApi.Models.Entities
{
    public class SendEmail
    {
        public int Id { get; set; }
        public required string to { get; set; }
        public string? subject { get; set; }
        public required string body { get; set; }

    }
}

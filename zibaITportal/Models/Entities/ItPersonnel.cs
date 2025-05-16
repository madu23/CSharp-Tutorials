namespace zibaITportal.Models.Entities
{
    public class ItPersonnel
    {
        public int Id { get; set; }
        public  required string  Name { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public required string Department { get; set; }
        public string? Position { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfJoining { get; set; }
        public  required string Address { get; set; }
       
    }
}

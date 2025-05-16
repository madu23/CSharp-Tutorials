namespace RtlEmployeePortal.Models.Entities
{
    public class employee
    {
        public int id {  get; set; }
        public required string name { get; set; }
        public required string Email { get; set; }
        public  string? phone { get; set; }
        public decimal salary { get; set; }
    }
}

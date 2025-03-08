using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Db;
public static class AppDb
{
    //public static Dictionary<int, Staff> StaffTable { get; set; } = new();
    public static Dictionary<int, MenuItem> MenuItemsTable { get; set; } = new Dictionary<int, MenuItem>();
    public static Dictionary<int, Staff> StaffTable { get; set; } = new Dictionary<int, Staff>();
    public static Dictionary<int, Customer> CustomerTable { get; set; } = new();
}
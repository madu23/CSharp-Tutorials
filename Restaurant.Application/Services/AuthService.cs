using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Services;
public class AuthService
{
    public Task<Staff> Login()
    {
        Console.WriteLine("Enter your login details");
        Console.WriteLine("Staff Id:");
        var staffId = Convert.ToInt16(Console.ReadLine());

        Console.WriteLine("Password:");
        var password = Console.ReadLine();
        var staff = AppDb.StaffTable.Where(_ => _.Value.StaffId == staffId && _.Value.Password == password).SingleOrDefault().Value;
        if (staff is null)
        {
            Console.WriteLine("Invalid login details");
            Login();
        }
        return Task.FromResult(staff!);
    }
}













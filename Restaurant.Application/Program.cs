using Restaurant.Application.Services;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, Welcome to Eke Tech Restaurant!");

        // Initialize services
        var startupTask = new StartupTask();
        var authService = new AuthService();
        var staffManagementService = new StaffManagementService();
        var menuManagementService = new MenuManagementService();

        // Seed admin record if not exists
        startupTask.SeedAdminRecord();

        // Authenticate user
        var staff = await authService.Login();
        if (staff is null) return;

        while (true)
        {
            if (staff.Designation == "System Admin")
            {
                // Just show 2 main options for admin as requested
                Console.WriteLine("\nSelect from the options below:");
                Console.WriteLine("1. Staff Management");
                Console.WriteLine("2. Restaurant Menu Management");
                Console.WriteLine("3. Exit");

                var mainSelection = Console.ReadLine();
                switch (mainSelection)
                {
                    case "1":
                        staffManagementService.ManageStaff();
                        break;
                    case "2":
                        menuManagementService.ManageRestaurantMenu();
                        break;
                    case "3":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid selection");
                        break;
                }
            }
            else
            {
                // Non-admin users will have different options
                Console.WriteLine("You don't have admin privileges to access these features.");
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                return;
            }
        }
    }
}

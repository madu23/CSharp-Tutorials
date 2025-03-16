using Restaurant.Application.Services;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, Welcome to Eke Tech Restaurant!");
        Console.WriteLine("=======================================");

        // Seed default admin data step 1
        var seedDbTask = new StartupTask();
        var result = await seedDbTask.SeedAdminRecord();
        if (result == false)
        {
            Console.WriteLine("Admin data was not successfully pre-created");
        }

        // Login Step 2
        var authService = new AuthService();
        var loginResult = await authService.Login();

        if (loginResult?.Designation == "System Admin")
        {
            // If logged in as System Admin, display the System Admin menu
            Console.WriteLine("You are logged in as a System Admin");
            Console.WriteLine($"Welcome {loginResult?.FirstName} {loginResult?.LastName}");
            Console.WriteLine($"Select a system menu from the list below");

            // Create a new instance of AppMenu object
            var appMenu = new AppMenu();
            var selectionTask = new SelectionTask();
            int mainMenuSelection;
            do
            {
                // Get the user's selection from the System Admin Main Menu
                mainMenuSelection = appMenu.GetMenuSelectionIndex("System Admin Main Menu");
                var continueLoop = await selectionTask.HandleSelection(mainMenuSelection);
                if (!continueLoop)
                {
                    Console.WriteLine("Exiting...");
                    return;
                }
            } while (true);
        }
        else
        {
            // If not logged in as System Admin, display an access denied message
            Console.WriteLine("You do not have access to system admin functions.");
        }
        Console.ReadLine();
    }
}

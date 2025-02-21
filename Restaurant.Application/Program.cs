using Restaurant.Application.Services;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, Welcome to Eke Tech Restaurant!");

        // seed default admin data step 1
        var seedDbTask = new StartupTask();
        var result = await seedDbTask.SeedAdminRecord();
        if (result == false)
        {
            Console.WriteLine("Admin data was not successfully pre-created");
        }

        // Login Step 2
        var authService = new AuthService();
        var loginResult = await authService.Login();

        Console.WriteLine($"Welcome {loginResult?.FirstName} {loginResult?.LastName}");
        Console.WriteLine($"Select a system menu from the list below");

        if (loginResult?.Designation == "System Admin")
        {
            var appMenu = new AppMenu();
            var selectionTask = new SelectionTask();
            int mainMenuSelection;
            do
            {
                mainMenuSelection = appMenu.GetMenuSelectionIndex("System Admin Main Menu");
                var continueLoop = await selectionTask.HandleSelection(mainMenuSelection);
                if (!continueLoop)
                {
                    Console.WriteLine("Exiting...");
                    return;
                }
            } while (true);
        }
    }
}

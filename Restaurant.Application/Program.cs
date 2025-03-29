using Restaurant.Application.Services;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, Welcome to Fola's Restaurant!");
        Console.WriteLine("=======================================");

        var seedDbTask = new StartupTask();
        var result = await seedDbTask.SeedAdminRecord();
        if (result == false)
        {
            Console.WriteLine("Admin data was not successfully pre-created");
        }

        var authService = new AuthService();
        var loginResult = await authService.Login();

        if (loginResult?.Designation == "System Admin")
        {
            Console.WriteLine("You are logged in as a System Admin");
            Console.WriteLine($"Welcome {loginResult?.FirstName} {loginResult?.LastName}");
            Console.WriteLine($"Select a system menu from the list below");

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
        else
        {
            Console.WriteLine("You do not have access to system admin functions.");
        }
        Console.ReadLine();
    }
}

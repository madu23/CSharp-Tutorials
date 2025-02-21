// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Restaurant.Application.Services;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application;

class Program
{
    //static void Main(string[] args)
    //{

    //}
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

        int menuCounter = 0;
        if (loginResult?.Designation == "System Admin")
        {
            Console.WriteLine("You are logged in as a System Admin");
            SysMenu sysMenu = new SysMenu();
            var menuHandler = new MenuSelectionHandler(sysMenu);
            while (true)
            {
                await sysMenu.DisplayMainMenu();
                int mainChoice = await sysMenu.GetMainMenuSelection();
                if (mainChoice == 3)
                {
                    Console.WriteLine("Exiting application... Goodbye!");
                    break;
                }
                string selectedMainMenu = sysMenu.systemMenu[mainChoice - 1];
                while (true)
                {
                    await sysMenu.DisplaySubMenu(selectedMainMenu);
                    int subChoice = await sysMenu.GetSubMenuSelection(selectedMainMenu);
                    if (
                        subChoice == -1
                        || sysMenu.subMenus[selectedMainMenu][subChoice - 1] == "Exit"
                    )
                        break;
                    Console.WriteLine(
                        $"You selected: {sysMenu.subMenus[selectedMainMenu][subChoice - 1]}"
                    );
                    if (selectedMainMenu == "Staff Management")
                        await menuHandler.HandleStaffManagement(subChoice);
                    else
                        Console.WriteLine("Invalid input! Please enter a valid option.");
                }
            }
        }
        else
            Console.WriteLine("You do not have access to system admin functions.");
        Console.ReadLine();
    }
}

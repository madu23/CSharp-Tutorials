using Restaurant.Application.Services;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Utilities;

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

        if (loginResult == null)
        {
            Console.WriteLine("Login failed. Exiting application.");
            return;
        }

        Console.WriteLine($"Welcome {loginResult?.FirstName} {loginResult?.LastName}");
        Console.WriteLine($"Select a system menu from the list below");

        if (loginResult?.Designation == "System Admin")
        {
            Console.WriteLine("You are logged in as a System Admin");
            // create a new instance of Menu object
            var menu = new Menu { Title = "" };
            // Build and display the the menu
            await menu.BuildSystemMenu(new Dictionary<string, List<string>>());
            // get user selection
            // if user menu selection has submenu, display it else call the handler
            while (true)
            {
                // Step 1: Display main menu
                var menuService = new DisplayMenuService(menu);
                await menuService.DisplayMainMenu();

                // Step 2: Capture user main selection
                var (menuIndex, menuTitle) = await menuService.GetMainMenuSelection();

                // Step 3: Handle menu selection
                var exitCommand = MenuType.Exit.ToString();
                if (string.Equals(menuTitle, exitCommand, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Exiting application...");
                    break;
                }

                await menuService.DisplaySubMenu(menuTitle);
                var (subMenuIndex, subMenuTitle) = await menuService.GetSubMenuSelection(menuTitle);

                if (string.Equals(subMenuTitle, exitCommand, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                // Step 4: Call the appropriate handler
                const string staffHandler = "Staff Management";
                const string restaurantHandler = "Restaurant Management";
                switch (menuTitle)
                {
                    case staffHandler:
                        var staffService = new StaffManagementService();
                        await staffService.HandleStaffManagement(subMenuIndex);
                        break;

                    case restaurantHandler:
                        new MenuSelectionHandler(menu, HandlerType.RestaurantHandler);
                        Console.WriteLine(
                            "Restaurant management functionality is under development."
                        );
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Returning to main menu...");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("You are not an admin! Logging out...");
        }
    }
}

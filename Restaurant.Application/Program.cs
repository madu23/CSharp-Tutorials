// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Restaurant.Application.Services;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;
using System.Diagnostics;

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

        if (loginResult?.Designation == "System Admin")
        {
            Console.WriteLine("You are logged in as a System Admin");
            // create a new instance of Menu object


            // Build and display the the menu


            // get user selection

            // if user menu selection has submenu, display it else call the handler



            SysMenu sysMenu = new SysMenu();
            var menuHandler = new MenuSelectionHandler(sysMenu);
            while (true)
            {
                // step 1: display main menu
                await sysMenu.DisplayMainMenu();
                var mainChoice = await sysMenu.GetMainMenuSelection();
                
                // step 2: capture user main selection and display submenu if the menu selected has submenu
                // if not, then check if the menu selected is "Exit". If the selection is exit, exit the app, else navigate to the appropriate page or action
                if (mainChoice.menuTitle == "Exit")
                {
                    Console.WriteLine("Exiting application... Goodbye!");
                    break;
                }
                else
                {
                    await sysMenu.DisplaySubMenu(mainChoice.menuTitle);
                }


                // select submenu

                var selectedMainMenu = sysMenu.SystemMenu[mainChoice.menuTitle];
                while (true)
                {
                    await sysMenu.DisplaySubMenu(selectedMainMenu);
                    int subChoice = await sysMenu.GetSubMenuSelection(selectedMainMenu);
                    if (
                        subChoice == -1
                        || sysMenu.subMenus[selectedMainMenu][subChoice - 1] == "ExiT"
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

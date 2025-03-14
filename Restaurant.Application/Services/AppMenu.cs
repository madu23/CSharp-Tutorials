using Restaurant.Domain.Entities;
using Restaurant.Domain.Db;

namespace Restaurant.Application.Services;


// <summary>
/// Manages the restaurant application's menu system and user interactions
/// </summary>
public class AppMenu
{
    private readonly Staff _staff;
    private readonly Menu _menu;
   
    public AppMenu(Staff staff)
    {
        _staff = staff;
        _menu = new Menu { Title = "Main Menu", TypeOfMenu = MenuType.MainMenu };
        InitializeMenuStructure();
    }

     /// <summary>
    /// Sets up the menu hierarchy using ResponseOptions for consistent indexing
    /// </summary>
    private void InitializeMenuStructure()
    {
        _menu.Submenus = new List<Menu>
        {
            new Menu 
            { 
                Title = "Staff Management",
                Index = ResponseOptions.MainMenu.StaffManagement - 1,
                TypeOfMenu = MenuType.SubMenu,
                Submenus = new List<Menu>
                {
                    new() { Title = "Create Staff", Index = ResponseOptions.StaffManagement.CreateStaff - 1 },
                    new() { Title = "View Staff", Index = ResponseOptions.StaffManagement.ViewStaff - 1 },
                    new() { Title = "Edit Staff", Index = ResponseOptions.StaffManagement.EditStaff - 1 },
                    new() { Title = "Back", Index = ResponseOptions.StaffManagement.Exit - 1, TypeOfMenu = MenuType.Exit }
                }
            },
            new Menu 
            { 
                Title = "Restaurant Management",
                Index = ResponseOptions.MainMenu.RestaurantManagement - 1,
                TypeOfMenu = MenuType.SubMenu,
                Submenus = new List<Menu>
                {
                    new() { Title = "Menu Setup", Index = ResponseOptions.RestaurantManagement.MenuSetup - 1 },
                    new() { Title = "Menu Item Setup", Index = ResponseOptions.RestaurantManagement.MenuItemSetup - 1 },
                    new() { Title = "Back", Index = ResponseOptions.RestaurantManagement.Exit - 1, TypeOfMenu = MenuType.Exit }
                }
            },
            new Menu { Title = "Exit", Index = ResponseOptions.MainMenu.Exit - 1, TypeOfMenu = MenuType.Exit }
        };
    }


    /// <summary>
    /// Entry point for menu system
    /// Handles main menu display and navigation
    /// </summary>

    public async Task<bool> DisplayMainMenu()
    {
        try
        {
            while (true)
            {
                // Display menu and get user choice

                Selection.DisplayMenu(_menu);
                var choice = Selection.GetUserChoice();
                // Validate input
                if (!choice.HasValue)
                {
                     // Process menu selection
                    Selection.ShowError("Invalid input");
                    continue;
                }

                var selectedMenu = Selection.GetSelectedMenuItem(_menu, choice.Value);
                if (selectedMenu == null)
                {
                    Selection.ShowError("Invalid menu option");
                    continue;
                }

                if (selectedMenu.TypeOfMenu == MenuType.Exit && await Selection.ConfirmExit())
                    return true;

                if (selectedMenu.Title == "Staff Management")
                    await Selection.HandleStaffOperations(selectedMenu);
                else if (selectedMenu.Title == "Restaurant Management")
                    await Selection.HandleRestaurantOperations(selectedMenu);
            }
        }
        catch (Exception ex)
        {
            Selection.ShowError($"An error occurred: {ex.Message}");
            return false;
        }
    }
}
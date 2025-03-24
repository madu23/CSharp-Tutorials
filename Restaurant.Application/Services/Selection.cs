using Restaurant.Domain.Entities;
using Restaurant.Domain.Db;

namespace Restaurant.Application.Services;

/// <summary>
/// Handles user interactions and menu operations for the restaurant management system
/// </summary>

public class Selection
{
  #region Menu Operations
  /// <summary>
    /// Handles staff management menu operations
    /// </summary>

    public static async Task HandleStaffOperations(Menu menu)
{
    while (true)
    {
        DisplayMenu(menu);
        var choice = GetUserChoice();

        if (!choice.HasValue)
        {
            ShowError("Invalid input");
            continue;
        }

        try
        {
            switch (choice.Value)
            {
                case ResponseOptions.StaffManagement.CreateStaff:
                    await HandleCreateStaff();
                    break;
                case ResponseOptions.StaffManagement.ViewStaff:
                    await HandleViewStaff();
                    break;
                case ResponseOptions.StaffManagement.EditStaff:
                    await HandleEditStaff();
                    break;
                case ResponseOptions.StaffManagement.Exit:
                    if (await ConfirmExit())
                        return;
                    break;
                default:
                    ShowError("Invalid option");
                    break;
            }
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }
}
/// <summary>
    /// Handles restaurant management menu operations
    /// </summary>
    public static async Task HandleRestaurantOperations(Menu menu)
{
    while (true)
    {
        DisplayMenu(menu);
        var choice = GetUserChoice();

        if (!choice.HasValue)
        {
            ShowError("Invalid input");
            continue;
        }

        try
        {
            switch (choice.Value)
            {
                case ResponseOptions.RestaurantManagement.MenuSetup:
                    await HandleMenuSetup();
                    break;
                case ResponseOptions.RestaurantManagement.MenuItemSetup:
                    await HandleMenuItemSetup();
                    break;
                case ResponseOptions.RestaurantManagement.Exit:
                    if (await ConfirmExit())
                        return;
                    break;
                default:
                    ShowError("Invalid option");
                    break;
            }
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }
}
     #endregion


     #region Staff Management
     
/// <summary>
    /// Creates a new staff member with validation
    /// </summary>
    private static async Task HandleCreateStaff()
    {
        Console.WriteLine("Enter staff details (staff id, first name, last name, designation, password)");
        var input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            ShowError("Invalid input");
            return;
        }

        var staffInfo = input.Split(',');
        if (staffInfo.Length != 5)
        {
            ShowError("Invalid number of parameters");
            return;
        }

        var newStaff = new Staff
        {
            StaffId = Convert.ToInt32(staffInfo[0]),
            FirstName = staffInfo[1].Trim(),
            LastName = staffInfo[2].Trim(),
            Designation = staffInfo[3].Trim(),
            Password = staffInfo[4].Trim()
        };

        newStaff.CreateStaff();
        await DisplayStaffList();
    }
/// <summary>
    /// Views details of an existing staff member
    /// </summary>
    private static async Task HandleViewStaff()
    {
        Console.WriteLine("Enter staff id to view:");
        if (!int.TryParse(Console.ReadLine(), out int staffId))
        {
            ShowError("Invalid staff ID");
            return;
        }

        var staff = new Staff().ViewStaff(staffId);
        await DisplayStaffDetails(staff);
    }
/// <summary>
    /// Updates details of an existing staff member
    /// </summary>
    private static async Task HandleEditStaff()
    {
        Console.WriteLine("Enter staff id to edit:");
        if (!int.TryParse(Console.ReadLine(), out int staffId))
        {
            ShowError("Invalid staff ID");
            return;
        }

        var existingStaff = new Staff().ViewStaff(staffId);
        await UpdateStaffDetails(existingStaff);
    }
/// <summary>
    /// Handles restaurant menu setup
    /// </summary>
    private static async Task HandleMenuSetup()
    {
        Console.WriteLine("Menu Setup - Feature coming soon");
        await Task.CompletedTask;
    }
/// <summary>
    /// Handles restaurant menu item setup
    /// </summary>
    private static async Task HandleMenuItemSetup()
    {
        Console.WriteLine("Menu Item Setup - Feature coming soon");
        await Task.CompletedTask;
    }
#endregion
    
    #region Display Methods
/// <summary>
    /// Displays menu options to the user
    /// </summary>
    public static void DisplayMenu(Menu menu)
{
    Console.Clear();
    Console.WriteLine($"\n{menu.Title}");
    Console.WriteLine("===================");
    
    var menuItems = menu.TypeOfMenu == MenuType.MainMenu ? menu.Submenus : menu.Submenus;
    foreach (var item in menuItems)
    {
        Console.WriteLine($"{item.Index + 1}. {item.Title}");
    }
    Console.WriteLine("\nEnter your choice:");
}
// <summary>
    /// Gets the user's menu choice with validation
    /// </summary>
    public static int? GetUserChoice()
    {
        return int.TryParse(Console.ReadLine(), out int choice) ? choice : null;
    }
/// <summary>
    /// Gets the selected menu item based on user choice
    /// </summary>
    public static Menu GetSelectedMenuItem(Menu currentMenu, int choice)
    {
        return currentMenu.Submenus.FirstOrDefault(m => m.Index == choice - 1);
    }
 /// <summary>
    /// Displays staff list in a formatted table
    /// </summary>
    private static async Task DisplayStaffList()
    {
        Console.WriteLine("\nStaff List");
        Console.WriteLine("===================");
        Console.WriteLine(String.Format("{0}\t {1}\t {2}\t {3}", "StaffId", "First Name", "Last Name", "Designation"));
        Console.WriteLine("========================================================================");
        
        var staffList = AppDb.StaffTable.Values.ToList();
        foreach (var staff in staffList)
        {
            Console.WriteLine($"{staff.StaffId,-8} {staff.FirstName,-15} {staff.LastName,-15} {staff.Designation,-15}");
        }
        
        await WaitForKey();
    }
/// <summary>
    /// Displays details of a specific staff member
    /// </summary
    private static async Task DisplayStaffDetails(Staff staff)
    {
        Console.WriteLine("\nStaff Details");
        Console.WriteLine("===================");
        Console.WriteLine(String.Format("{0}\t {1}\t {2}\t {3}", "StaffId", "First Name", "Last Name", "Designation"));
        Console.WriteLine("========================================================================");
        Console.WriteLine($"{staff.StaffId,-8} {staff.FirstName,-15} {staff.LastName,-15} {staff.Designation,-15}");
        
        await WaitForKey();
    }
 /// <summary>
    /// Updates staff member details with validation
    /// </summary>
    private static async Task UpdateStaffDetails(Staff existingStaff)
    {
        Console.WriteLine("Enter new staff details (first name, last name, designation, password)");
        Console.WriteLine($"Current values: {existingStaff.FirstName}, {existingStaff.LastName}, {existingStaff.Designation}");

        var input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            ShowError("Invalid input");
            return;
        }

        var updateInfo = input.Split(',');
        if (updateInfo.Length != 4)
        {
            ShowError("Invalid number of parameters");
            return;
        }

   
        var updatedStaff = new Staff
        {
            StaffId = existingStaff.StaffId,
            FirstName = updateInfo[0].Trim(),
            LastName = updateInfo[1].Trim(),
            Designation = updateInfo[2].Trim(),
            Password = updateInfo[3].Trim()
        };

        existingStaff.EditStaff(updatedStaff);
        Console.WriteLine("Staff updated successfully!");
        await WaitForKey();
    }
#endregion


    #region Helper Methods
 /// <summary>
    /// Displays error message and waits for user acknowledgment
    /// </summary>
    public static void ShowError(string message)
    {
        Console.WriteLine($"Error: {message}");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
/// <summary>
    /// Waits for user to press any key
    /// </summary>
    public static async Task WaitForKey()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        await Task.CompletedTask;
    }
  /// <summary>
    /// Confirms if user wants to exit current menu
    /// </summary>
    /// <returns>True if user confirms exit, false otherwise</returns
    public static async Task<bool> ConfirmExit()
    {
        Console.WriteLine("\nAre you sure you want to go back? (Y/N)");
        return Console.ReadLine()?.ToUpper() == "Y";
    }
     #endregion
}
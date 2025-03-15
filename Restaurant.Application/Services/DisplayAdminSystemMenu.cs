using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Services;

/// <summary>
/// Service to handle the display of system menus (main menu and submenus)
/// </summary>

public class DisplayMenuService
{
    private readonly Menu _menu;

    /// <summary>
    /// Initializes a new instance of DisplayMenuService with a given menu.
    /// </summary>
    public DisplayMenuService(Menu menu)
    {
        _menu = menu;
    }

    public async Task DisplayMainMenu()
    {
        Console.WriteLine("\nSelect a system menu from the list below:");
        Console.WriteLine($"==== {MenuType.MainMenu} ====");
        int index = 1;
        foreach (var menuItem in _menu.GetSystemMenu().Keys)
        {
            Console.WriteLine($"{index}. {menuItem}");
            index++;
        }
        await Task.CompletedTask;
    }

    /// <returns>Tuple containing the menu index and title.</returns>
    public Task<(int menuIndex, string menuTitle)> GetMainMenuSelection()
    {
        Console.Write("\nEnter your choice: ");
        if (!int.TryParse(Console.ReadLine(), out int selectedIndex))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return GetMainMenuSelection();
        }
        // search the dictionary using the index of the number entered by the user
        var systemMenu = _menu.GetSystemMenu();
        string? selectedMenu = systemMenu.Keys.ElementAtOrDefault(selectedIndex - 1);

        if (string.IsNullOrEmpty(selectedMenu))
        {
            Console.WriteLine("Invalid option. Please try again.");
            return GetMainMenuSelection();
        }

        return Task.FromResult((selectedIndex, selectedMenu));
    }

    public async Task DisplaySubMenu(string mainMenuChoice)
    {
        var submenuOptions = _menu.GetSystemMenu().GetValueOrDefault(mainMenuChoice);
        if (submenuOptions != null && submenuOptions.Any())
        {
            Console.WriteLine($"\n===={MenuType.SubMenu}====");
            Console.WriteLine($"\n{mainMenuChoice} - Select an option below:");
            int index = 1;
            foreach (var option in submenuOptions)
            {
                Console.WriteLine($"{index}. {option}");
                index++;
            }
        }
        else
        {
            Console.WriteLine("\nNo available submenu for this selection.");
        }
        await Task.CompletedTask;
    }

    public Task<(int menuIndex, string menuTitle)> GetSubMenuSelection(string mainMenuChoice)
    {
        var submenuOptions = _menu.GetSystemMenu()[mainMenuChoice];

        Console.Write("\nEnter your choice: ");
        if (
            !int.TryParse(Console.ReadLine(), out int selectedIndex)
            || selectedIndex < 1
            || selectedIndex > submenuOptions.Count
        )
        {
            Console.WriteLine("Invalid input. Please enter a valid option.");
            return GetSubMenuSelection(mainMenuChoice);
        }
        string selectedMenu = submenuOptions[selectedIndex - 1];
        return Task.FromResult((selectedIndex, selectedMenu));
    }
}

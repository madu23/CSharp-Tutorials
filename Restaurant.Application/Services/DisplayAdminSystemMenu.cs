using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Services;

public class SysMenu
{
    //public List<string> SystemMenu { get; set; } = new List<string>
    //{
    //    "Staff Management",
    //    "Restaurant Management",
    //    "Exit Application",
    //};

    public Dictionary<string, List<string>> SystemMenu = new Dictionary<string, List<string>>
    {
        {
            "Staff Management",
            new List<string> { "Create Staff", "View Staff", "Edit Staff", "Exit" }
        },
        {
            "Restaurant Management",
            new List<string> { "Menu Setup", "Menu Item Setup", "Exit" }
        },
        {
            "Exit",
            new List<string>()
        },
    };

    public async Task DisplayMainMenu()
    {
        Console.WriteLine("\nSelect a system menu from the list below:");
        int menuCounter = 0;
        foreach (var sysMenu in SystemMenu)
        {
            menuCounter++;
            Console.WriteLine($"{menuCounter} {sysMenu}");
        }
        await Task.CompletedTask;
    }

    public Task<(int menuIndex, string menuTitle)> GetMainMenuSelection()
    {
        Console.Write("\nEnter your choice: ");
        int menuSelection = Convert.ToInt32(Console.ReadLine());
        if (menuSelection >= 1 && menuSelection <= SystemMenu.Count)
        {
            // search the dictionary using the index of the number entered by the user

            return Task.FromResult((menuSelection, "MenuTitle"));
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid option.");
            return GetMainMenuSelection();
        }

    }

    public async Task DisplaySubMenu(string mainMenuChoice)
    {
        if (SystemMenu.ContainsKey(mainMenuChoice))
        {
            Console.WriteLine($"\n{mainMenuChoice} - Select an option below:");
            List<string> submenuOptions = SystemMenu[mainMenuChoice];
            int menuCounter = 0;
            foreach (var sysMenu in submenuOptions)
            {
                menuCounter++;
                Console.WriteLine($"{menuCounter}. {sysMenu}");
            }
        }
        await Task.CompletedTask;
    }

    // Remove this block of code
    //public Task<int> GetSubMenuSelection(string mainMenuChoice)
    //{
    //    if (!subMenus.ContainsKey(mainMenuChoice))
    //    {
    //        Console.WriteLine("Invalid main menu selection.");
    //        return Task.FromResult(-1);
    //    }

    //    List<string> submenuOptions = subMenus[mainMenuChoice];

    //    while (true)
    //    {
    //        Console.Write("\nEnter your choice: ");
    //        int submenuSelection = Convert.ToInt32(Console.ReadLine());
    //        if (submenuSelection >= 1 && submenuSelection <= submenuOptions.Count)
    //        {
    //            return Task.FromResult(submenuSelection);
    //        }

    //        Console.WriteLine("Invalid input. Please enter a valid option.");
    //    }
    //}
}


public class Menu
{
    private const string EXIT = "exit";
    private Dictionary<string, List<string>> SystemMenu = new Dictionary<string, List<string>>
        {
            {
                "Staff Management",
                new List<string> { "Create Staff", "View Staff", "Edit Staff", "Exit" }
            },
            {
                "Restaurant Management",
                new List<string> { "Menu Setup", "Menu Item Setup", "Exit" }
            },
            {
                "Exit",
                new List<string>()
            },
        };
    public required string Title { get; set; }
    public int Index { get; set; }
    public List<Menu>? Submenus { get; set; } = new();
    public MenuType TypeOfMenu { get; set; }

    public Task<Menu> BuildSystemMenu(Dictionary<string, List<string>> menu)
    {
        int menuIndex = 0;
        foreach (var item in SystemMenu)
        {
            Title = item.Key;
            Index = menuIndex++;
            foreach (var submenu in item.Value)
            {
                Submenus!.Add(new Menu { Title = submenu, Index = menuIndex, TypeOfMenu = submenu == EXIT ? MenuType.Exit : MenuType.SubMenu });
            }
            if(item.Key == EXIT)
            {
                TypeOfMenu = MenuType.Exit;
            }
        }
        return Task.FromResult(this);
    }
}

public enum MenuType
{
    MainMenu = 0,
    SubMenu = 1,
    Exit = 2
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Services;

public class SysMenu
{
    public List<string> systemMenu = new List<string>
    {
        "Staff Management",
        "Restaurant Management",
        "Exit Application",
    };

    public Dictionary<string, List<string>> subMenus = new Dictionary<string, List<string>>
    {
        {
            "Staff Management",
            new List<string> { "Create Staff", "View Staff", "Edit Staff", "Exit" }
        },
        {
            "Restaurant Management",
            new List<string> { "Menu Setup", "Menu Item Setup", "Exit" }
        },
    };

    public async Task DisplayMainMenu()
    {
        Console.WriteLine("\nSelect a system menu from the list below:");
        int menuCounter = 0;
        foreach (var sysMenu in systemMenu)
        {
            menuCounter++;
            Console.WriteLine($"{menuCounter} {sysMenu}");
        }
        await Task.CompletedTask;
    }

    public Task<int> GetMainMenuSelection()
    {
        while (true)
        {
            Console.Write("\nEnter your choice: ");
            int menuSelection = Convert.ToInt32(Console.ReadLine());
            if (menuSelection >= 1 && menuSelection <= systemMenu.Count)
                return Task.FromResult(menuSelection);
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid option.");
                GetMainMenuSelection();
            }
        }
    }

    public async Task DisplaySubMenu(string mainMenuChoice)
    {
        if (subMenus.ContainsKey(mainMenuChoice))
        {
            Console.WriteLine($"\n{mainMenuChoice} - Select an option below:");
            List<string> submenuOptions = subMenus[mainMenuChoice];
            int menuCounter = 0;
            foreach (var sysMenu in submenuOptions)
            {
                menuCounter++;
                Console.WriteLine($"{menuCounter}. {sysMenu}");
            }
        }
        await Task.CompletedTask;
    }

    public Task<int> GetSubMenuSelection(string mainMenuChoice)
    {
        if (!subMenus.ContainsKey(mainMenuChoice))
        {
            Console.WriteLine("Invalid main menu selection.");
            return Task.FromResult(-1);
        }

        List<string> submenuOptions = subMenus[mainMenuChoice];

        while (true)
        {
            Console.Write("\nEnter your choice: ");
            int submenuSelection = Convert.ToInt32(Console.ReadLine());
            if (submenuSelection >= 1 && submenuSelection <= submenuOptions.Count)
            {
                return Task.FromResult(submenuSelection);
            }

            Console.WriteLine("Invalid input. Please enter a valid option.");
        }
    }
}

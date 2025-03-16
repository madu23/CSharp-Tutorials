using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Handlers;

public class MenuHandler
{
    private readonly Dictionary<string, List<string>> _menuStructure;

    public MenuHandler()
    {
        _menuStructure = new Dictionary<string, List<string>>
        {
            {
                "System Admin Main Menu",
                new List<string> { "Staff Management", "Restaurant Management", "Exit" }
            },
            {
                "Staff Management",
                new List<string>
                {
                    "View Staff",
                    "Create Staff",
                    "Edit Staff",
                    "Delete Staff",
                    "Back",
                }
            },
            {
                "Restaurant Management",
                new List<string> { "Menu Setup", "Menu Item Setup", "Back" }
            },
        };
    }

    public void DisplayMenu(string menuName)
    {
        if (_menuStructure.ContainsKey(menuName))
        {
            Console.WriteLine($"\n{menuName}:");
            var menuItems = _menuStructure[menuName];
            for (int i = 0; i < menuItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuItems[i]}");
            }
        }
        else
        {
            Console.WriteLine("Invalid menu name.");
        }
    }

    public string GetMenuSelection(string menuName)
    {
        var selectedIndex = GetMenuSelectionIndex(menuName);
        return _menuStructure[menuName][selectedIndex - 1];
    }

    public int GetMenuSelectionIndex(string menuName)
    {
        DisplayMenu(menuName);
        var selection = Console.ReadLine();
        if (
            int.TryParse(selection, out int selectedIndex)
            && selectedIndex > 0
            && selectedIndex <= _menuStructure[menuName].Count
        )
        {
            return selectedIndex;
        }
        else
        {
            Console.WriteLine("Invalid selection. Please try again.");
            return GetMenuSelectionIndex(menuName);
        }
    }
}

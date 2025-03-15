namespace Restaurant.Domain.Entities;

/// <summary>
/// Represents the system menu structure and its functionality.
/// This class defines the main menu and its submenus for staff and restaurant management.
/// </summary>
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
        { "Exit", new List<string>() },
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
                Submenus!.Add(
                    new Menu
                    {
                        Title = submenu,
                        Index = menuIndex,
                        TypeOfMenu = submenu == EXIT ? MenuType.Exit : MenuType.SubMenu,
                    }
                );
            }
            if (item.Key == EXIT)
            {
                TypeOfMenu = MenuType.Exit;
            }
        }
        return Task.FromResult(this);
    }

    /// <summary>
    /// Retrieves the system menu containing categorized menu options.
    /// </summary>
    /// <returns>
    /// This is a dictionary where the keys represent menu categories (e.g., "Staff Management"),
    /// and the values are lists of menu options under each category.
    /// </returns>
    public Dictionary<string, List<string>> GetSystemMenu()
    {
        return SystemMenu;
    }
}

public enum MenuType
{
    MainMenu = 0,
    SubMenu = 1,
    Exit = 2,
}

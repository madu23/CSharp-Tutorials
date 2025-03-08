using Restaurant.Domain.Db;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Domain.Entities;

/// <summary>
/// This class represents a menu item in the restaurant
/// </summary>
public class MenuItem
{
    public int ItemId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string Category { get; set; } = null!; // "Continental" or "Local"

    public void CreateMenuItem()
    {
        // Check if menu item with same ID already exists
        if (AppDb.MenuItemsTable.ContainsKey(this.ItemId))
        {
            // Update the existing item
            AppDb.MenuItemsTable[this.ItemId] = this;
        }
        else
        {
            // Add a new item
            AppDb.MenuItemsTable.Add(this.ItemId, this);
        }
    }

    public static List<MenuItem> GetAllMenuItems()
    {
        return AppDb.MenuItemsTable.Values.ToList();
    }

    public static List<MenuItem> GetMenuItemsByCategory(string category)
    {
        return AppDb.MenuItemsTable.Values
            .Where(item => item.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public static MenuItem GetMenuItem(int id)
    {
        if (!AppDb.MenuItemsTable.ContainsKey(id))
            throw new EntityNotFoundException($"Menu item with ID {id} not found");

        return AppDb.MenuItemsTable[id];
    }

    public static void DeleteMenuItem(int id)
    {
        if (!AppDb.MenuItemsTable.ContainsKey(id))
            throw new EntityNotFoundException($"Menu item with ID {id} not found");

        AppDb.MenuItemsTable.Remove(id);
    }
}
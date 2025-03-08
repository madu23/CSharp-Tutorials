using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Application.Services;

public class MenuManagementService
{
    private Dictionary<int, MenuItem> _menuItems = new Dictionary<int, MenuItem>();

    public void ManageRestaurantMenu()
    {
        bool returnToMainMenu = false;
        while (!returnToMainMenu)
        {
            Console.WriteLine("\n--- Restaurant Menu Management ---");
            Console.WriteLine("1. Create Menu Item");
            Console.WriteLine("2. View Menu Items");
            Console.WriteLine("3. Edit Menu Item");
            Console.WriteLine("4. Delete Menu Item");
            Console.WriteLine("5. Return to Main Menu");

            var menuSelection = Console.ReadLine();
            switch (menuSelection)
            {
                case "1":
                    CreateMenuItem();
                    break;
                case "2":
                    ViewMenuItems();
                    break;
                case "3":
                    EditMenuItem();
                    break;
                case "4":
                    DeleteMenuItem();
                    break;
                case "5":
                    returnToMainMenu = true;
                    break;
                default:
                    Console.WriteLine("Invalid selection");
                    break;
            }
        }
    }

    private void CreateMenuItem()
    {
        try
        {
            Console.WriteLine("Select Menu Type:");
            Console.WriteLine("1. Continental Dishes");
            Console.WriteLine("2. Local Dishes");

            var menuTypeSelection = Console.ReadLine();
            string menuType;

            switch (menuTypeSelection)
            {
                case "1":
                    menuType = "Continental";
                    break;
                case "2":
                    menuType = "Local";
                    break;
                default:
                    Console.WriteLine("Invalid selection");
                    return;
            }

            Console.WriteLine($"Creating a new {menuType} menu item");
            Console.WriteLine("Enter menu item details (id, name, description, price):");
            var menuItemInfo = Console.ReadLine();
            var splitMenuItemInfo = menuItemInfo.Split(',');

            if (splitMenuItemInfo.Length < 4)
            {
                Console.WriteLine("Insufficient information. Please provide all required details.");
                return;
            }

            var menuItem = new MenuItem
            {
                ItemId = Convert.ToInt32(splitMenuItemInfo[0]),
                Name = splitMenuItemInfo[1].Trim(),
                Description = splitMenuItemInfo[2].Trim(),
                Price = Convert.ToDecimal(splitMenuItemInfo[3].Trim()),
                Category = menuType
            };

            _menuItems[menuItem.ItemId] = menuItem;
            Console.WriteLine("Menu item created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something went wrong: {ex.Message}");
        }
    }

    private void ViewMenuItems()
    {
        try
        {
            Console.WriteLine("Select which menu items to view:");
            Console.WriteLine("1. Continental Dishes");
            Console.WriteLine("2. Local Dishes");
            Console.WriteLine("3. All Dishes");

            var viewSelection = Console.ReadLine();
            List<MenuItem> menuItems;

            switch (viewSelection)
            {
                case "1":
                    menuItems = _menuItems.Values.Where(m => m.Category == "Continental").ToList();
                    Console.WriteLine("\nContinental Dishes Menu");
                    break;
                case "2":
                    menuItems = _menuItems.Values.Where(m => m.Category == "Local").ToList();
                    Console.WriteLine("\nLocal Dishes Menu");
                    break;
                case "3":
                    menuItems = _menuItems.Values.ToList();
                    Console.WriteLine("\nComplete Restaurant Menu");
                    break;
                default:
                    Console.WriteLine("Invalid selection");
                    return;
            }

            if (menuItems.Count == 0)
            {
                Console.WriteLine("No menu items found.");
                return;
            }

            Console.WriteLine("===========================================");
            Console.WriteLine(String.Format("{0,-5} {1,-20} {2,-30} {3,-10} {4,-15}", "ID", "Name", "Description", "Price", "Category"));
            Console.WriteLine("===========================================");

            foreach (var item in menuItems)
            {
                Console.WriteLine(String.Format("{0,-5} {1,-20} {2,-30} {3,-10:C} {4,-15}",
                    item.ItemId, item.Name, item.Description, item.Price, item.Category));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something went wrong: {ex.Message}");
        }
    }

    private void EditMenuItem()
    {
        try
        {
            Console.WriteLine("Enter the menu item ID to edit:");
            var itemId = Convert.ToInt32(Console.ReadLine());

            if (!_menuItems.ContainsKey(itemId))
            {
                Console.WriteLine("Menu item not found.");
                return;
            }

            var menuItem = _menuItems[itemId];
            Console.WriteLine($"Editing: {menuItem.Name} ({menuItem.Category})");
            Console.WriteLine("Enter new details (name, description, price):");

            var itemInfo = Console.ReadLine();
            var splitItemInfo = itemInfo.Split(',');

            if (splitItemInfo.Length < 3)
            {
                Console.WriteLine("Insufficient information. Please provide all required details.");
                return;
            }

            // Ask if they want to change the category
            Console.WriteLine("Do you want to change the category? (y/n)");
            var changeCategory = Console.ReadLine()?.ToLower() == "y";

            string category = menuItem.Category;
            if (changeCategory)
            {
                Console.WriteLine("Select new category:");
                Console.WriteLine("1. Continental Dishes");
                Console.WriteLine("2. Local Dishes");

                var categorySelection = Console.ReadLine();
                category = categorySelection == "1" ? "Continental" : "Local";
            }

            // Update the menu item
            menuItem.Name = splitItemInfo[0].Trim();
            menuItem.Description = splitItemInfo[1].Trim();
            menuItem.Price = Convert.ToDecimal(splitItemInfo[2].Trim());
            menuItem.Category = category;

            Console.WriteLine("Menu item updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something went wrong: {ex.Message}");
        }
    }

    private void DeleteMenuItem()
    {
        try
        {
            Console.WriteLine("Enter the menu item ID to delete:");
            var itemId = Convert.ToInt32(Console.ReadLine());

            if (!_menuItems.ContainsKey(itemId))
            {
                Console.WriteLine("Menu item not found.");
                return;
            }

            var menuItem = _menuItems[itemId];
            Console.WriteLine($"Are you sure you want to delete '{menuItem.Name}'? (y/n)");

            var confirmation = Console.ReadLine()?.ToLower();
            if (confirmation == "y")
            {
                _menuItems.Remove(itemId);
                Console.WriteLine("Menu item deleted successfully.");
            }
            else
            {
                Console.WriteLine("Delete operation cancelled.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something went wrong: {ex.Message}");
        }
    }
}
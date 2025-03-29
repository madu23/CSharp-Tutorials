using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Services;
public class MainMenuTask1
{
    public Task<bool> showMenu()
    {
        public class Dictionary<string, list<string>> menuOptions = new Dictionary<string, list<string>>();
        public MainMenuTask1()
    {
        menuOptions = new Dictionary<string, list<string>>
            {
                {
                    "Admin main menu", new List<string>
                    {
                        "staff management",
                        "restaurant management",
                        "Exit"
                    },
                    {
                        "Staff management", new List<string>
                        {
                            "Create staff",
                            "View staff",
                            "Edit staff",
                            "Delete staff",
                            "Back",
                        }
                    },
                    {
                        "Restaurant management", new List<string>
                        {
                            "menu setup",
                            "menu item setup",
                            "Back",

                        }
                    }
                }
            }
}
    }
    public void DisplayMenu()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Select a menu option from the list below");
        Console.WriteLine("=======================================");
        int counter = 0;
        foreach (var menu in menuOptions)
        {
            Console.WriteLine($"{counter + 1}. {menu.Key}");
            counter++;
        }
    }

    public string getMenuSelection(string menuType)
    {

        Console.WriteLine("Enter your selection");
        var selection = Console.ReadLine();
        if (menuType == "Admin main menu")
        {
            if (selection == "1")
            {
                return "Staff management";
            }
            else if (selection == "2")
            {
                return "Restaurant management";
            }
            else if (selection == "3")
            {
                return "Exit";
            }
            else
            {
                return "Invalid selection";
            }
        }
        else if (menuType == "Staff management")
        {
            if (selection == "1")
            {
                return "Create staff";
            }
            else if (selection == "2")
            {
                return "View staff";
            }
            else if (selection == "3")
            {
                return "Edit staff";
            }
            else if (selection == "4")
            {
                return "Delete staff";
            }
            else if (selection == "5")
            {
                return "Back";
            }
            else
            {
                return "Invalid selection";
            }
        }
        else if (menuType == "Restaurant management")
        {
            if (selection == "1")
            {
                return "Menu setup";
            }
            else if (selection == "2")
            {
                return "Menu item setup";
            }
            else if (selection == "3")
            {
                return "Back";
            }
            else
            {
                return "Invalid selection";
            }
        }
       
    }
   
    
    
}





   
    

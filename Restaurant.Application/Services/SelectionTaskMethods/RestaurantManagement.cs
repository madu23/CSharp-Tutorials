using System;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Handlers;

namespace Restaurant.Application.Services.SelectionTaskMethods
{
    public class RestaurantManagement
    {
        private readonly MenuHandler _menuHandler;

        public RestaurantManagement(MenuHandler menuHandler)
        {
            _menuHandler = menuHandler;
        }

        public async Task Execute()
        {
            int restaurantMenuSelection;
            do
            {
                restaurantMenuSelection = _menuHandler.GetMenuSelectionIndex(
                    "Restaurant Management"
                );
                switch (restaurantMenuSelection)
                {
                    case ResponseOptions.RestaurantManagement.MenuSetup:
                    case ResponseOptions.RestaurantManagement.MenuItemSetup:
                        await DisplayUnderDevelopmentMessage();
                        break;
                    case ResponseOptions.RestaurantManagement.Exit:
                        return; // this returns to main menu
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            } while (true);
        }

        private async Task DisplayUnderDevelopmentMessage()
        {
            string message = "Restaurant Menu is still under development !!!";
            foreach (var word in message.Split(' '))
            {
                Console.Write(word + " ");
                await Task.Delay(200);
            }
            Console.WriteLine();
        }
    }
}

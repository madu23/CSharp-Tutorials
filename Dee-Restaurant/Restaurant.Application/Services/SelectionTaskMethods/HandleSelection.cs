using System;
using System.Threading.Tasks;
using Restaurant.Domain.Handlers;

namespace Restaurant.Application.Services.SelectionTaskMethods
{
    public static class HandleSelection
    {
        public static async Task<bool> Execute(int mainMenuSelection, MenuHandler menuHandler, StaffHandler staffHandler, ResponseOptions responseOptions)
        {
            switch (mainMenuSelection)
            {
                case ResponseOptions.MainMenu.StaffManagement:
                    await HandleStaffManagement.Execute(menuHandler, staffHandler);
                    break;
                case ResponseOptions.MainMenu.RestaurantManagement:
                    await HandleRestaurantManagement.Execute(menuHandler);
                    break;
                case ResponseOptions.MainMenu.Exit:
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid main menu selection.");
                    break;
            }
            return true;
        }
    }
}
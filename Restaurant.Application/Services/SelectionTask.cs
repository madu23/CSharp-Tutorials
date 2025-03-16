using System;
using System.Threading.Tasks;
using Restaurant.Application.Services.SelectionTaskMethods;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Handlers;

namespace Restaurant.Application.Services
{
    public class SelectionTask
    {
        // Private fields for handling menu, response options, staff, and input
        private readonly MenuHandler _menuHandler;
        private readonly ResponseOptions _responseOptions;
        private readonly StaffHandler _staffHandler;
        private readonly InputHandler _inputHandler;

        // Constructor to initialize the handlers
        public SelectionTask()
        {
            _menuHandler = new MenuHandler();
            _responseOptions = new ResponseOptions();
            _staffHandler = new StaffHandler();
            _inputHandler = new InputHandler();
        }

        // Method to handle the selection from the main menu
        public async Task<bool> HandleSelection(int mainMenuSelection)
        {
            switch (mainMenuSelection)
            {
                case ResponseOptions.MainMenu.StaffManagement:
                    // Handle the Staff Management option
                    await new HandleStaffManagement(
                        _menuHandler,
                        _staffHandler,
                        _inputHandler
                    ).Execute();
                    break;
                case ResponseOptions.MainMenu.RestaurantManagement:
                    // Handle the Restaurant Management option
                    await new HandleRestaurantManagement().Execute();
                    break;
                case ResponseOptions.MainMenu.Exit:
                    // Handle the Exit option
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                    break;
                default:
                    // Handle invalid main menu selections
                    Console.WriteLine("Invalid main menu selection.");
                    break;
            }
            return true;
        }
    }
}

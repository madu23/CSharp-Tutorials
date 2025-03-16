using System;
using System.Threading.Tasks;
using Restaurant.Application.Services.SelectionTaskMethods;
using Restaurant.Domain.Entities; // Add this line
using Restaurant.Domain.Handlers;

namespace Restaurant.Application.Services
{
    public class SelectionTask
    {
        private readonly MenuHandler _menuHandler;
        private readonly ResponseOptions _responseOptions;
        private readonly StaffHandler _staffHandler;
        private readonly InputHandler _inputHandler;

        public SelectionTask()
        {
            _menuHandler = new MenuHandler();
            _responseOptions = new ResponseOptions();
            _staffHandler = new StaffHandler();
            _inputHandler = new InputHandler();
        }

        public async Task<bool> HandleSelection(int mainMenuSelection)
        {
            switch (mainMenuSelection)
            {
                case ResponseOptions.MainMenu.StaffManagement:
                    await new HandleStaffManagement(
                        _menuHandler,
                        _staffHandler,
                        _inputHandler
                    ).Execute();
                    break;
                case ResponseOptions.MainMenu.RestaurantManagement:
                    await new HandleRestaurantManagement().Execute();
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

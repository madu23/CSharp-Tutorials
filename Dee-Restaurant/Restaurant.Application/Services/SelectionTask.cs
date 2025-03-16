using System;
using System.Threading.Tasks;
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
                    await HandleStaffManagement();
                    break;
                case ResponseOptions.MainMenu.RestaurantManagement:
                    await HandleRestaurantManagement();
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

        private async Task HandleStaffManagement()
        {
            int staffMenuSelection;
            do
            {
                staffMenuSelection = _menuHandler.GetMenuSelectionIndex("Staff Management");
                switch (staffMenuSelection)
                {
                    case ResponseOptions.StaffManagement.ViewStaff:
                        await HandleViewStaff.HandleViewStaff(_staffHandler, _inputHandler);
                        break;
                    case ResponseOptions.StaffManagement.CreateStaff:
                        await HandleCreateStaff.HandleCreateStaff(_staffHandler, _inputHandler);
                        break;
                    case ResponseOptions.StaffManagement.EditStaff:
                        await HandleEditStaff.HandleEditStaff(_staffHandler, _inputHandler);
                        break;
                    case ResponseOptions.StaffManagement.DeleteStaff:
                        await HandleDeleteStaff.HandleDeleteStaff(_staffHandler, _inputHandler);
                        break;
                    case ResponseOptions.StaffManagement.Exit:
                        return; // this returns to main menu
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            } while (true);
        }

        private async Task HandleRestaurantManagement()
        {
            int restaurantMenuSelection;
            do
            {
                restaurantMenuSelection = _menuHandler.GetMenuSelectionIndex("Restaurant Management");
                switch (restaurantMenuSelection)
                {
                    case ResponseOptions.RestaurantManagement.MenuSetup:
                    case ResponseOptions.RestaurantManagement.MenuItemSetup:
                        await DisplayUnderDevelopmentMessage.DisplayUnderDevelopmentMessage();
                        break;
                    case ResponseOptions.RestaurantManagement.Exit:
                        return; // this returns to main menu
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            } while (true);
        }
    }
}
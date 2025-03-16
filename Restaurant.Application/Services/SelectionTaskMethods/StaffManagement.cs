using System;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Handlers;

namespace Restaurant.Application.Services.SelectionTaskMethods
{
    public class StaffManagement
    {
        private readonly MenuHandler _menuHandler;
        private readonly StaffHandler _staffHandler;
        private readonly InputHandler _inputHandler;

        public StaffManagement(
            MenuHandler menuHandler,
            StaffHandler staffHandler,
            InputHandler inputHandler
        )
        {
            _menuHandler = menuHandler;
            _staffHandler = staffHandler;
            _inputHandler = inputHandler;
        }

        public async Task Execute()
        {
            int staffMenuSelection;
            do
            {
                staffMenuSelection = _menuHandler.GetMenuSelectionIndex("Staff Management");
                switch (staffMenuSelection)
                {
                    case ResponseOptions.StaffManagement.ViewStaff:
                        await new HandleViewStaff(_staffHandler, _inputHandler).Execute();
                        break;
                    case ResponseOptions.StaffManagement.CreateStaff:
                        await new HandleCreateStaff(_staffHandler, _inputHandler).Execute();
                        break;
                    case ResponseOptions.StaffManagement.EditStaff:
                        await new HandleEditStaff(_staffHandler, _inputHandler).Execute();
                        break;
                    case ResponseOptions.StaffManagement.DeleteStaff:
                        await new HandleDeleteStaff(_staffHandler, _inputHandler).Execute();
                        break;
                    case ResponseOptions.StaffManagement.Exit:
                        return;
                    default:
                        Console.WriteLine("Invalid selection. Please try gain.");
                        break;
                }
            } while (true);
        }
    }
}

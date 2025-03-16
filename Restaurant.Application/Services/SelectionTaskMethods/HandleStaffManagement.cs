using System;
using System.Threading.Tasks;
using Restaurant.Domain.Handlers;

namespace Restaurant.Application.Services.SelectionTaskMethods
{
    public class HandleStaffManagement
    {
        private readonly MenuHandler _menuHandler;
        private readonly StaffHandler _staffHandler;
        private readonly InputHandler _inputHandler;

        public HandleStaffManagement(
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
            // Create an instance of StaffManagement and execute the task
            var staffManagement = new StaffManagement(_menuHandler, _staffHandler, _inputHandler);
            await staffManagement.Execute();
        }
    }
}

using System;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Handlers;

namespace Restaurant.Application.Services.SelectionTaskMethods
{
    public class HandleEditStaff
    {
        private readonly StaffHandler _staffHandler;
        private readonly InputHandler _inputHandler;

        public HandleEditStaff(StaffHandler staffHandler, InputHandler inputHandler)
        {
            _staffHandler = staffHandler;
            _inputHandler = inputHandler;
        }

        public async Task Execute()
        {
            // Create an instance of EditStaff and execute the task
            var editStaff = new EditStaff(_staffHandler, _inputHandler);
            await editStaff.Execute();
        }
    }
}

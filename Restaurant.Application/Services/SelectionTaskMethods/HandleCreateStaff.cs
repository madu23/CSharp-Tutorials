using System;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Handlers;

namespace Restaurant.Application.Services.SelectionTaskMethods
{
    public class HandleCreateStaff
    {
        private readonly StaffHandler _staffHandler;
        private readonly InputHandler _inputHandler;

        public HandleCreateStaff(StaffHandler staffHandler, InputHandler inputHandler)
        {
            _staffHandler = staffHandler;
            _inputHandler = inputHandler;
        }

        public async Task Execute()
        {
            // Create an instance of CreateStaff and execute the task
            var createStaff = new CreateStaff(_staffHandler, _inputHandler);
            await createStaff.Execute();
        }
    }
}

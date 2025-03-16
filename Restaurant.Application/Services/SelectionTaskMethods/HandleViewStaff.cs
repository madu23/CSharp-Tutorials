using System;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Handlers;

namespace Restaurant.Application.Services.SelectionTaskMethods
{
    public class HandleViewStaff
    {
        private readonly StaffHandler _staffHandler;
        private readonly InputHandler _inputHandler;

        public HandleViewStaff(StaffHandler staffHandler, InputHandler inputHandler)
        {
            _staffHandler = staffHandler;
            _inputHandler = inputHandler;
        }

        public async Task Execute()
        {
            // Create an instance of ViewStaff and execute the task
            var viewStaff = new ViewStaff(_staffHandler, _inputHandler);
            await viewStaff.Execute();
        }
    }
}

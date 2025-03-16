using System;
using System.Threading.Tasks;
using Restaurant.Domain.Handlers;

namespace Restaurant.Application.Services.SelectionTaskMethods
{
    public class DeleteStaff
    {
        private readonly StaffHandler _staffHandler;
        private readonly InputHandler _inputHandler;

        public DeleteStaff(StaffHandler staffHandler, InputHandler inputHandler)
        {
            _staffHandler = staffHandler;
            _inputHandler = inputHandler;
        }

        public async Task Execute()
        {
            int id = _inputHandler.GetValidIntInput("\nEnter Staff Id to delete:");

            if (id == 1)
            {
                Console.WriteLine("\nCannot delete System Admin.");
                return;
            }

            try
            {
                _staffHandler.DeleteStaff(id);
                Console.WriteLine($"Staff with Id {id} has been deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await Task.CompletedTask;
        }
    }
}

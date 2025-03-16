using System;
using System.Threading.Tasks;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Handlers;

namespace Restaurant.Application.Services.SelectionTaskMethods
{
    public class HandleDeleteStaff
    {
        private readonly StaffHandler _staffHandler;
        private readonly InputHandler _inputHandler;

        public HandleDeleteStaff(StaffHandler staffHandler, InputHandler inputHandler)
        {
            _staffHandler = staffHandler;
            _inputHandler = inputHandler;
        }

        public async Task Execute()
        {
            while (true)
            {
                // Prompt user to enter the Staff ID to delete
                int id = _inputHandler.GetValidIntInput(
                    $"\nEnter Staff Id to delete ({ResponseOptions.DeleteStaff.EnterStaffId}):"
                );

                // Check if the entered ID is for the System Admin
                if (id == 1)
                {
                    Console.WriteLine("\nCannot delete System Admin.");
                    return;
                }

                try
                {
                    // Check if the staff with the given ID exists
                    if (_staffHandler.ViewStaff(id) == null)
                    {
                        Console.WriteLine(
                            $"Staff with Id {id} does not exist. Kindly input the correct Id."
                        );
                        continue;
                    }

                    // Delete the staff with the given ID
                    _staffHandler.DeleteStaff(id);
                    Console.WriteLine($"Staff with Id {id} has been deleted.");
                    break;
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during deletion
                    Console.WriteLine(ex.Message);
                }
            }
            await Task.CompletedTask;
        }
    }
}

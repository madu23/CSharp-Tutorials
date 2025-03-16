using System;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Domain.Db;
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
            int newStaffId;
            do
            {
                // Prompt user to enter a new Staff ID
                newStaffId = _inputHandler.GetValidIntInput($"\nEnter Staff Id:");
                if (AppDb.StaffTable.ContainsKey(newStaffId))
                {
                    // If Staff ID already exists, prompt user to enter a unique Staff ID
                    Console.WriteLine(
                        $"Something went wrong: Staff {newStaffId} with {AppDb.StaffTable[newStaffId].Designation} already exists."
                    );
                    Console.WriteLine("Please input a unique Staff Id.");
                }
                else
                {
                    break;
                }
            } while (true);

            // Prompt user to enter the new staff details
            var firstName = _inputHandler.GetValidStringInput($"Enter First Name:");
            var lastName = _inputHandler.GetValidStringInput($"Enter Last Name:");
            var designation = _inputHandler.GetValidStringInput($"Enter Designation:");
            var password = _inputHandler.GetValidStringInput($"Enter Password:");

            // Create a new Staff object with the entered details
            var newStaffData = new Staff
            {
                StaffId = newStaffId,
                FirstName = firstName,
                LastName = lastName,
                Designation = designation,
                Password = password,
            };

            // Add the new staff to the database
            _staffHandler.CreateStaff(newStaffData);
            Console.WriteLine("\nNew Staff has been created");

            // Display the list of all staff
            Display.DisplayStaffList(AppDb.StaffTable);
            await Task.CompletedTask;
        }
    }
}

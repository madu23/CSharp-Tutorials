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
            int newStaffId;
            do
            {
                newStaffId = _inputHandler.GetValidIntInput("\nEnter Staff Id:");
                if (AppDb.StaffTable.ContainsKey(newStaffId))
                {
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

            var firstName = _inputHandler.GetValidStringInput("Enter First Name:");
            var lastName = _inputHandler.GetValidStringInput("Enter Last Name:");
            var designation = _inputHandler.GetValidStringInput("Enter Designation:");
            var password = _inputHandler.GetValidStringInput("Enter Password:");

            var newStaffData = new Staff
            {
                StaffId = newStaffId,
                FirstName = firstName,
                LastName = lastName,
                Designation = designation,
                Password = password,
            };
            _staffHandler.CreateStaff(newStaffData);
            Console.WriteLine("\nNew Staff has been created");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(
                string.Format(
                    "{0, -10} {1, -15} {2, -15} {3, -20}",
                    "StaffId",
                    "First Name",
                    "Last Name",
                    "Designation"
                )
            );
            Console.WriteLine(
                "========================================================================"
            );
            var staffList = AppDb.StaffTable.Values.ToList();
            foreach (var record in staffList)
            {
                Console.WriteLine(
                    string.Format(
                        "{0, -10} {1, -15} {2, -15} {3, -20}",
                        record.StaffId,
                        record.FirstName,
                        record.LastName,
                        record.Designation
                    )
                );
            }
            await Task.CompletedTask;
        }
    }
}
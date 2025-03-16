using System;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Handlers;

namespace Restaurant.Application.Services.SelectionTaskMethods
{
    public class CreateStaff
    {
        private readonly StaffHandler _staffHandler;
        private readonly InputHandler _inputHandler;

        public CreateStaff(StaffHandler staffHandler, InputHandler inputHandler)
        {
            _staffHandler = staffHandler;
            _inputHandler = inputHandler;
        }

        public async Task Execute()
        {
            var newStaffData = new Staff();
            bool isCancelled = false;

            while (!isCancelled)
            {
                Console.WriteLine("\nSelect the detail to input:");
                Console.WriteLine($"{ResponseOptions.CreateStaff.EnterStaffId}. Enter Staff Id");
                Console.WriteLine(
                    $"{ResponseOptions.CreateStaff.EnterFirstName}. Enter First Name"
                );
                Console.WriteLine($"{ResponseOptions.CreateStaff.EnterLastName}. Enter Last Name");
                Console.WriteLine(
                    $"{ResponseOptions.CreateStaff.EnterDesignation}. Enter Designation"
                );
                Console.WriteLine($"{ResponseOptions.CreateStaff.EnterPassword}. Enter Password");
                Console.WriteLine($"{ResponseOptions.CreateStaff.Cancel}. Cancel");

                var selection = _inputHandler.GetValidIntInput("Enter your choice:");

                switch (selection)
                {
                    case ResponseOptions.CreateStaff.EnterStaffId:
                        newStaffData.StaffId = _inputHandler.GetValidIntInput("Enter Staff Id:");
                        break;
                    case ResponseOptions.CreateStaff.EnterFirstName:
                        newStaffData.FirstName = _inputHandler.GetValidStringInput(
                            "Enter First Name:"
                        );
                        break;
                    case ResponseOptions.CreateStaff.EnterLastName:
                        newStaffData.LastName = _inputHandler.GetValidStringInput(
                            "Enter Last Name:"
                        );
                        break;
                    case ResponseOptions.CreateStaff.EnterDesignation:
                        newStaffData.Designation = _inputHandler.GetValidStringInput(
                            "Enter Designation:"
                        );
                        break;
                    case ResponseOptions.CreateStaff.EnterPassword:
                        newStaffData.Password = _inputHandler.GetValidStringInput(
                            "Enter Password:"
                        );
                        break;
                    case ResponseOptions.CreateStaff.Cancel:
                        isCancelled = true;
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }

            if (!isCancelled)
            {
                _staffHandler.CreateStaff(newStaffData);
                Console.WriteLine("\nNew Staff has been created");
                Display.DisplayStaffList(AppDb.StaffTable);
            }

            await Task.CompletedTask;
        }
    }
}

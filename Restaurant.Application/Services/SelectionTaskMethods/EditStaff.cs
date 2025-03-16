using System;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Handlers;

namespace Restaurant.Application.Services.SelectionTaskMethods
{
    public class EditStaff
    {
        private readonly StaffHandler _staffHandler;
        private readonly InputHandler _inputHandler;

        public EditStaff(StaffHandler staffHandler, InputHandler inputHandler)
        {
            _staffHandler = staffHandler;
            _inputHandler = inputHandler;
        }

        public async Task Execute()
        {
            int id;
            Staff staffToEdit;
            do
            {
                id = _inputHandler.GetValidIntInput("\nEnter Staff Id to edit");

                if (id == 1)
                {
                    Console.WriteLine("\nCannot Edit System Admin data.");
                    return;
                }

                staffToEdit = _staffHandler.ViewStaff(id);
                if (staffToEdit == null)
                {
                    Console.WriteLine($"Staff with Id {id} not found, Input existing Id.");
                }
            } while (staffToEdit == null);

            string newFirstName,
                newLastName,
                newDesignation,
                newPassword;
            do
            {
                Console.WriteLine(
                    "Enter the details you want to edit (To keep current value, press enter):"
                );
                newFirstName = _inputHandler.GetValidStringInput(
                    $"Current First Name: {staffToEdit?.FirstName}\nNew First Name: "
                );
                newLastName = _inputHandler.GetValidStringInput(
                    $"Current Last Name: {staffToEdit?.LastName}\nNew Last Name: "
                );
                newDesignation = _inputHandler.GetValidStringInput(
                    $"Current Designation: {staffToEdit?.Designation}\nNew Designation: "
                );
                newPassword = _inputHandler.GetValidStringInput(
                    $"Current Password: {staffToEdit?.Password}\nNew Password: "
                );

                if (
                    string.IsNullOrWhiteSpace(newFirstName)
                    && string.IsNullOrWhiteSpace(newLastName)
                    && string.IsNullOrWhiteSpace(newDesignation)
                    && string.IsNullOrWhiteSpace(newPassword)
                )
                {
                    Console.WriteLine("No changes made. Keeping all current values.");
                    break;
                }

                var updatedStaff = new Staff
                {
                    FirstName = string.IsNullOrEmpty(newFirstName)
                        ? staffToEdit?.FirstName
                        : newFirstName,
                    LastName = string.IsNullOrEmpty(newLastName)
                        ? staffToEdit?.LastName
                        : newLastName,
                    Designation = string.IsNullOrEmpty(newDesignation)
                        ? staffToEdit?.Designation
                        : newDesignation,
                    Password = string.IsNullOrEmpty(newPassword)
                        ? staffToEdit?.Password
                        : newPassword,
                };

                _staffHandler.EditStaff(id, updatedStaff);
                Console.WriteLine("Staff details updated successfully.");
                break;
            } while (true);
            await Task.CompletedTask;
        }
    }
}

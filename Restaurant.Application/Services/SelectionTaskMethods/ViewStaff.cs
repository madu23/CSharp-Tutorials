using System;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Handlers;

namespace Restaurant.Application.Services.SelectionTaskMethods
{
    public class ViewStaff
    {
        private readonly StaffHandler _staffHandler;
        private readonly InputHandler _inputHandler;

        public ViewStaff(StaffHandler staffHandler, InputHandler inputHandler)
        {
            _staffHandler = staffHandler;
            _inputHandler = inputHandler;
        }

        public async Task Execute()
        {
            // Prompt user to select an option to view a specific staff, all staffs, or cancel
            Console.WriteLine(
                $"\n{ResponseOptions.ViewStaff.SpecificStaff}. View a specific staff"
            );
            Console.WriteLine($"{ResponseOptions.ViewStaff.AllStaffs}. View all staffs");
            Console.WriteLine($"{ResponseOptions.ViewStaff.Cancel}. Cancel");
            var viewStaffOption = Console.ReadLine();

            if (viewStaffOption == ResponseOptions.ViewStaff.SpecificStaff.ToString())
            {
                // Prompt user to enter the Staff ID to view
                int id = _inputHandler.GetValidIntInput("\nEnter Staff Id:");

                // Retrieve the details of the specific staff
                var specificStaff = _staffHandler.ViewStaff(id);
                if (specificStaff == null)
                {
                    // If staff not found, display a message
                    Console.WriteLine("Staff not found.");
                }
                else
                {
                    // Display the details of the specific staff
                    Console.WriteLine("\nStaff Details:");
                    Console.WriteLine(
                        "========================================================================"
                    );
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
                    Console.WriteLine(
                        string.Format(
                            "{0, -10} {1, -15} {2, -15} {3, -20}",
                            specificStaff.StaffId,
                            specificStaff.FirstName,
                            specificStaff.LastName,
                            specificStaff.Designation
                        )
                    );
                }
            }
            else if (viewStaffOption == ResponseOptions.ViewStaff.AllStaffs.ToString())
            {
                // Display the details of all staffs
                Console.WriteLine("\nStaff Details:");
                Console.WriteLine(
                    "========================================================================"
                );
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
                    "======================================================================"
                );
                foreach (var s in AppDb.StaffTable.Values)
                {
                    Console.WriteLine(
                        string.Format(
                            "{0,-10} {1,-15} {2,-15} {3,-20}",
                            s.StaffId,
                            s.FirstName,
                            s.LastName,
                            s.Designation
                        )
                    );
                }
            }
            else if (viewStaffOption == ResponseOptions.ViewStaff.Cancel.ToString())
            {
                // Cancel the operation
                Console.WriteLine("Operation canceled.");
            }
            else
            {
                // If an invalid option is selected, display a message
                Console.WriteLine("\nInvalid option selected to view staff.");
            }
            await Task.CompletedTask;
        }
    }
}

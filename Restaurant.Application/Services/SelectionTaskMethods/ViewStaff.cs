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
            Console.WriteLine("\n1. View a specific staff");
            Console.WriteLine("2. View all staffs");
            var viewStaffOption = Console.ReadLine();

            if (viewStaffOption == ResponseOptions.ViewStaff.SpecificStaff.ToString())
            {
                int id = _inputHandler.GetValidIntInput("\nEnter Staff Id:");

                var specificStaff = _staffHandler.ViewStaff(id);
                if (specificStaff == null)
                {
                    Console.WriteLine("Staff not found.");
                }
                else
                {
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
            else
            {
                Console.WriteLine("\nInvalid option selected to view staff.");
            }
            await Task.CompletedTask;
        }
    }
}

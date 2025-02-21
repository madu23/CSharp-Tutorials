using System;
using System.Threading.Tasks;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Services
{
    public class SelectionTask
    {
        private readonly AppMenu _appMenu;

        public SelectionTask()
        {
            _appMenu = new AppMenu();
        }

        public async Task<bool> HandleSelection(int mainMenuSelection)
        {
            if (mainMenuSelection == 1)
            {
                int staffMenuSelection;
                do
                {
                    staffMenuSelection = _appMenu.GetMenuSelectionIndex("Staff Management");
                    switch (staffMenuSelection)
                    {
                        case 1:
                            await HandleViewStaff();
                            break;
                        case 2:
                            await HandleCreateStaff();
                            break;
                        case 3:
                            await HandleEditStaff();
                            break;
                        case 4:
                            return true; // this returns to main menu
                        default:
                            Console.WriteLine("Invalid selection. Please try again.");
                            break;
                    }
                } while (true);
            }
            else if (mainMenuSelection == 2)
            {
                // Handle restaurant management logic here
            }
            else
            {
                Console.WriteLine("Invalid main menu selection.");
            }
            return false;
        }

        private async Task HandleViewStaff()
        {
            Console.WriteLine("\nPress 1 to view a specific staff, or 2 to view all staffs:");
            var viewStaffOption = Console.ReadLine();

            if (viewStaffOption == "1")
            {
                Console.WriteLine("\nEnter Staff Id:");
                Console.WriteLine("=======================");
                int id = Convert.ToInt32(Console.ReadLine());

                Staff staffData = new Staff();
                var specificStaff = staffData.ViewStaff(id);
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
            else if (viewStaffOption == "2")
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

        private async Task HandleCreateStaff()
        {
            string[] splitStaffInfo;
            do
            {
                Console.WriteLine(
                    "\nEnter staff details (staff id, first name, last name, designation, password)"
                );
                var newStaffInfo = Console.ReadLine().Trim();
                splitStaffInfo = newStaffInfo.Split(',');

                if (splitStaffInfo.Length != 5 || splitStaffInfo.Any(string.IsNullOrWhiteSpace))
                {
                    Console.WriteLine("Invalid input. Please enter all details correctly.");
                    continue;
                }

                int newStaffId = Convert.ToInt32(splitStaffInfo[0].Trim());
                if (AppDb.StaffTable.ContainsKey(newStaffId))
                {
                    Console.WriteLine(
                        $"Something went wrong: Staff {newStaffId} with {AppDb.StaffTable[newStaffId].Designation} already exists."
                    );
                    Console.WriteLine("Please input correct details.");
                    continue;
                }

                var newStaffData = new Staff
                {
                    StaffId = newStaffId,
                    FirstName = splitStaffInfo[1].Trim(),
                    LastName = splitStaffInfo[2].Trim(),
                    Designation = splitStaffInfo[3].Trim(),
                    Password = splitStaffInfo[4].Trim(),
                };
                newStaffData.CreateStaff();
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
                break;
            } while (true);
            await Task.CompletedTask;
        }

        private async Task HandleEditStaff()
        {
            int id;
            do
            {
                Console.WriteLine("\nEnter Staff Id to edit");
                var input = Console.ReadLine();
                if (int.TryParse(input, out id))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid Staff Id.");
                }
            } while (true);

            Staff staffData = new Staff();
            var staffToEdit = staffData.ViewStaff(id);
            if (staffToEdit == null)
            {
                Console.WriteLine("Staff not found.");
                return;
            }

            string newFirstName,
                newLastName,
                newDesignation,
                newPassword;
            do
            {
                Console.WriteLine(
                    "Enter the details you want to edit (To keep current value, press enter):"
                );
                Console.WriteLine($"Current First Name: {staffToEdit?.FirstName}");
                Console.Write("New First Name: ");
                newFirstName = Console.ReadLine().Trim();
                Console.WriteLine($"Current Last Name: {staffToEdit?.LastName}");
                Console.Write("New Last Name: ");
                newLastName = Console.ReadLine().Trim();
                Console.WriteLine($"Current Designation: {staffToEdit?.Designation}");
                Console.Write("New Designation: ");
                newDesignation = Console.ReadLine().Trim();
                Console.WriteLine($"Current Password: {staffToEdit?.Password}");
                Console.Write("New Password: ");
                newPassword = Console.ReadLine().Trim();

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

                staffData.EditStaff(id, updatedStaff);
                Console.WriteLine("Staff details updated successfully.");
                break;
            } while (true);
            await Task.CompletedTask;
        }
    }
}

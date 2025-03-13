using System;
using System.Threading.Tasks;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Services
{
    public class SelectionTask
    {
        private readonly AppMenu _appMenu;
        private readonly ResponseOptions _responseOptions;

        public SelectionTask()
        {
            _appMenu = new AppMenu();
            _responseOptions = new ResponseOptions();
        }

        /// <summary>
        /// This section handles the selection of the main menu.
        /// </summary>
        public async Task<bool> HandleSelection(int mainMenuSelection)
        {
            switch (mainMenuSelection)
            {
                case ResponseOptions.MainMenu.StaffManagement:
                    await HandleStaffManagement();
                    break;
                case ResponseOptions.MainMenu.RestaurantManagement:
                    await HandleRestaurantManagement();
                    break;
                case ResponseOptions.MainMenu.Exit:
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid main menu selection.");
                    break;
            }
            return true;
        }

        /// <summary>
        /// This handles the staff management menu.
        /// </summary>
        public async Task HandleStaffManagement()
        {
            int staffMenuSelection;
            do
            {
                staffMenuSelection = _appMenu.GetMenuSelectionIndex("Staff Management");
                switch (staffMenuSelection)
                {
                    case ResponseOptions.StaffManagement.ViewStaff:
                        await HandleViewStaff();
                        break;
                    case ResponseOptions.StaffManagement.CreateStaff:
                        await HandleCreateStaff();
                        break;
                    case ResponseOptions.StaffManagement.EditStaff:
                        await HandleEditStaff();
                        break;
                    case ResponseOptions.StaffManagement.DeleteStaff:
                        await HandleDeleteStaff();
                        break;
                    case ResponseOptions.StaffManagement.Exit:
                        return; // this returns to main menu
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            } while (true);
        }

        /// <summary>
        /// This section handles the restaurant management menu.
        /// </summary>
        private async Task HandleRestaurantManagement()
        {
            int restaurantMenuSelection;
            do
            {
                restaurantMenuSelection = _appMenu.GetMenuSelectionIndex("Restaurant Management");
                switch (restaurantMenuSelection)
                {
                    case ResponseOptions.RestaurantManagement.MenuSetup:
                        // Handle Menu Setup logic here
                        break;
                    case ResponseOptions.RestaurantManagement.MenuItemSetup:
                        // Handle Menu Item Setup logic here
                        break;
                    case ResponseOptions.RestaurantManagement.Exit:
                        return; // this returns to main menu
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            } while (true);
        }

        /// <summary>
        /// This section handles the viewing of staff details.
        /// </summary>
        private async Task HandleViewStaff()
        {
            Console.WriteLine("\nPress 1 to view a specific staff, or 2 to view all staffs:");
            var viewStaffOption = Console.ReadLine();

            if (viewStaffOption == ResponseOptions.ViewStaff.SpecificStaff.ToString())
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

        /// <summary>
        /// This section handles the creation of a new staff.
        /// </summary>
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

        /// <summary>
        /// This section handles the editing of an existing staff.
        /// </summary>
        private async Task HandleEditStaff()
        {
            int id;
            do
            {
                Console.WriteLine("\nEnter Staff Id to edit");
                var input = Console.ReadLine();
                if (int.TryParse(input, out id))
                {
                    if (id == 1)
                    {
                        Console.WriteLine("\nCannot Edit System Admin data.");
                        return;
                    }
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

        private async Task HandleDeleteStaff()
        {
            int id;
            do
            {
                Console.WriteLine("\nEnter Staff Id to delete:");
                var input = Console.ReadLine();
                if (int.TryParse(input, out id))
                {
                    if (id == 1)
                    {
                        Console.WriteLine("\nCannot delete System Admin.");
                        return;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid Staff Id.");
                }
            } while (true);

            if (AppDb.StaffTable.ContainsKey(id))
            {
                AppDb.StaffTable.Remove(id);
                Console.WriteLine($"Staff with Id {id} has been deleted.");
            }
            else
            {
                Console.WriteLine("Staff not found.");
            }
            await Task.CompletedTask;
        }
    }
}

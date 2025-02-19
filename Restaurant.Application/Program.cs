using Restaurant.Application.Services;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, Welcome to Eke Tech Restaurant!");

        // seed default admin data step 1
        var seedDbTask = new StartupTask();
        var result = await seedDbTask.SeedAdminRecord();
        if (result == false)
        {
            Console.WriteLine("Admin data was not successfully pre-created");
        }

        // Login Step 2
        var authService = new AuthService();
        var loginResult = await authService.Login();

        Console.WriteLine($"Welcome {loginResult?.FirstName} {loginResult?.LastName}");
        Console.WriteLine($"Select a system menu from the list below");

        if (loginResult?.Designation == "System Admin")
        {
            var appMenu = new AppMenu();
            int mainMenuSelection;
            do
            {
                mainMenuSelection = appMenu.GetMenuSelectionIndex("System Admin Main Menu");
                if (mainMenuSelection == 1)
                {
                    int staffMenuSelection;
                    do
                    {
                        staffMenuSelection = appMenu.GetMenuSelectionIndex("Staff Management");
                        if (staffMenuSelection == 1)
                        {
                            Console.WriteLine(
                                "\nPress 1 to view a specific staff, or 2 to view all staffs:"
                            );
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
                                        String.Format(
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
                        }
                        else if (staffMenuSelection == 2)
                        {
                            string[] splitStaffInfo;
                            do
                            {
                                Console.WriteLine(
                                    "\nEnter staff details (staff id, first name, last name, designation, password)"
                                );
                                var newStaffInfo = Console.ReadLine().Trim();
                                splitStaffInfo = newStaffInfo.Split(',');

                                if (
                                    splitStaffInfo.Length != 5
                                    || splitStaffInfo.Any(string.IsNullOrWhiteSpace)
                                )
                                {
                                    Console.WriteLine(
                                        "Invalid input. Please enter all details correctly."
                                    );
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
                                    String.Format(
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
                                        String.Format(
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
                        }
                        else if (staffMenuSelection == 3)
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
                                    Console.WriteLine(
                                        "Invalid input. Please enter a valid Staff Id."
                                    );
                                }
                            } while (true);

                            Staff staffData = new Staff();
                            var staffToEdit = staffData.ViewStaff(id);
                            if (staffToEdit == null)
                            {
                                Console.WriteLine("Staff not found.");
                                continue;
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
                                Console.WriteLine($"Current First Name: {staffToEdit.FirstName}");
                                Console.Write("New First Name: ");
                                newFirstName = Console.ReadLine().Trim();
                                Console.WriteLine($"Current Last Name: {staffToEdit.LastName}");
                                Console.Write("New Last Name: ");
                                newLastName = Console.ReadLine().Trim();
                                Console.WriteLine(
                                    $"Current Designation: {staffToEdit.Designation}"
                                );
                                Console.Write("New Designation: ");
                                newDesignation = Console.ReadLine().Trim();
                                Console.WriteLine($"Current Password: {staffToEdit.Password}");
                                Console.Write("New Password: ");
                                newPassword = Console.ReadLine().Trim();

                                if (
                                    string.IsNullOrWhiteSpace(newFirstName)
                                    && string.IsNullOrWhiteSpace(newLastName)
                                    && string.IsNullOrWhiteSpace(newDesignation)
                                    && string.IsNullOrWhiteSpace(newPassword)
                                )
                                {
                                    Console.WriteLine(
                                        "No changes made. Keeping all current values."
                                    );
                                    break;
                                }

                                var updatedStaff = new Staff
                                {
                                    FirstName = string.IsNullOrEmpty(newFirstName)
                                        ? staffToEdit.FirstName
                                        : newFirstName,
                                    LastName = string.IsNullOrEmpty(newLastName)
                                        ? staffToEdit.LastName
                                        : newLastName,
                                    Designation = string.IsNullOrEmpty(newDesignation)
                                        ? staffToEdit.Designation
                                        : newDesignation,
                                    Password = string.IsNullOrEmpty(newPassword)
                                        ? staffToEdit.Password
                                        : newPassword,
                                };

                                staffData.EditStaff(id, updatedStaff);
                                Console.WriteLine("Staff details updated successfully.");
                                break;
                            } while (true);
                        }
                        else if (staffMenuSelection == 4)
                        {
                            break; // this returns to main menu
                        }
                        else
                        {
                            Console.WriteLine("Invalid menu selection.");
                        }
                    } while (true);
                }
                else if (mainMenuSelection == 2)
                {
                    int restaurantMenuSelection;
                    do
                    {
                        restaurantMenuSelection = appMenu.GetMenuSelectionIndex(
                            "Restaurant Management"
                        );
                        if (restaurantMenuSelection == 1)
                        {
                            // Logic for menu setup would be here
                        }
                        else if (restaurantMenuSelection == 2)
                        {
                            // Logic for menu item setup would be here
                        }
                        else if (restaurantMenuSelection == 3)
                        {
                            break; // this returns to main menu
                        }
                        else
                        {
                            Console.WriteLine("Invalid menu selection.");
                        }
                    } while (true);
                }
                else if (mainMenuSelection == 3)
                {
                    Console.WriteLine("Exiting...");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid menu selection.");
                }
            } while (true);
        }
    }
}

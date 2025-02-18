// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using Restaurant.Application.Services;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application;

class Program
{
    //static void Main(string[] args)
    //{

    //}
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

        int menuCounter = 0;
        if (loginResult?.Designation == "System Admin")
        {
            List<string> systemMenu = new List<string>
            {
                "Create a new Staff",
                "View Staff",
                "Edit Staff",
                "Setup a new Restaurant",
                "Setup Restaurant Menu",
                "Exit",
            };
            do
            {
                menuCounter = 0;
                foreach (var sysMenu in systemMenu)
                {
                    menuCounter++;
                    Console.WriteLine($"{menuCounter} {sysMenu}");
                }
                var menuSelection = Console.ReadLine();
                if (menuSelection == "1")
                {
                    try
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
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nSomething went wrong: {ex.Message}");
                    }
                }
                else if (menuSelection == "2")
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

                        try
                        {
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
                        catch (Exception ex)
                        {
                            Console.WriteLine($"\nSomething went wrong: {ex.Message}");
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
                else if (menuSelection == "3")
                {
                    Console.WriteLine("\nEnter Staff Id to edit");
                    int id = Convert.ToInt32(Console.ReadLine());

                    try
                    {
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
                            Console.WriteLine($"Current Designation: {staffToEdit.Designation}");
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
                                    "No changes made. Please enter at least one detail to update."
                                );
                                continue;
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
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Something went wrong: {ex.Message}");
                    }
                }
                else if (menuSelection == "6")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid menu selection.");
                }
            } while (true);
        }
    }
}

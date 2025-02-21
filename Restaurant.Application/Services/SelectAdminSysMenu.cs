using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Services;

public class MenuSelectionHandler
{
    private readonly SysMenu sysMenu;

    public MenuSelectionHandler(SysMenu sysMenu)
    {
        this.sysMenu = sysMenu;
    }

    public async Task HandleStaffManagement(int subChoice)
    {
        switch (subChoice)
        {
            case 1:
                await CreateStaff();
                break;
            case 2:
                await ViewStaff();
                break;
            case 3:
                await EditStaff();
                break;
            default:
                Console.WriteLine("Invalid selection.");
                break;
        }
    }

    private async Task CreateStaff()
    {
        try
        {
            Console.WriteLine(
                "Enter staff details (staff id, first name, last name, designation, password)"
            );
            var newStaffInfo = Console.ReadLine();
            var splitStaffInfo = newStaffInfo.Trim().Split(',');
            if (splitStaffInfo.Count() != 5)
            {
                Console.WriteLine(
                    $"Incorrect number of details! You entered {splitStaffInfo.Count()}, but 5 are required."
                );
                Console.WriteLine();
                await CreateStaff();
                return;
            }
            var newStaffData = new Staff
            {
                StaffId = Convert.ToInt32(splitStaffInfo[0]),
                FirstName = splitStaffInfo[1],
                LastName = splitStaffInfo[2],
                Designation = splitStaffInfo[3],
                Password = splitStaffInfo[4],
            };
            newStaffData.CreateStaff();
            Console.WriteLine("Staff list");
            Console.WriteLine(
                "------------------------------------------------------------------------------------------------------------"
            );
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(
                String.Format(
                    "{0, 4}\t {1, -30}\t {2, -30}\t {3, -30}",
                    "StaffId",
                    "First Name",
                    "Last Name",
                    "Designation"
                )
            );
            Console.WriteLine(
                "------------------------------------------------------------------------------------------------------------"
            );
            var staffList = AppDb.StaffTable.Values.ToList();
            foreach (var record in staffList)
            {
                Console.WriteLine(
                    String.Format(
                        "{0, 4}\t {1, -30}\t {2, -30}\t {3, -30}",
                        record.StaffId,
                        record.FirstName,
                        record.LastName,
                        record.Designation
                    )
                );
            }
            Console.WriteLine("Do you want to create another staff? (Yes/No)");
            if (Console.ReadLine().Trim().Equals("YES", StringComparison.OrdinalIgnoreCase))
            {
                await CreateStaff();
                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}. Please try again.\n");
            await CreateStaff();
            return;
        }
    }

    private async Task ViewStaff()
    {
        try
        {
            Console.WriteLine(
                "Do you want to \n\tA. View a specific staff \n\tB. View all the staff?\n\nChoose A or B"
            );
            Console.Write("Enter choice: ");
            string adminInput = Console.ReadLine();
            if (string.Equals(adminInput, "A", StringComparison.OrdinalIgnoreCase))
            {
                while (true)
                {
                    Console.Write("Enter the staff ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int staffIdToView))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.\n");
                        continue;
                    }
                    var viewStaff = new Staff().ViewStaff(staffIdToView);
                    if (viewStaff != null)
                    {
                        Console.WriteLine(
                            $"Staff Details:\nStaffId: {viewStaff.StaffId}\nFirst Name: {viewStaff.FirstName}\nLast Name: {viewStaff.LastName}\nDesignation: {viewStaff.Designation}"
                        );
                        Console.WriteLine("Staff Details Retrieved Successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Staff ID not found.\n");
                        continue;
                    }
                    Console.Write("Do you want to view another staff? (Yes/No): ");
                    if (Console.ReadLine().Trim().Equals("YES", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    else
                        break;
                }
            }
            else if (string.Equals(adminInput, "B", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("View all staff");
                Console.WriteLine("Staff List:");
                Console.WriteLine(
                    "------------------------------------------------------------------------------------------------------------"
                );
                Console.WriteLine();
                Console.WriteLine(
                    String.Format(
                        "{0, 4}\t {1, -30}\t {2, -30}\t {3, -30}",
                        "StaffId",
                        "First Name",
                        "Last Name",
                        "Designation"
                    )
                );
                Console.WriteLine(
                    "------------------------------------------------------------------------------------------------------------"
                );
                var staffList = new Staff().ViewAllStaff();
                foreach (var record in staffList)
                {
                    Console.WriteLine(
                        String.Format(
                            "{0, 4}\t {1, -30}\t {2, -30}\t {3, -30}",
                            record.StaffId,
                            record.FirstName,
                            record.LastName,
                            record.Designation
                        )
                    );
                }
            }
            else
            {
                Console.WriteLine("Invalid selection. Please choose A or B.\n");
                await ViewStaff();
                return;
            }
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine($"Staff ID not found. Try again.\n");
            await ViewStaff();
            return;
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid format! Please enter numbers where required.\n");
            await ViewStaff();
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}. Please try again.\n");
            await ViewStaff();
            return;
        }
    }

    private async Task EditStaff()
    {
        try
        {
            Console.Write("Enter the staff ID to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int staffIdToEdit))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.\n");
                await EditStaff();
                return;
            }
            if (!AppDb.StaffTable.ContainsKey(staffIdToEdit))
            {
                Console.WriteLine("Staff ID not found. Try again.\n");
                await EditStaff();
                return;
            }
            var staffToEdit = AppDb.StaffTable[staffIdToEdit];
            Console.WriteLine(
                $"Editing details for {staffToEdit.FirstName} {staffToEdit.LastName}"
            );
            Console.WriteLine(
                "What do you want to edit? Choose from the list below (separated by a comma) \n1. First Name\n2. Last Name\n3. Password\n4. Designation\n5. Cancel"
            );
            var editSelection = Console.ReadLine();
            var selectedFields = editSelection.Trim().Split(',');
            if (selectedFields.Contains("5"))
            {
                if (selectedFields.Length > 1) // If "5" is selected along with any other number
                {
                    Console.WriteLine("Returning to main menu...");
                    return;
                }
                else
                {
                    Console.WriteLine("cancelling operation...");
                    return;
                }
            }
            if (selectedFields.Length == 0 || string.IsNullOrWhiteSpace(editSelection))
            {
                Console.WriteLine("Pick a value. Try again!\n");
                await EditStaff();
                return;
            }

            var staffInfoToEdit = new Staff { StaffId = staffIdToEdit };

            foreach (var field in selectedFields)
            {
                switch (field.Trim())
                {
                    case "1":
                        Console.Write("Enter the new first name: ");
                        staffInfoToEdit.FirstName = Console.ReadLine();
                        break;
                    case "2":
                        Console.Write("Enter the new last name: ");
                        staffInfoToEdit.LastName = Console.ReadLine();
                        break;
                    case "3":
                        Console.Write("Enter the new password: ");
                        staffInfoToEdit.Password = Console.ReadLine();
                        break;
                    case "4":
                        Console.Write("Enter the new designation: ");
                        staffInfoToEdit.Designation = Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("\nInvalid selection. Try again!\n");
                        await EditStaff();
                        continue;
                }
            }

            new Staff().EditStaff(staffIdToEdit, staffInfoToEdit);
            Console.WriteLine("Staff details updated successfully.");

            Console.Write("Do you want to edit another staff? (Yes/No): ");
            if (Console.ReadLine().Trim().Equals("YES", StringComparison.OrdinalIgnoreCase))
            {
                await EditStaff();
                return;
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid format! Please enter numbers where required.\n");
            await EditStaff();
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}. Please try again.\n");
            await EditStaff();
            return;
        }
    }
}

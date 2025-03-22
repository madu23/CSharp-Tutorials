using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Utilities;

namespace Restaurant.Application.Services;

/// <summary>
/// Handles staff management operations such as creating, viewing, and editing staff members.
/// </summary>

public class StaffManagementService
/// <summary>
/// Manages staff operations based on user selection.
/// </summary>
{
    public async Task HandleStaffManagement(int subChoice)
    {
        switch ((StaffMenuAction)subChoice)
        {
            case StaffMenuAction.Create:
                await CreateStaff();
                break;
            case StaffMenuAction.View:
                await ViewStaff();
                break;
            case StaffMenuAction.Edit:
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
            string fieldNames = string.Join(
                ", ",
                Enum.GetValues(typeof(Field))
                    .Cast<Field>()
                    .Where(f => f != Field.Cancel) // Exclude Cancel
                    .Select(f => f.ToString())
            );

            Console.WriteLine($"Enter staff details ({fieldNames}), separated by a comma:");
            var newStaffInfo = Console.ReadLine();
            if (string.IsNullOrEmpty(newStaffInfo))
            {
                Console.WriteLine("No input provided. Please try again.\n");
                await CreateStaff();
                return;
            }
            Console.WriteLine($"You entered: {newStaffInfo}");
            var splitStaffInfo = newStaffInfo.Trim().Split(',');
            int expectedFieldsCount = Enum.GetValues(typeof(Field)).Length - 1;
            if (splitStaffInfo.Count() != expectedFieldsCount)
            {
                Console.WriteLine(
                    $"Incorrect number of details! You entered {splitStaffInfo.Count()}, but {expectedFieldsCount} are required."
                );
                Console.WriteLine();
                await CreateStaff();
                return;
            }
            Console.WriteLine("Are you sure you want to create this staff? (Yes/Cancel)");
            var confirmation = UserInputHandler.GetUserConfirmation(Console.ReadLine().Trim());
            if (confirmation == CancelOperation.Cancel || confirmation == CancelOperation.No)
            {
                Console.WriteLine("Staff creation cancelled.\n");
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
            Console.WriteLine("Staff List");
            PrintHandler.PrintStaffList(new Staff().ViewAllStaff());

            bool repeat = await UserInputHandler.AskToRepeat("create", CreateStaff);
            if (!repeat)
                return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}. Please try again.\n");
            await CreateStaff();
            return;
        }
    }

    private static ViewStaffOption GetViewStaffOption(string input)
    {
        switch (input.Trim().ToUpper())
        {
            case "A":
                return ViewStaffOption.ViewOne;
            case "B":
                return ViewStaffOption.ViewAll;
            case "C":
                return ViewStaffOption.Cancel;
            default:
                return ViewStaffOption.Invalid;
        }
    }

    private async Task ViewStaff()
    {
        try
        {
            Console.WriteLine(
                "Do you want to \n\tA. View a specific staff \n\tB. View all the staff?\n\tC. Cancel\n\nChoose one of the above\n"
            );
            Console.Write("Enter choice: ");
            string adminInput = Console.ReadLine();
            switch (GetViewStaffOption(adminInput))
            {
                case ViewStaffOption.ViewOne:
                    Console.Write("Enter the staff ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int staffIdToView))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.\n");
                        await ViewStaff();
                        return;
                    }
                    var viewStaff = new Staff().ViewStaff(staffIdToView);
                    if (viewStaff != null)
                    {
                        Console.WriteLine(
                            $"\nStaff Details:\nStaffId: {viewStaff.StaffId}\nFirst Name: {viewStaff.FirstName}\nLast Name: {viewStaff.LastName}\nDesignation: {viewStaff.Designation}\n"
                        );
                        Console.WriteLine("Staff Details Retrieved Successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Staff ID not found.\n");
                        await ViewStaff();
                        return;
                    }

                    bool repeat = await UserInputHandler.AskToRepeat("view", ViewStaff);
                    if (!repeat)
                        return;
                    break;

                case ViewStaffOption.ViewAll:
                    Console.WriteLine("View all staff");
                    Console.WriteLine("Staff List:");
                    PrintHandler.PrintStaffList(new Staff().ViewAllStaff());
                    break;

                case ViewStaffOption.Cancel:
                    Console.WriteLine("Returning to main menu...");
                    return;

                case ViewStaffOption.Invalid:
                default:
                    Console.WriteLine("Invalid selection. Try again!\n");
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

    private static Field? GetEditField(string input)
    {
        if (int.TryParse(input.Trim(), out int choice) && Enum.IsDefined(typeof(Field), choice))
        {
            return (Field)choice;
        }
        return null;
    }

    private async Task EditStaff()
    {
        try
        {
            Console.Write("\nEnter the staff ID to edit: ");
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
            while (true)
            {
                Console.WriteLine(
                    "What do you want to edit? Choose from the list below (separated by a comma) \n1. First Name\n2. Last Name\n3. Password\n4. Designation\n5. Cancel"
                );

                var editSelection = Console.ReadLine().Trim();
                var selectedFields = editSelection.Trim().Split(',');

                var editChoices = selectedFields
                    .Select(x => GetEditField(x))
                    .Where(x => x.HasValue) // Filter out invalid entries
                    .Select(x => x.Value)
                    .ToList();
                if (editChoices.Contains(Field.Cancel))
                {
                    if (selectedFields.Length > 1)
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
                    continue;
                }
                var staffInfoToEdit = new Staff { StaffId = staffIdToEdit };
                bool validator = false;
                foreach (var field in editChoices)
                {
                    switch (field)
                    {
                        case Field.FirstName:
                            Console.Write("Enter the new first name: ");
                            staffInfoToEdit.FirstName = Console.ReadLine();
                            validator = true;
                            break;
                        case Field.LastName:
                            Console.Write("Enter the new last name: ");
                            staffInfoToEdit.LastName = Console.ReadLine();
                            validator = true;
                            break;
                        case Field.Password:
                            Console.Write("Enter the new password: ");
                            staffInfoToEdit.Password = Console.ReadLine();
                            validator = true;
                            break;
                        case Field.Designation:
                            Console.Write("Enter the new designation: ");
                            staffInfoToEdit.Designation = Console.ReadLine();
                            validator = true;
                            break;
                        default:
                            Console.WriteLine("\nInvalid selection. Try again!\n");
                            validator = false;
                            break;
                    }
                }
                if (!validator)
                    continue;
                new Staff().EditStaff(staffIdToEdit, staffInfoToEdit);
                Console.WriteLine("Staff details updated successfully.");
                break;
            }

            bool repeat = await UserInputHandler.AskToRepeat("edit", EditStaff);
            if (!repeat)
                return;
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
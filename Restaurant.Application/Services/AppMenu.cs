using Restaurant.Domain.Entities;
using Restaurant.Domain.Db;

namespace Restaurant.Application.Services;

public class AppMenu
{
    public readonly Staff _staff;

    public AppMenu(Staff staff)
    {
        _staff = staff;
    }

    public async Task<bool> DisplayMainMenu()
    {
        try
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nMain Menu");
                Console.WriteLine("1. Staff Management");
                Console.WriteLine("2. Restaurant Management");
                Console.WriteLine("3. Exit");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await DisplayStaffMenu();
                        break;
                    case "2":
                        await DisplayRestaurantMenu();
                        break;
                    case "3":
                        if (await ConfirmExit())
                            return true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DisplayStaffMenu()
    {
        try
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nStaff Management Menu");
                Console.WriteLine("1.Create Staff");
                Console.WriteLine("2. View Staff");
                Console.WriteLine("3. Edit Staff");
                Console.WriteLine("4. Back to Main Menu");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":  // Create Staff
                        try
                        {
                            Console.WriteLine("Enter staff details (staff id, first name, last name, designation, password)");
                            var newStaffInfo = Console.ReadLine();
                            var splitStaffInfo = newStaffInfo.Split(',');
                            var newStaffData = new Staff
                            {
                                StaffId = Convert.ToInt32(splitStaffInfo[0]),
                                FirstName = splitStaffInfo[1].Trim(),
                                LastName = splitStaffInfo[2].Trim(),
                                Designation = splitStaffInfo[3].Trim(),
                                Password = splitStaffInfo[4].Trim()
                            };
                            newStaffData.CreateStaff();

                            // Display updated staff list
                            Console.WriteLine("\nStaff List Updated");
                            Console.WriteLine("===================");
                            Console.WriteLine(String.Format("{0}\t {1}\t {2}\t {3}", "StaffId", "First Name", "Last Name", "Designation"));
                            Console.WriteLine("========================================================================");
                            var staffList = AppDb.StaffTable.Values.ToList();
                            foreach (var record in staffList)
                            {
                                Console.WriteLine($"{record.StaffId,-8} {record.FirstName,-15} {record.LastName,-15} {record.Designation,-15}");
                            }
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Something went wrong: {ex.Message}");
                            Console.ReadKey();
                        }
                        break;

                    case "2": // View Staff
                        try
                        {
                            Console.WriteLine("Enter staff id to view:");
                            var staffIdToView = Convert.ToInt32(Console.ReadLine());

                            var staffToView = new Staff().ViewStaff(staffIdToView);
                            Console.WriteLine("\nStaff Details");
                            Console.WriteLine("===================");
                            Console.WriteLine(String.Format("{0}\t {1}\t {2}\t {3}", "StaffId", "First Name", "Last Name", "Designation"));
                            Console.WriteLine("========================================================================");
                            Console.WriteLine($"{staffToView.StaffId,-8} {staffToView.FirstName,-15} {staffToView.LastName,-15} {staffToView.Designation,-15}");
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Something went wrong: {ex.Message}");
                            Console.ReadKey();
                        }
                        break;

                    case "3": // Edit Staff
                        try
                        {
                            Console.WriteLine("Enter staff id to edit:");
                            var staffIdToEdit = Convert.ToInt32(Console.ReadLine());

                            var existingStaff = new Staff().ViewStaff(staffIdToEdit);

                            Console.WriteLine("Enter new staff details (first name, last name, designation, password)");
                            Console.WriteLine($"Current values: {existingStaff.FirstName}, {existingStaff.LastName}, {existingStaff.Designation}");

                            var updateInfo = Console.ReadLine();
                            var splitUpdateInfo = updateInfo.Split(',');

                            var updatedStaff = new Staff
                            {
                                StaffId = staffIdToEdit,
                                FirstName = splitUpdateInfo[0].Trim(),
                                LastName = splitUpdateInfo[1].Trim(),
                                Designation = splitUpdateInfo[2].Trim(),
                                Password = splitUpdateInfo[3].Trim()
                            };

                            existingStaff.EditStaff(updatedStaff);
                            Console.WriteLine("Staff updated successfully!");
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Something went wrong: {ex.Message}");
                            Console.ReadKey();
                        }
                        break;

                    case "4":
                        if (await ConfirmExit())
                            return true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> DisplayRestaurantMenu()
    {
        try
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nRestaurant Management Menu");
                Console.WriteLine("1. Menu Setup");
                Console.WriteLine("2. Menu Item Setup");
                Console.WriteLine("3. Back to Main Menu");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Menu setup logic will be implemented
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "2":
                        // Menu item setup logic will be implemented
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "3":
                        if (await ConfirmExit())
                            return true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }


    public async Task<bool> ConfirmExit()
    {
        Console.WriteLine("\nAre you sure you want to go back? (Y/N)");
        return Console.ReadLine()?.ToUpper() == "Y";
    }
}









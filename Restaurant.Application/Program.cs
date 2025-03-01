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

        while (true)
        {
            Console.WriteLine("Select a system menu from the list below");
            int menuCounter = 0;
            if (loginResult?.Designation == "System Admin")
            {
                List<string> systemMenu = new List<string> { "Create a new Staff", "View Staff", "Edit Staff", "Delete Staff", "Setup a new Restaurant", "Setup Restaurant Menu", "Exit" };
                foreach (var sysMenu in systemMenu)
                {
                    menuCounter++;
                    Console.WriteLine($"{menuCounter} {sysMenu}");
                }
            }
            var menuSelection = Console.ReadLine();
            switch (menuSelection)
            {
                case "1":
                    CreateStaff();
                    break;
                case "2":
                    ViewStaff();
                    break;
                case "3":
                    EditStaff();
                    break;
                case "4":
                    DeleteStaff();
                    break;
                case "7":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid selection");
                    break;
            }
        }
    }

    static void CreateStaff()
    {
        try
        {
            Console.WriteLine("Enter staff details (staff id, first name, last name, designation, password)");
            var newStaffInfo = Console.ReadLine();
            var splitStaffInfo = newStaffInfo.Split(',');
            var newStaffData = new Staff
            {
                StaffId = Convert.ToInt32(splitStaffInfo[0]),
                FirstName = splitStaffInfo[1],
                LastName = splitStaffInfo[2],
                Designation = splitStaffInfo[3],
                Password = splitStaffInfo[4]
            };
            newStaffData.CreateStaff();
            Console.WriteLine("Staff created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something went wrong: {ex.Message}");
        }
    }

    static void ViewStaff()
    {
        Console.WriteLine("Staff list");
        Console.WriteLine("===================");
        Console.WriteLine();
        Console.WriteLine(String.Format("{0}\t {1}\t {2}\t {3}", "StaffId", "First Name", "Last Name", "Designation"));
        Console.WriteLine("========================================================================");
        var staffList = AppDb.StaffTable.Values.ToList();
        foreach (var record in staffList)
        {
            Console.WriteLine(String.Format("{0}\t {1}\t {2}\t {3}", record.StaffId, record.FirstName, record.LastName, record.Designation));
        }
    }

    static void EditStaff()
    {
        try
        {
            Console.WriteLine("Enter the staff id to edit:");
            var staffId = Convert.ToInt32(Console.ReadLine());
            var staff = AppDb.StaffTable.Where(_ => _.Value.StaffId == staffId).SingleOrDefault().Value;
            if (staff == null)
            {
                Console.WriteLine("Staff not found.");
                return;
            }

            // Store original values
            var originalFirstName = staff.FirstName;
            var originalLastName = staff.LastName;
            var originalDesignation = staff.Designation;
            var originalPassword = staff.Password;

            Console.WriteLine("\nWhat would you like to edit?");
            Console.WriteLine("1. First Name");
            Console.WriteLine("2. Last Name");
            Console.WriteLine("3. Designation");
            Console.WriteLine("4. Password");
            Console.WriteLine("5. All Fields");

            Console.Write("\nEnter your choice (1-5): ");
            var choice = Convert.ToInt32(Console.ReadLine());

            try
            {
                // Store the values before modification
                var newFirstName = staff.FirstName;
                var newLastName = staff.LastName;
                var newDesignation = staff.Designation;
                var newPassword = staff.Password;

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter new First Name: ");
                        newFirstName = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Enter new Last Name: ");
                        newLastName = Console.ReadLine();
                        break;
                    case 3:
                        Console.Write("Enter new Designation: ");
                        newDesignation = Console.ReadLine();
                        break;
                    case 4:
                        Console.Write("Enter new Password: ");
                        newPassword = Console.ReadLine();
                        break;
                    case 5:
                        Console.WriteLine("Enter new details (first name, last name, designation, password)");
                        var newStaffInfo = Console.ReadLine();
                        var splitStaffInfo = newStaffInfo.Split(',');
                        newFirstName = splitStaffInfo[0];
                        newLastName = splitStaffInfo[1];
                        newDesignation = splitStaffInfo[2];
                        newPassword = splitStaffInfo[3];
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        return;
                }

                // First remove the current staff record
                Staff.DeleteStaff(staffId);

                // Check for duplicates before saving
                bool isDuplicate = AppDb.StaffTable.Values.Any(s =>
                    s.FirstName.Trim().Equals(newFirstName.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    s.LastName.Trim().Equals(newLastName.Trim(), StringComparison.OrdinalIgnoreCase));

                if (isDuplicate)
                {
                    // If duplicate found, restore the original record
                    var originalStaff = new Staff
                    {
                        StaffId = staffId,
                        FirstName = originalFirstName,
                        LastName = originalLastName,
                        Designation = originalDesignation,
                        Password = originalPassword
                    };
                    originalStaff.CreateStaff();
                    throw new Exception($"A staff member with the name {newFirstName} {newLastName} already exists.");
                }

                // Create new staff record with updated values
                var updatedStaff = new Staff
                {
                    StaffId = staffId,
                    FirstName = newFirstName,
                    LastName = newLastName,
                    Designation = newDesignation,
                    Password = newPassword
                };
                updatedStaff.CreateStaff();
                Console.WriteLine("Staff updated successfully.");
            }
            catch (Exception)
            {
                // Restore original values if not already restored
                if (!AppDb.StaffTable.ContainsKey(staffId))
                {
                    var originalStaff = new Staff
                    {
                        StaffId = staffId,
                        FirstName = originalFirstName,
                        LastName = originalLastName,
                        Designation = originalDesignation,
                        Password = originalPassword
                    };
                    originalStaff.CreateStaff();
                }
                throw; // Re-throw the exception to be caught by outer try-catch
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something went wrong: {ex.Message}");
        }
    }
    static void DeleteStaff()
    {
        try
        {
            Console.WriteLine("Enter the staff id to delete:");
            var staffId = Convert.ToInt32(Console.ReadLine());
            var staff = AppDb.StaffTable.Where(_ => _.Value.StaffId == staffId).SingleOrDefault().Value;
            if (staff == null)
            {
                Console.WriteLine("Staff not found.");
                return;
            }

            Staff.DeleteStaff(staffId);
            Console.WriteLine("Staff deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something went wrong: {ex.Message}");
        }
    }
}
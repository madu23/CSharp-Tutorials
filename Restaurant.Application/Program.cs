using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Welcome to Eke Tech Restaurant!");

        // when application starts up, verify you have an admin account already setup.
        var adminStaff = AppDb.StaffTable.Where(_ => _.Value.Designation == "System Admin").FirstOrDefault();
        if (adminStaff.Value == null)
        {
            // meaning there is no admin. So go ahead and create one
            var newAdminStaff = new Staff
            {
                StaffId = 1,
                FirstName = "System",
                LastName = "Admin",
                Designation = "System Admin",
                Password = "superPassword"
            };

            newAdminStaff.CreateStaff();
        }

        Console.WriteLine("Enter your login details");
        Console.WriteLine("Staff Id:");
        var staffId = Convert.ToInt16(Console.ReadLine());

        Console.WriteLine("Password:");
        var password = Console.ReadLine();
        var staff = AppDb.StaffTable.Where(_ => _.Value.StaffId == staffId && _.Value.Password == password).SingleOrDefault().Value;
        if (staff is null)
        {
            Console.WriteLine("Invalid Credential");
            return;
        }

        Console.WriteLine($"Welcome {staff.FirstName} {staff.LastName}");

        while (true)
        {
            Console.WriteLine("Select a system menu from the list below");
            int menuCounter = 0;
            if (staff.Designation == "System Admin")
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

            Console.WriteLine("Enter new details (first name, last name, designation, password)");
            var newStaffInfo = Console.ReadLine();
            var splitStaffInfo = newStaffInfo.Split(',');
            staff.FirstName = splitStaffInfo[0];
            staff.LastName = splitStaffInfo[1];
            staff.Designation = splitStaffInfo[2];
            staff.Password = splitStaffInfo[3];
            staff.CreateStaff();
            Console.WriteLine("Staff updated successfully.");
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
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
       var result =  await seedDbTask.SeedAdminRecord();
        if(result == false)
        {
            Console.WriteLine("Admin data was not successfully pre-created");
        }

        // Login Step 2
        var authService = new AuthService();
        var loginResult = await authService.Login();

        //show main menu step 3
        var MainMenuDb = new MainMenuTask();
        var menuResult = await MainMenuDb.showMenu();
       





        Console.WriteLine($"Welcome {loginResult?.FirstName} {loginResult?.LastName}");
        Console.WriteLine($"Select a system menu from the list below");

        int menuCounter = 0;
        if(loginResult?.Designation == "System Admin")
        {
            List<string> systemMenu = new List<string> { "Create a new Staff", "View Staff", "Edit Staff", "Setup a new Restaurant", "Setup Restaurant Menu" };
            foreach (var sysMenu in systemMenu)
            {
                menuCounter++;
                Console.WriteLine($"{menuCounter} {sysMenu}");
            }
        }
        var menuSelection = Console.ReadLine();
        if (menuSelection == "1") 
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
                Console.WriteLine("Staff list");
                Console.WriteLine("===================");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(String.Format("{0}\t {1}\t {2}\t {3}", "StaffId", "First Name", "Last Name", "Designation"));
                Console.WriteLine("========================================================================");
                var staffList = AppDb.StaffTable.Values.ToList();
                foreach (var record in staffList)
                {
                    Console.WriteLine(String.Format("{0}\t {1}\t {2}\t {3}", record.StaffId, record.FirstName, record.LastName, record.Designation));
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Something went wrong: {ex.Message}");
            }

        }

       else if(menuSelection == "2")
       {
            Console.WriteLine("enter staff you want to view id");
            var viewstaffid = Console.ReadLine();
            var getStaffid= AppDb.StaffTable;
            var staffList = AppDb.StaffTable.Values.ToList();
                foreach (var record in staffList)
                {
                    Console.WriteLine(String.Format("{0}\t {1}\t {2}\t {3}", record.StaffId, record.FirstName, record.LastName, record.Designation));
                }
       }
        Console.ReadLine();
    }

}

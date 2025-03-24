<<<<<<< HEAD
﻿using Restaurant.Application.Services;
=======
﻿
using Restaurant.Application.Services;
>>>>>>> ed7dc5dd83c1de243b7a64f0c029b42d5192c598
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application;

class Program
{
<<<<<<< HEAD
=======

>>>>>>> ed7dc5dd83c1de243b7a64f0c029b42d5192c598
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, Welcome to Eke Tech Restaurant!");
        Console.WriteLine("=======================================");

        // Seed default admin data step 1
        var seedDbTask = new StartupTask();
        var result = await seedDbTask.SeedAdminRecord();
        if (result == false)
        {
            Console.WriteLine("Admin data was not successfully pre-created");
        }

        // Login Step 2
        var authService = new AuthService();
        var loginResult = await authService.Login();

<<<<<<< HEAD
        if (loginResult?.Designation == "System Admin")
        {
            // If logged in as System Admin, display the System Admin menu
            Console.WriteLine("You are logged in as a System Admin");
            Console.WriteLine($"Welcome {loginResult?.FirstName} {loginResult?.LastName}");
            Console.WriteLine($"Select a system menu from the list below");

            // Create a new instance of AppMenu object
            var appMenu = new AppMenu();
            var selectionTask = new SelectionTask();
            int mainMenuSelection;
            do
            {
                // Get the user's selection from the System Admin Main Menu
                mainMenuSelection = appMenu.GetMenuSelectionIndex("System Admin Main Menu");
                var continueLoop = await selectionTask.HandleSelection(mainMenuSelection);
                if (!continueLoop)
                {
                    Console.WriteLine("Exiting...");
                    return;
                }
            } while (true);
        }
        else
        {
            // If not logged in as System Admin, display an access denied message
            Console.WriteLine("You do not have access to system admin functions.");
=======
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine($"Welcome {loginResult?.FirstName} {loginResult?.LastName}");
        Console.WriteLine("========================================================================");

        Console.WriteLine($"Select a system menu from the list below");


        // Use AppMenu service if user is admin
        if (loginResult?.Designation == "System Admin")
        {
            var appMenu = new AppMenu(loginResult);
            await appMenu.DisplayMainMenu();
        }
        else
        {
            Console.WriteLine("Access Denied. System Admin access required.");
        }

        {
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
                Console.WriteLine("===================");
                Console.WriteLine("Staff updated successfully!");

                // Display updated staff list
                Console.WriteLine("Updated Staff List");
                Console.WriteLine("===================");
                Console.WriteLine(String.Format("{0}\t {1}\t {2}\t {3}", "StaffId", "First Name", "Last Name", "Designation"));
                Console.WriteLine("========================================================================");
                var staffList = AppDb.StaffTable.Values.ToList();
                foreach (var record in staffList)
                {


                    Console.WriteLine($"{record.StaffId,-8} {record.FirstName,-15} {record.LastName,-15} {record.Designation,-15}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex.Message}");
            }
>>>>>>> ed7dc5dd83c1de243b7a64f0c029b42d5192c598
        }
        Console.ReadLine();
    }
}

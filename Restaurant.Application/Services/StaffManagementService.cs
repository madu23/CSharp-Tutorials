using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Application.Services;

public class StaffManagementService
{
    private Dictionary<int, Staff> _staffMembers = new Dictionary<int, Staff>();
    public void ManageStaff()
    {
        bool returnToMainMenu = false;
        while (!returnToMainMenu)
        {
            Console.WriteLine("\n--- Staff Management ---");
            Console.WriteLine("1. Create a new Staff");
            Console.WriteLine("2. View Staff");
            Console.WriteLine("3. Edit Staff");
            Console.WriteLine("4. Delete Staff");
            Console.WriteLine("5. Return to Main Menu");

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
                case "5":
                    returnToMainMenu = true;
                    break;
                default:
                    Console.WriteLine("Invalid selection");
                    break;
            }
        }
    }

    private void CreateStaff()
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

    private void ViewStaff()
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

    private void EditStaff()
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

    private void DeleteStaff()
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
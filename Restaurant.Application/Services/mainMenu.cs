using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Services;
public class MainMenuTask
{
   public Task<bool> showMenu()
    {
        
        Console.WriteLine($"Select a system menu from the list below");

       Console.WriteLine("Main menu");
       Console.WriteLine("1. Staff Management");
       Console.WriteLine("2. Restaurant Menu");
    var menuSelection = Console.ReadLine();
      if (menuSelection == "1")
      {
            Console.WriteLine("Staff Management Menu");
            Console.WriteLine("1. View Staff");
            Console.WriteLine("2. Create Staff");
            Console.WriteLine("3. Edit Staff");
            Console.WriteLine("4. Back to main menu");
            var staffMenuSelection = Console.ReadLine();
            if (staffMenuSelection == "1")
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
            else if (staffMenuSelection == "2")
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
                
            }
            else if (staffMenuSelection == "3")
            {
                Console.WriteLine("Enter staff id to edit");
                var staffId = Console.ReadLine();
                var staff = AppDb.StaffTable[Convert.ToInt32(staffId)];
                Console.WriteLine("Enter new staff details (staff id, first name, last name, designation, password)");
                var newStaffInfo = Console.ReadLine();
                var splitStaffInfo = newStaffInfo.Split(',');
                staff.StaffId = Convert.ToInt32(splitStaffInfo[0]);
                staff.FirstName = splitStaffInfo[1];
                staff.LastName = splitStaffInfo[2];
                staff.Designation = splitStaffInfo[3];
                staff.Password = splitStaffInfo[4];

                
                
            } 
            else if (staffMenuSelection == "4")
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(true); 
            }
            

            return Task.FromResult(true);
         }
        else if (menuSelection == "2")
        {
            Console.WriteLine("Restaurant Menu");
            Console.WriteLine("1. Menu Setup");
            Console.WriteLine("2. Menu Item Setup");
            Console.WriteLine("3. Back to main menu");
            var restaurantMenuSelection = Console.ReadLine();
            return Task.FromResult(true);
        }
        else
        {
           return Task.FromResult(true);
        } 

    }
}



    
       
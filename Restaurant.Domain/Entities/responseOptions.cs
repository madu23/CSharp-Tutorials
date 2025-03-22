using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Domain.Entities
{
    public static class DisplayResponse
    {
        public static void DisplayStaffDetails(Staff staff)
        {
            Console.WriteLine(
                string.Format(
                    "{0, -10} {1, -15} {2, -15} {3, -20}",
                    staff.StaffId,
                    staff.FirstName,
                    staff.LastName,
                    staff.Designation
                )
            );
        }

        public static void DisplayStaffList(Dictionary<int, Staff> staffTable)
        {
            Console.WriteLine();
            Console.WriteLine();
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
            var staffList = staffTable.Values.ToList();
            foreach (var record in staffList)
            {
                Console.WriteLine(
                    string.Format(
                        "{0, -10} {1, -15} {2, -15} {3, -20}",
                        record.StaffId,
                        record.FirstName,
                        record.LastName,
                        record.Designation
                    )
                );
            }
        }

        public static async Task DisplayUnderDevelopmentMessage()
        {
            // Display a message indicating that the feature is under development
            string message = "Restaurant Menu is still under development !!!";
            foreach (var word in message.Split(' '))
            {
                Console.Write(word + " ");
                await Task.Delay(300);
            }
            Console.WriteLine();
        }

        public static async Task DisplayInvalidSelectionMessage()
        {
            Console.WriteLine("Invalid selection. Please try again.");
            await Task.CompletedTask;
        }
    }
}

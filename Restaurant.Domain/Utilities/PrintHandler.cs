using System;
using System.Collections.Generic;
using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Utilities
{
    /// <summary>
    /// Provides utility methods for printing data.
    /// </summary>
    public static class PrintHandler
    {
        /// <summary>
        /// Prints a formatted list of staff members to the console.
        /// </summary>
        /// <param name="staffList">The list of staff members to print.</param>
        public static void PrintStaffList(List<Staff> staffList)
        {
            // Print header separator
            Console.WriteLine(
                "---------------------------------------------------------------------------------------------------------"
            );
            Console.WriteLine();

            // Print table header with formatted columns
            Console.WriteLine(
                string.Format(
                    "{0, 4}\t {1, -30}\t {2, -30}\t {3, -30}",
                    "StaffId",
                    "First Name",
                    "Last Name",
                    "Designation"
                )
            );

            // Print another separator after the header
            Console.WriteLine(
                "---------------------------------------------------------------------------------------------------------"
            );

            // Iterate through the list and print each staff member’s details in a formatted manner
            foreach (var record in staffList)
            {
                Console.WriteLine(
                    string.Format(
                        "{0, 4}\t {1, -30}\t {2, -30}\t {3, -30}",
                        record.StaffId,
                        record.FirstName,
                        record.LastName,
                        record.Designation
                    )
                );
            }
        }
    }
}

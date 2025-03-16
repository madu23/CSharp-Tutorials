using System;
using System.Collections.Generic;
using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Handlers
{
    public class StaffHandler
    {
        public void CreateStaff(Staff staff)
        {
            // Logic to create a new staff record in the database
            AppDb.StaffTable[staff.StaffId] = staff;
        }

        public void EditStaff(int id, Staff updatedStaff)
        {
            // Logic to update an existing staff record in the database
            if (AppDb.StaffTable.ContainsKey(id))
            {
                AppDb.StaffTable[id] = updatedStaff;
            }
            else
            {
                throw new Exception("Staff not found.");
            }
        }

        public void DeleteStaff(int id)
        {
            // Logic to delete a staff record from the database
            if (AppDb.StaffTable.ContainsKey(id))
            {
                AppDb.StaffTable.Remove(id);
            }
            else
            {
                throw new Exception("Staff not found.");
            }
        }

        public Staff ViewStaff(int id)
        {
            // Logic to retrieve a staff record from the database
            AppDb.StaffTable.TryGetValue(id, out var staff);
            return staff;
        }
    }
}
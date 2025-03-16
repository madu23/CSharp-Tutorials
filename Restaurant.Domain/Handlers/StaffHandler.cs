using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Domain.Handlers;

public class StaffHandler
{
    public void CreateStaff(Staff staff)
    {
        // Check if the staff already exists
        var existingStaff = AppDb
            .StaffTable.Where(_ => _.Key == staff.StaffId)
            .FirstOrDefault()
            .Value;
        if (existingStaff != null)
            throw new InvalidOperationException(
                $"Staff {existingStaff.StaffId} with {existingStaff.FirstName} {existingStaff.LastName} already exist"
            );

        // Add staff to the database if they don't exist
        AppDb.StaffTable.Add(staff.StaffId, staff);
    }

    public Staff? ViewStaff(int id)
    {
        // Retrieve the staff details for the given ID
        if (AppDb.StaffTable.TryGetValue(id, out var staff))
        {
            return staff;
        }
        return null;
    }

    public Staff EditStaff(int id, Staff updatedStaff)
    {
        // Retrieve the staff details for the given ID
        var staffToEdit = AppDb.StaffTable[id];
        if (staffToEdit == null)
            throw new EntityNotFoundException($"Staff with id {id} does not exist");

        // Update the staff details
        staffToEdit.FirstName = updatedStaff.FirstName;
        staffToEdit.LastName = updatedStaff.LastName;
        staffToEdit.Designation = updatedStaff.Designation;
        staffToEdit.Password = updatedStaff.Password;

        return staffToEdit;
    }

    public void DeleteStaff(int id)
    {
        // Check if the staff exists and delete the staff with the given ID
        if (AppDb.StaffTable.ContainsKey(id))
        {
            AppDb.StaffTable.Remove(id);
        }
        else
        {
            throw new EntityNotFoundException($"Staff with id {id} does not exist");
        }
    }
}

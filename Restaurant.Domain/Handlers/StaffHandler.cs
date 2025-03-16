using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Domain.Handlers;

public class StaffHandler
{
    public void CreateStaff(Staff staff)
    {
        // check if the staff does not exist
        var existingStaff = AppDb
            .StaffTable.Where(_ => _.Key == staff.StaffId)
            .FirstOrDefault()
            .Value;
        if (existingStaff != null)
            throw new InvalidOperationException(
                $"Staff {existingStaff.StaffId} with {existingStaff.FirstName} {existingStaff.LastName} already exist"
            );

        // add staff to the database if they don't exists
        AppDb.StaffTable.Add(staff.StaffId, staff);
    }

    public Staff? ViewStaff(int id)
    {
        if (AppDb.StaffTable.TryGetValue(id, out var staff))
        {
            return staff;
        }
        return null;
    }

    public Staff EditStaff(int id, Staff updatedStaff)
    {
        var staffToEdit = AppDb.StaffTable[id];
        if (staffToEdit == null)
            throw new EntityNotFoundException($"Staff with id {id} does not exist");

        staffToEdit.FirstName = updatedStaff.FirstName;
        staffToEdit.LastName = updatedStaff.LastName;
        staffToEdit.Designation = updatedStaff.Designation;
        staffToEdit.Password = updatedStaff.Password;

        return staffToEdit;
    }

    public void DeleteStaff(int id)
    {
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

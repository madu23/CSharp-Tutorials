using Restaurant.Domain.Db;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Domain.Entities;

/// <summary>
/// This is an object that represent a restaurant staff. All staff record will be stored against this object
/// </summary>
public class Staff
{
    public int StaffId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Designation { get; set; } = null!;
    public string Password { get; set; } = null!;

    /// <summary>
    /// Creates a new staff record in the database.
    /// </summary>
    public void CreateStaff()
    {
        // Check if the staff already exists
        var staff = AppDb.StaffTable.Where(_ => _.Key == this.StaffId).FirstOrDefault().Value;
        if (staff != null)
            throw new InvalidOperationException(
                $"Staff {staff.StaffId} with {staff.FirstName} {staff.LastName} already exist"
            );

        // Add staff to the database if they don't exist
        AppDb.StaffTable.Add(this.StaffId, this);
        return;
    }

    /// <summary>
    /// Retrieves the staff details for the given ID.
    /// The staff details.
    /// </summary>

    public Staff ViewStaff(int id)
    {
        // Retrieve the staff details for the given ID
        var staff = AppDb.StaffTable[id];
        if (staff == null)
            throw new EntityNotFoundException($"Staff with id {id} does not exist");
        return staff;
    }

    /// <summary>
    /// Edits the staff details for the given ID.
    /// </summary>

    public Staff EditStaff(int id, Staff staff)
    {
        // Retrieve the staff details for the given ID
        var staffToEdit = AppDb.StaffTable[id];
        if (staffToEdit == null)
            throw new EntityNotFoundException($"Staff with id {id} does not exist");

        // Update the staff details
        staffToEdit.FirstName = staff.FirstName;
        staffToEdit.LastName = staff.LastName;
        staffToEdit.Designation = staff.Designation;
        staffToEdit.Password = staff.Password;

        return staffToEdit;
    }
}

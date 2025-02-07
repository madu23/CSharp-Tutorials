
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


    public void CreateStaff()
    {
        // check if the staff does not exist
        var staff = AppDb.StaffTable.Where(_ => _.Key == this.StaffId).FirstOrDefault().Value;
        if (staff != null)
            throw new InvalidOperationException($"Staff {staff.StaffId} with {staff.FirstName} {staff.LastName} already exist");

        // add staff to the database if they don't exists
        AppDb.StaffTable.Add(this.StaffId, this);
        return;

    }

    public Staff ViewStaff(int id)
    {
        var staff = AppDb.StaffTable[id];
        if (staff == null) throw new EntityNotFoundException($"Staff with id {id} does not exist");
        return staff;

    }
    public void EditStaff(Staff updatedStaff)
    {
        var staff = AppDb.StaffTable.GetValueOrDefault(this.StaffId);
        if (staff == null)
            throw new EntityNotFoundException($"Staff with id {this.StaffId} does not exist");

        staff.FirstName = updatedStaff.FirstName;
        staff.LastName = updatedStaff.LastName;
        staff.Designation = updatedStaff.Designation;
        staff.Password = updatedStaff.Password;
    }



}
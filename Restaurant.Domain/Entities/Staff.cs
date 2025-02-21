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
            throw new InvalidOperationException(
                $"Staff {staff.StaffId} with {staff.FirstName} {staff.LastName} already exist"
            );

        // add staff to the database if they don't exists
        AppDb.StaffTable.Add(this.StaffId, this);
        return;
    }

    public Staff ViewStaff(int id)
    {
        var staff = AppDb.StaffTable[id];
        if (staff == null)
            throw new EntityNotFoundException($"Staff with id {id} does not exist");
        return staff;
    }

    public List<Staff> ViewAllStaff()
    {
        return AppDb.StaffTable.Values.ToList();
    }

    public Staff EditStaff(int staffId, Staff staffInfo)
    {
        var staff = AppDb.StaffTable[staffId];
        if (staff == null)
            throw new EntityNotFoundException($"Staff {StaffId} doesnt exist");

        if (staffInfo.FirstName != null)
            staff.FirstName = staffInfo.FirstName;
        if (staffInfo.LastName != null)
            staff.LastName = staffInfo.LastName;
        if (staffInfo.Designation != null)
            staff.Designation = staffInfo.Designation;
        if (staffInfo.Password != null)
            staff.Password = staffInfo.Password;
        AppDb.StaffTable[staffId] = staff;
        return staff;
    }
}

using System;

namespace restaurant;
public class Staff
{
    public int staffId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set;} = null!;
    public string Designation { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Staff create(int staffid , string FirstName, string LastName , string designation , string password);
    {
        public bool update(int id, string FirstName , string LastName , string Designation , string password )
        {
            var staff = _staffList.FirstorDefault(s => s.id==id);
            if (staff==null)
            return false;

            staff.staffId = staffId
            staff.FirstName = FirstName
            staff.LastName = LastName
            staff.Designation = Designation
            staff.Password = password
            return true;
        }
        public bool Delete(int id)
        {
            var staff = staffList.FirstorDefault(s =>s.id==id);
            if (staff==null)
            return false;
            _staffList.Remove(staff);
            return true;
        }
    }
}



    





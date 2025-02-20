
using Restaurant.Domain.Db;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Services;
public class StartupTask
{
    public Task<bool> SeedAdminRecord()
    {
        try
        {
            // when application starts up, verify you have an admin account already setup.
            var adminStaff = AppDb.StaffTable.Where(_ => _.Value.Designation == "System Admin").FirstOrDefault();
            if (adminStaff.Value == null)
            {
                // meaning there is no admin. So go ahead and create one
                var newAdminStaff = new Staff
                {
                    StaffId = 1,
                    FirstName = "System",
                    LastName = "Admin",
                    Designation = "System Admin",
                    Password = "superPassword"
                };

                newAdminStaff.CreateStaff();
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(true);
            }
        }
        catch (Exception ex)
        {
            return Task.FromResult(false);
        }
    }

    public void SayHello()
    {
        SeedAdminRecord();
    }
}

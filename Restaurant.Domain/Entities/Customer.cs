
using Restaurant.Domain.Db;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Domain.Entities;

/// <summary>
/// This is an object that represent a restaurant staff. All staff record will be stored against this object
/// </summary>
public class Customer
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string sex { get; set; } = null!;
    public string Password { get; set; } = null!;


    public void CreateCustomer()
    {
        // check if the staff does not exist
        var customer = AppDb.CustomerTable.Where(_ => _.Key == this.CustomerId).FirstOrDefault().Value;
        if (customer != null)
            throw new InvalidOperationException($"Customer {customer.CustomerId} with {customer.FirstName} {customer.LastName} already exist");

        // add staff to the database if they don't exists
        AppDb.CustomerTable.Add(customer.CustomerId, customer);
        return;

    }

    public Customer ViewStaff(int id)
    {
        var customer = AppDb.CustomerTable[id];
        if (customer == null) throw new EntityNotFoundException($"Staff with id {id} does not exist");
        return customer;

    }
}
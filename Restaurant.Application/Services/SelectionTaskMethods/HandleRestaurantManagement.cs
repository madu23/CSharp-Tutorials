using System;
using System.Threading.Tasks;
using Restaurant.Domain.Handlers;

namespace Restaurant.Application.Services.SelectionTaskMethods
{
    public class HandleRestaurantManagement
    {
        // Private field to hold the MenuHandler instance
        private readonly MenuHandler _menuHandler;

        // Constructor to initialize the MenuHandler instance
        public HandleRestaurantManagement()
        {
            _menuHandler = new MenuHandler();
        }

        // Method to execute the restaurant management task
        public async Task Execute()
        {
            // Create an instance of RestaurantManagement and execute the task
            var restaurantManagement = new RestaurantManagement(_menuHandler);
            await restaurantManagement.Execute();
        }
    }
}

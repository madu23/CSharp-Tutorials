using System;
using System.Threading.Tasks;
using Restaurant.Domain.Handlers;

namespace Restaurant.Application.Services.SelectionTaskMethods
{
    public class HandleRestaurantManagement
    {
        private readonly MenuHandler _menuHandler;

        public HandleRestaurantManagement()
        {
            _menuHandler = new MenuHandler();
        }

        public async Task Execute()
        {
            var restaurantManagement = new RestaurantManagement(_menuHandler);
            await restaurantManagement.Execute();
        }
    }
}

namespace Restaurant.Domain.Entities
{
    /// <summary>
    /// Defines constant values for menu options
    /// </summary>
    public static class ResponseOptions
    {
        public static class MainMenu
        {
            public const int StaffManagement = 1;
            public const int RestaurantManagement = 2;
            public const int Exit = 3;
        }

        public static class StaffManagement
        {
            public const int CreateStaff = 1;
            public const int ViewStaff = 2;
            public const int EditStaff = 3;
            public const int Exit = 4;
        }

        public static class RestaurantManagement
        {
            public const int MenuSetup = 1;
            public const int MenuItemSetup = 2;
            public const int Exit = 3;
        }
    }
}
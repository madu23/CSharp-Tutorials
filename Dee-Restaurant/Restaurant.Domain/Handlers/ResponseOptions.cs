using System;

namespace Restaurant.Domain.Handlers
{
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
            public const int ViewStaff = 1;
            public const int CreateStaff = 2;
            public const int EditStaff = 3;
            public const int DeleteStaff = 4;
            public const int Exit = 5;
        }

        public static class RestaurantManagement
        {
            public const int MenuSetup = 1;
            public const int MenuItemSetup = 2;
            public const int Exit = 3;
        }

        public static class ViewStaff
        {
            public const string SpecificStaff = "1";
            public const string AllStaffs = "2";
        }
    }
}
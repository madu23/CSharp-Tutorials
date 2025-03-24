namespace Restaurant.Domain.Entities
{
    public class ResponseOptions
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
            public const int SpecificStaff = 1;
            public const int AllStaffs = 2;
            public const int Cancel = 3;
        }

        public static class CreateStaff
        {
            public const int EnterStaffId = 1;
            public const int EnterFirstName = 2;
            public const int EnterLastName = 3;
            public const int EnterDesignation = 4;
            public const int EnterPassword = 5;
            public const int Cancel = 6;
        }

        public static class EditStaff
        {
            public const int EnterStaffId = 1;
            public const int EnterNewFirstName = 2;
            public const int EnterNewLastName = 3;
            public const int EnterNewDesignation = 4;
            public const int EnterNewPassword = 5;
            public const int Cancel = 6;
        }

        public static class DeleteStaff
        {
            public const int EnterStaffId = 1;
            public const int Cancel = 2;
        }

        public static class AuthService
        {
            public const int EnterStaffId = 1;
            public const int EnterPassword = 2;
            public const int Cancel = 3;
        }
    }
}

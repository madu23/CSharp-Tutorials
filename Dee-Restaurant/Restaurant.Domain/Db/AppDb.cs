using System.Collections.Generic;
using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Db
{
    public static class AppDb
    {
        public static Dictionary<int, Staff> StaffTable { get; set; } = new Dictionary<int, Staff>();
    }
}
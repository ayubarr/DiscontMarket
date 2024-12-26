namespace DiscontMarket.Services.Helpers.Constants
{
    public static class AdminInfo
    {
        public static string adminName = "Admin";
        public static string Email = "admin@admin.com";
        public static uint Id = 0;
        public static string Password = "P@ssw0rd!";

        public static string FirstName = adminName;
        public static string LastName = adminName;
        public static string MiddleName = adminName;
        public static string UserName = adminName;
        public static string NormalizedUserName = adminName.Normalize();
        public static string NormalizedEmail = Email.Normalize();
    }
}

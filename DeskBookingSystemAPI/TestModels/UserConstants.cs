namespace DeskBookingSystemAPI.TestModels
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel()
            {
                Username = "Admin",
                EmailAddress= "admin@email.com",
                Password = "password123",
                GivenName = "Mikolaj",
                Surname = "Blaszczyk",
                Role="Admin"
            },
            new UserModel()
            {
                Username = "Employee",
                EmailAddress = "employee@email.com",
                Password = "password123",
                GivenName = "John",
                Surname = "Doe",
                Role="Employee"
            }
        };
    }
}

namespace DeskBookingSystemAPI.TestModels
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel()
            {
                ID= 2,
                Username = "Admin",
                Password = "password123",
                Role="Admin"
            },
            new UserModel()
            {
                ID= 1,
                Username = "Employee",
                Password = "password123",
                Role="Employee"
            }
        };
    }
}

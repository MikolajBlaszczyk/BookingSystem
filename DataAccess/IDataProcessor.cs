using DataAccess.Models;

namespace DataAccess
{
    public interface IDataProcessor
    {
        Task AddDesk(string position, int monitors, int locationID);
        Task AddLocation(string address, string city);
        Task DeleteDesk(int deskID);
        Task DeleteLocation(int locationID);
        Task DeleteReservation(int reservationID);
        Task<string> GetAllDesks();
        Task<string> GetAllLocations();
        Task<string> GetAllReservationsAdmin();
        Task<string> GetAllReservationsEmployee();
        Task<List<UserModel>> GetAllUsers();
        Task<string> GetSpecifiedDesk(int deskID);
        Task InsertDeskReservation(int userID, int deskID, string startDate, string endDate);
        Task UpdateDesk(bool reserved, int deskID);
        Task UpdateNumberOfDesks(int numberOfDesks, int locationID);
    }
}
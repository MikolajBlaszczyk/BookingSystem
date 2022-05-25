
namespace DataAccess
{
    internal interface IInsertData
    {
        IDataAccess DataAccess { get; set; }

        Task InsertDesk(string position, int monitors, int locationID, bool isReserved = false, int? UserID = null);
        Task InsertLocation(string address, string city, int numberOfDesks = 0, bool allReserved = false);
        Task InsertReservation(int userID, int deskID, string startDate, string endDate);
    }
}
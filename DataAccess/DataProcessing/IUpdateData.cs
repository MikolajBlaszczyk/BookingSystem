
namespace DataAccess
{
    public interface IUpdateData
    {
        IDataAccess DataAccess { get; set; }

        Task DeskUpdateAsync(bool reserved, int DeskId);
        Task LocAllReservedUpdateAsync(bool reserved, int locationID);
        Task LocNumberUpdateAsync(int numberOfDesk, int locationID);
    }
}
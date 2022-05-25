using DataAccess.Models;

namespace DataAccess
{
    internal interface IGetData
    {
        IDataAccess DataAccess { get; set; }

        Task<List<DeskModel>> GetAllDesksAsync();
        Task<List<LocationModel>> GetAllLocationsAsync();
        Task<List<ReservationModel>> GetAllReservations();
        Task<List<ReservationModel>> GetAllReservationsEmployee();
        Task<List<UserModel>> GetAllUsersAsync();
        Task<List<DeskModel>> GetSpecifiedDesk(int id);
        Task<List<LocationModel>> GetSpecifiedLocationAsync(int id);
        Task<List<UserModel>> GetSpecifiedUserAsync(int id);
    }
}
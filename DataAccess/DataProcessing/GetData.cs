using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class GetData : IGetData
    {
        public IDataAccess DataAccess { get; set; }
        public GetData()
        {
            DataAccess = new DataAccess();
        }

        public async Task<List<DeskModel>> GetAllDesksAsync()
        {
            string command = "exec [dbo].[spGetAllDesks]";
            var output = await DataAccess.GetDataAsync<DeskModel>(command);
            return output;
        }

        public async Task<List<DeskModel>> GetSpecifiedDesk(int id)
        {
            string command = "exec [dbo].[spGetSpecifiedDesk] @ID";
            var output = await DataAccess.GetDataAsync<DeskModel, object>(command, new { ID = id });
            return output;
        }

        public async Task<List<LocationModel>> GetAllLocationsAsync()
        {
            string command = "exec [dbo].[spGetAllLocations]";
            var output = await DataAccess.GetDataAsync<LocationModel>(command);
            return output;
        }

        public async Task<List<LocationModel>> GetSpecifiedLocationAsync(int id)
        {
            string command = "exec [dbo].[spGetSpecifiedLocation] @ID";
            var output = await DataAccess.GetDataAsync<LocationModel, object>(command, new { ID = id });
            return output;
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            string command = "exec [dbo].[spGetAllUsers]";
            var output = await DataAccess.GetDataAsync<UserModel>(command);
            return output;
        }

        public async Task<List<UserModel>> GetSpecifiedUserAsync(int id)
        {
            string command = "exec [dbo].[spGetSpecifiedUser] @ID";
            var output = await DataAccess.GetDataAsync<UserModel, object>(command, new { ID = id });
            return output;
        }

        public async Task<List<ReservationModel>> GetAllReservations()
        {
            string command = "exec dbo.spGetAllReservations";
            var output = await DataAccess.GetDataAsync<ReservationModel>(command);
            return output;
        }

        public async Task<List<ReservationModel>> GetAllReservationsEmployee()
        {
            string command = "exec dbo.spGetAllReservationEmployee";
            var output = await DataAccess.GetDataAsync<ReservationModel>(command);
            return output;
        }
    }
}

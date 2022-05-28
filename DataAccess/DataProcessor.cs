using DataAccess.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataProcessor : IDataProcessor
    {
        internal IGetData GetData { get; set; }
        internal IUpdateData UpdateData { get; set; }
        internal IInsertData InsertData { get; set; }

        internal IDeleteData DeleteData { get; set; }
        public DataProcessor(IGetData getData, IUpdateData updateData, IInsertData insertData, IDeleteData deleteData)
        {
            GetData = getData;
            UpdateData = updateData;
            InsertData = insertData;
            DeleteData = deleteData;
        }


        public async Task<string> GetAllLocations()
        {
            var obj = await GetData.GetAllLocationsAsync();
            string json = JsonConvert.SerializeObject(obj);
            return json;
        }

        public async Task UpdateDesk(bool reserved, int deskID)
        {
            await UpdateData.DeskUpdateAsync(reserved, deskID);
        }

        public async Task<string> GetAllDesks()
        {
            var obj = await GetData.GetAllDesksAsync();
            string json = JsonConvert.SerializeObject(obj);
            return json;
        }

        public async Task AddLocation(string address, string city)
        {
            await InsertData.InsertLocation(address, city);
        }

        public async Task AddDesk(string position, int monitors, int locationID)
        {
            await InsertData.InsertDesk(position, monitors, locationID);
        }

        public async Task DeleteLocation(int locationID)
        {
            await DeleteData.DeleteLocation(locationID);
        }

        public async Task DeleteDesk(int deskID)
        {
            await DeleteData.DeleteDesk(deskID);
        }

        public async Task DeleteReservation(int reservationID)
        {
            await DeleteData.DeleteReservation(reservationID);
        }

        public async Task<string> GetSpecifiedDesk(int deskID)
        {
            var obj = GetData.GetSpecifiedDesk(deskID);
            string json = JsonConvert.SerializeObject(obj);
            return json;
        }

        public async Task UpdateNumberOfDesks(int numberOfDesks, int locationID)
        {
            await UpdateData.LocNumberUpdateAsync(numberOfDesks, locationID);
        }

        public async Task<string> GetAllReservationsAdmin()
        {
            var obj = await GetData.GetAllReservations();
            string json = JsonConvert.SerializeObject(obj);
            return json;
        }

        public async Task<string> GetAllReservationsEmployee()
        {
            var obj = await GetData.GetAllReservationsEmployee();
            string json = JsonConvert.SerializeObject(obj);
            return json;
        }

        public async Task InsertDeskReservation(int userID, int deskID, string startDate, string endDate)
        {
            await InsertData.InsertReservation(userID, deskID, startDate, endDate);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var obj = await GetData.GetAllUsersAsync();
            return obj;
        }
    }
}

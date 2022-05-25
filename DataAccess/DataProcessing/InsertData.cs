using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class InsertData : IInsertData
    {

        //only for admins!!!
        public IDataAccess DataAccess { get; set; }
        public InsertData()
        {
            DataAccess = new DataAccess();
        }

        public async Task InsertLocation(string address, string city, int numberOfDesks = 0, bool allReserved = false) // last two shouldn't be touched for now
        {
            string command = "exec [dbo].[spInsertLocation] @Address, @City"; // for now only two params
            await DataAccess.PostDataAsync<object>(command, new { Address = address, City = city, NumberOfDesks = numberOfDesks, AllReserved = allReserved });
        }

        public async Task InsertDesk(string position, int monitors, int locationID, bool isReserved = false, int? UserID = null) // last two shouldn't be touched for now
        {
            string command = "exec [dbo].[spInsertDesk] @Position, @Monitors, @LocationID";// for now only three params
            await DataAccess.PostDataAsync<object>(command, new { Position = position, Monitors = monitors, LocationID = locationID });
        }

        public async Task InsertReservation(int userID, int deskID, string startDate, string endDate)
        {
            string command = "exec [dbo].[spInsertDeskReservation] @UserID,@DeskID, @StartDate, @EndDate";
            await DataAccess.PostDataAsync(command, new { UserID = userID, DeskID = deskID, StartDate = startDate, EndDate = endDate });
        }
    }
}

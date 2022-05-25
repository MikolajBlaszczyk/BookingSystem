using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class DeleteData : IDeleteData
    {
        public IDataAccess DataAccess { get; set; }
        public DeleteData()
        {
            DataAccess = new DataAccess();
        }

        public async Task DeleteLocation(int locationID)
        {
            string command = "exec [dbo].[spDeleteLocation] @ID";
            await DataAccess.PostDataAsync<object>(command, new { ID = locationID });
        }

        public async Task DeleteDesk(int deskID)
        {
            string command = "exec [dbo].[spDeleteDesk] @ID";
            await DataAccess.PostDataAsync<object>(command, new { ID = deskID });
        }

        public async Task DeleteReservation(int reservationID)
        {
            string command = "exec [dbo].[spDeleteReservation] @ID";
            await DataAccess.PostDataAsync(command, new { ID = reservationID });
        }
    }

}

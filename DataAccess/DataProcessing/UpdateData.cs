using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class UpdateData : IUpdateData
    {
        public IDataAccess DataAccess { get; set; }

        public UpdateData()
        {
            DataAccess = new DataAccess();
        }

        public async Task LocAllReservedUpdateAsync(bool reserved, int locationID)
        {
            string command = "exec [dbo].[spLocationUpdate] @AllReserved, @ID";
            await DataAccess.PostDataAsync<object>(command, new { AllReserved = reserved, ID = locationID }); // await?
        }

        public async Task LocNumberUpdateAsync(int numberOfDesk, int locationID)
        {
            string command = "exec  [dbo].[spLocationNumberUpdate] @NoDesk, @ID";
            await DataAccess.PostDataAsync<object>(command, new { NoDesk = numberOfDesk, ID = locationID }); // await? 
        }

        public async Task DeskUpdateAsync(bool reserved, int UserId, int DeskId)
        {
            string command = "exec [dbo].[spDeskUpdate] @Reserved, @UserID, @ID";
            await DataAccess.PostDataAsync<object>(command, new { Reserved = reserved, UserID = UserId, ID = DeskId });
        }
    }
}

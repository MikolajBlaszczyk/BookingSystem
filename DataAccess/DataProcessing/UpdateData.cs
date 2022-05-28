using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UpdateData : IUpdateData
    {
        public IDataAccess DataAccess { get; set; }

        public UpdateData(IDataAccess dataAccess)
        {
            DataAccess = dataAccess;
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

        public async Task DeskUpdateAsync(bool reserved, int DeskId)
        {
            string command = "exec [dbo].[spDeskUpdate] @Reserved, @ID";
            await DataAccess.PostDataAsync<object>(command, new { Reserved = reserved, ID = DeskId });
        }

    }
}

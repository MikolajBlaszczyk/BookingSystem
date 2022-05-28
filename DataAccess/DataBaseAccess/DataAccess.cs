using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataAccessProcess : IDataAccess
    {


        //Getting the Data with additional requirements
        public async Task<List<T>> GetDataAsync<T, U>(string command, U parameter)
        {
            using (IDbConnection cnn = new SqlConnection(@"Data Source=MIKOŁAJ\SQLEXPRESS;Initial Catalog=SoftwareMind;User ID=MikolajBlaszczyk;Password=408404")) // Give connection string
            {
                var output = await cnn.QueryAsync<T>(command, parameter);
                return output.ToList();
            }
        }

        //Getting all the Data
        public async Task<List<T>> GetDataAsync<T>(string command)
        {
            using (IDbConnection cnn = new SqlConnection(@"Data Source=MIKOŁAJ\SQLEXPRESS;Initial Catalog=SoftwareMind;User ID=MikolajBlaszczyk;Password=408404"))
            {
                var output = await cnn.QueryAsync<T>(command);
                return output.ToList();
            }
        }

        // Posting Data 
        public async Task PostDataAsync<T>(string command, T parameter)
        {
            using (IDbConnection cnn = new SqlConnection(@"Data Source=MIKOŁAJ\SQLEXPRESS;Initial Catalog=SoftwareMind;User ID=MikolajBlaszczyk;Password=408404")) // Give connection string 
            {
                await cnn.ExecuteAsync(command, parameter);
            }
        }

    }
}

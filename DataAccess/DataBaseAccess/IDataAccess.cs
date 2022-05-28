
namespace DataAccess
{
    public interface IDataAccess
    {
        Task<List<T>> GetDataAsync<T, U>(string command, U parameter);
        Task<List<T>> GetDataAsync<T>(string command);
        Task PostDataAsync<T>(string command, T parameter);
    }
}
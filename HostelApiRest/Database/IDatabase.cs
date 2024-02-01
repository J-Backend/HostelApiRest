using System.Data.SqlClient;

namespace HostelApiRest.Database
{
    public interface IDatabase
    {
        SqlConnection GetConnection();
    }
}
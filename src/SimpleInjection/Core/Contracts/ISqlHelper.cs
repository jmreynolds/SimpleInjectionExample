using System.Data.SqlClient;

namespace Core.Contracts
{
    public interface ISqlHelper
    {
        void Close();
        SqlDataReader GetDataReader(string sqlCommand);
        SqlDataReader GetDataReader();
        SqlCommand SelectCommand { get; }
    }
}
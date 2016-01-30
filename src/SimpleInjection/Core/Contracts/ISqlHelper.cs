using System;
using System.Data;

namespace Core.Contracts
{
    public interface ISqlHelper : IDisposable
    {
        void ExecuteNonQuery(string sqlQuery);
        void AddParam<T>(string key, T value);
        DataTable GetTable(string sSQL);
    }
    public interface ISqlHelperFactory
    {
        ISqlHelper GetSqlHelper();
    }
}
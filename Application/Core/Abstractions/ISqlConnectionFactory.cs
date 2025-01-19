using System.Data;

namespace Application.Core.Abstractions
{
    public interface IDBConnectionFactory
    {
        IDbConnection CreateConnection();
    }

}
using System.Data;

namespace CKK.DB.Interfaces
{
    public interface IConnectionFactory // Interface for connection
    {
        IDbConnection GetConnection { get; }
    }
}

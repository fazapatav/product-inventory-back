using System.Data;


namespace Sofka.ProductInventory.Core.Domain.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}

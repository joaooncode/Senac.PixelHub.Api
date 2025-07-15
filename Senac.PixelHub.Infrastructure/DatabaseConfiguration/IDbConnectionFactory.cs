using System.Data;

namespace Senac.PixelHub.Infrastructure.DatabaseConfiguration
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}

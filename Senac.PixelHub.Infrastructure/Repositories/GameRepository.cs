using Dapper;
using Senac.PixelHub.Domain.Entities;
using Senac.PixelHub.Domain.Repositories.Games


using Senac.PixelHub.Infrastructure.DatabaseConfiguration;



namespace Senac.PixelHub.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {

        private readonly IDbConnectionFactory _connectionFactory;

        public GameRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<GameEntity>> GetAllGames()
        {
            return await _connectionFactory.CreateConnection()
                .QueryAsync<GameEntity>(
                    @"SELECT id, title FROM games ORDER BY title"
                );
        }
    }
}

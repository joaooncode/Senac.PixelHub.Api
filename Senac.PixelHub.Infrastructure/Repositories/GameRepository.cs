using Dapper;
using Senac.PixelHub.Domain.Entities;
using Senac.PixelHub.Domain.Repositories.Games;


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
                    @"SELECT g.id, g.title, g.isAvailable, c.Name AS Category FROM Games g JOIN Categories c ON g.Category = c.Id ORDER BY title"
                );
        }

        public async Task<GameEntity> GetGameById(long id)
        {
            return await _connectionFactory.CreateConnection()
                .QueryFirstOrDefaultAsync<GameEntity>(
                    @"SELECT g.Id,
                             g.Title,
                             g.Description,
                             c.Name as Categories,
                             g.IsAvailable,
                             g.ReturnDate   
                               FROM Games g
                                INNER JOIN Categories c ON c.Id = g.Category
                                     WHERE g.Id = @Id", new { Id = id }
                );
        }

        public Task<GameEntity> RentGame(long id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateGame(GameEntity game)
        {
            throw new NotImplementedException();
        }
    }
}

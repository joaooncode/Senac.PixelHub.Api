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

        public async Task<long> CreateGame(GameEntity game)
        {
            return await _connectionFactory.CreateConnection()
                .QueryFirstOrDefaultAsync<long>(
                @"INSERT INTO Games(
                   Title,
                   Description,
                   Category,
                   Responsible,
                   IsAvailable,
                   ReturnDate
                ) OUTPUT INSERTED.Id
                  VALUES(
            @Title,
            @Description,
            @Category,
            NULL,
            1,
            NULL
        )", new
        {
            Title = game.Title,
            Description = game.Description,
            Category = (int)game.Category,
        }
);
        }

        public async Task DeleteGame(long id)
        {
             await _connectionFactory.CreateConnection()
                .QueryFirstOrDefaultAsync<GameEntity>(@"DELETE FROM Games WHERE Id = @Id", new
                {
                    Id= id
                });
        }

        public async Task<IEnumerable<GameEntity>> GetAllGames()
        {
            return await _connectionFactory.CreateConnection()
                .QueryAsync<GameEntity>(
                    @"SELECT g.Id, 
                     g.Title, 
                     g.Description,
                     g.IsAvailable, 
                     g.Responsible,
                     g.ReturnDate,
                     g.Category 
              FROM Games g 
              ORDER BY g.Title"
        );
        }

        public async Task<GameEntity> GetGameById(long id)
        {
            return await _connectionFactory.CreateConnection()
                .QueryFirstOrDefaultAsync<GameEntity>(
@"SELECT g.Id,
         g.Title,
         g.Description,
         c.Id as Category,
         g.IsAvailable,
         g.ReturnDate   
           FROM Games g
            INNER JOIN Categories c ON c.Id = g.Category
                 WHERE g.Id = @Id", new { Id = id }
                );
        }

        public async Task<GameEntity> RentGame(GameEntity game)
        {
            return await _connectionFactory.CreateConnection()
                .QueryFirstOrDefaultAsync<GameEntity>(
                    @"UPDATE Games SET 
                                    Responsible = @Responsible,
                                    IsAvailable = @IsAvailable, 
                                    ReturnDate = @ReturnDate  
                                        WHERE Id = @Id",
new
{
    Id = game.Id,
    Responsible = game.Responsible,
    IsAvailable = game.IsAvailable,
    ReturnDate = game.ReturnDate
}
                );
        }

        public async Task ReturnGame(long id)
        {
            await _connectionFactory.CreateConnection()
                .QueryFirstOrDefaultAsync<GameEntity>(@"UPDATE Games SET IsAvailable = 1, Responsible = NULL, ReturnDate = NULL WHERE Id = @Id", new
                {
                    Id =  id
                });
        }

        public async Task UpdateGame(GameEntity game)
        {
            await _connectionFactory.CreateConnection()
                .QueryFirstOrDefaultAsync<GameEntity>
                (
                    @"UPDATE Games SET 
                                    Title = @Title, 
                                    Description = @Description, 
                                    Category = @Category
                                        WHERE Id = @Id
", new{
    Id = game.Id,
    Title = game.Title,
    Description = game.Description,
    Category = (int)game.Category,
});

        }
    }
}
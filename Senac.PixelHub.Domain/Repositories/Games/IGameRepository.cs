using Senac.PixelHub.Domain.Entities;

namespace Senac.PixelHub.Domain.Repositories.Games
{
    public interface IGameRepository
    {
        Task DeleteGame(long id);

        Task<IEnumerable<GameEntity>> GetAllGames();

        Task<GameEntity> GetGameById(long id);

        Task UpdateGame(GameEntity game);

        Task<GameEntity> RentGame(GameEntity game);
        Task ReturnGame(long id);
        Task<long> CreateGame(GameEntity game);
    }
}

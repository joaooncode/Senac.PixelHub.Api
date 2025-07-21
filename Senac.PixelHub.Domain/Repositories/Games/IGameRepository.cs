using Senac.PixelHub.Domain.Entities;

namespace Senac.PixelHub.Domain.Repositories.Games
{
    public interface IGameRepository
    {
        Task<IEnumerable<GameEntity>> GetAllGames();

        Task<GameEntity> GetGameById(long id);

        Task UpdateGame(GameEntity game);

        Task<GameEntity> RentGame(long id);
    }
}

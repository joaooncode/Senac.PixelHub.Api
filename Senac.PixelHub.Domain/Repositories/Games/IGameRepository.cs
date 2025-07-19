using Senac.PixelHub.Domain.Entities;

namespace Senac.PixelHub.Domain.Repositories.Games
{
    public interface IGameRepository
    {
        Task<IEnumerable<GameEntity>> GetAllGames();
        Task<GameEntity> GetGameById(long id);
    }
}

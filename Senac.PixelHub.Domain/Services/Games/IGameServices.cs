
using Senac.PixelHub.Domain.Entities;
using Senac.PixelHub.Domain.Responses.Games;

namespace Senac.PixelHub.Domain.Services.Games
{
    public interface IGameServices
    {
        Task<IEnumerable<GetAllGamesResponse>>GetAllGames();


        Task<GetGameByIdResponse> GetGameById(long id);

        bool IsGameOverDue();

    }
}

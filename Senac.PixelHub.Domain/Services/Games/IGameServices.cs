
using Senac.PixelHub.Domain.DTO_S.Requests.Game;
using Senac.PixelHub.Domain.DTO_S.Responses;
using Senac.PixelHub.Domain.DTO_S.Responses.Games;
using Senac.PixelHub.Domain.Entities;
using Senac.PixelHub.Domain.Responses.Games;

namespace Senac.PixelHub.Domain.Services.Games
{
    public interface IGameServices
    {
        Task<CreateGameResponse> CreateGame(CreateGameRequest createGameRequest);
        
        Task DeleteGame(long id);
        Task<IEnumerable<GetAllGamesResponse>>GetAllGames();

        Task<GetGameByIdResponse> GetGameById(long id);

        Task<RentGameResponse>RentGame(long id, string responsible);

        Task<ReturnGameResponse> ReturnGame(long id);

        Task UpdateGame(long id, UpdateGameRequest updateGameRequest);

        bool IsGameOverDue();
    }
}

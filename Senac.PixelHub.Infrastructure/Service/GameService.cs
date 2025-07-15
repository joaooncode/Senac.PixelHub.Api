using Senac.PixelHub.Domain.Repositories.Games;
using Senac.PixelHub.Domain.Responses.Games;
using Senac.PixelHub.Domain.Services.Games;

namespace Senac.PixelHub.Infrastructure.Service
{
    public class GameService : IGameServices
    {
        private readonly IGameRepository _gameRepository;


        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }


        public async Task<IEnumerable<GetAllGamesResponse>> GetAllGames()
        {
           var games = await _gameRepository.GetAllGames();

            var getAllGamesResponse = games.Select(g => new GetAllGamesResponse { Title = g.Title, IsAvailable = g.IsAvailable, IsOverdue = g.IsOverdue, WithdrawalDate = g.WithdrawalDate });


            return getAllGamesResponse;
        }
    }
}

using Senac.PixelHub.Domain.Entities;
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

            var getAllGamesResponse = games.Select(g => new GetAllGamesResponse { Title = g.Title, IsAvailable = g.IsAvailable, Category = g.Category.ToString() });


            return getAllGamesResponse;
        }

        public async Task<GetGameByIdResponse> GetGameById(long id)
        {
            var game = await _gameRepository.GetGameById(id);

            if (game == null)
            {
                throw new ArgumentException($"No game found with the id {id}");
            }

            return new GetGameByIdResponse 
            { 
                Id = game.Id,
                Title = game.Title,
                Category = game.Category,
                Description = game.Description,
                IsAvailable = game.IsAvailable,
                Responsible = game.Responsible,
                ReturnDate = game.ReturnDate 
            };

        }

        public bool IsGameOverDue()
        {
            throw new NotImplementedException();
        }
    }
}

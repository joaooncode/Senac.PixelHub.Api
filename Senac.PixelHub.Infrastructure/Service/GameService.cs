using Senac.PixelHub.Domain.DTO_S.Requests.Game;
using Senac.PixelHub.Domain.Entities;
using Senac.PixelHub.Domain.Entities.Enums;
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
            var gamesTask = _gameRepository.GetAllGames();
            gamesTask.Wait();
            var games = gamesTask.Result;

            return games.Any(g => !g.IsAvailable && g.ReturnDate < DateTime.UtcNow);
        }

        public async Task<RentGameResponse> RentGame(long id, string responsible)
        {
            var game = await _gameRepository.GetGameById(id);

            if (game == null)
            {
                throw new ArgumentException($"Nenhum jogo encontrado com o id {id}");
            }

            if (!game.IsAvailable)
            {
                throw new Exception($"Este jogo já está alugado");
            }

            game.Responsible = responsible;

            game.IsAvailable = false;

            int diasEntrega = game.Category switch
            {
                CategoriesEnum.BRONZE => 9,
                CategoriesEnum.SILVER=> 6,
                CategoriesEnum.GOLD => 3,
                CategoriesEnum.DIAMOND => 2,

            };

            game.ReturnDate = DateTime.UtcNow.AddDays(diasEntrega);

            await _gameRepository.UpdateGame(game);


            return new RentGameResponse
            {
                Title = game.Title,
                Responsible = game.Responsible,
                ReturnDate = game.ReturnDate
            };
        }
    }
}

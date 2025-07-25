using Senac.PixelHub.Domain.DTO_S.Requests.Game;
using Senac.PixelHub.Domain.DTO_S.Responses;
using Senac.PixelHub.Domain.DTO_S.Responses.Games;
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

        public async Task<CreateGameResponse> CreateGame(CreateGameRequest createGameRequest)
        {
            bool isCategoryValid = Enum.TryParse(createGameRequest.Category, ignoreCase: true, out CategoriesEnum category);

            if (!isCategoryValid)
            {
                throw new Exception($"Categoria {createGameRequest.Category} inválida");

            }

            try
            {
                var game = new GameEntity
                {
                    Title = createGameRequest.Title,
                    Description = createGameRequest.Description,
                    Category = category
                };

                long gameIdResponse = await _gameRepository.CreateGame(game);

                var response = new CreateGameResponse
                {
                    Id = gameIdResponse,
                };
                
                return response;
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao criar jogo: {ex.Message}");
            }

          
        }

        public async Task DeleteGame(long id)
        {
            var game = await _gameRepository.GetGameById(id);

            if (game == null)
            {
                throw new Exception($"Nenhum jogo encontrado com id {id}");
            }

            await _gameRepository.DeleteGame(id);
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
                Category = game.Category.ToString(),
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

            int dueDays = game.Category switch
            {
                CategoriesEnum.BRONZE => 10,
                CategoriesEnum.SILVER=> 9,
                CategoriesEnum.GOLD => 8,
                CategoriesEnum.PLATINUM=> 7,
                CategoriesEnum.DIAMOND => 2,
            };

            game.ReturnDate = DateTime.UtcNow.AddDays(dueDays);

            await _gameRepository.RentGame(game);


            return new RentGameResponse
            {
                Title = game.Title,
                Responsible = game.Responsible,
                ReturnDate = game.ReturnDate
            };
        }

        public async Task<ReturnGameResponse> ReturnGame(long id)
        {
            var game = await _gameRepository.GetGameById(id);

           

            if (game == null)
            {
                throw new Exception($"Nenhum jogo encontrado com o id {id}");
            }

            if (game.IsAvailable)
            {
                throw new Exception($"O jogo {game.Title} não está alugado");
            }

            await _gameRepository.ReturnGame(id);

            return new ReturnGameResponse
            {
                IsAvailable = game.IsAvailable,
                Resposible = game.Responsible,
                ReturnDate = game.ReturnDate
            };

        }

        public async Task UpdateGame(long id, UpdateGameRequest updateGameRequest)
        {
            bool isCategoryValid = Enum.TryParse(updateGameRequest.Category, ignoreCase: true, out CategoriesEnum category);

            if (!isCategoryValid)
            {
                throw new Exception($"Categoria {updateGameRequest.Category}");
            }


            try
            {
                var game = await _gameRepository.GetGameById(id);

                if (game == null)
                {
                    throw new Exception($"Nenhum jogo encontrado com o id: {id}");
                }

                game.Description = updateGameRequest.Description;
                game.Category = category;
                game.Title = updateGameRequest.Title;


                await _gameRepository.UpdateGame(game);

            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao atualizar jogo: {ex.Message}");
            }
        }
    }
}

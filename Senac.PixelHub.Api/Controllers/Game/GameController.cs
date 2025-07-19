using Microsoft.AspNetCore.Mvc;
using Senac.PixelHub.Domain.Services.Games;
namespace Senac.PixelHub.Api.Controllers.Jogos;


[ApiController]
[Route("game")]
public class GameController : Controller
{
    private readonly IGameServices _gameService;

    public GameController(IGameServices gameService)
    {
        _gameService = gameService;
    }


    [HttpGet]

    public async Task<IActionResult> GetAllGames()
    {
        var getAllGamesResponse = await _gameService.GetAllGames();

        return Ok(getAllGamesResponse);
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetGameById(long id)
    {
        var getGameByIdResponse = await _gameService.GetGameById(id);

        return Ok(getGameByIdResponse);
    }
    

}

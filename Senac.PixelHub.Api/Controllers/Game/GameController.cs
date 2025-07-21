using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Senac.PixelHub.Domain.DTO_S.Requests.Game;
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


    [HttpPut("{id}/alugar")]

    public async Task<IActionResult> RentGame(long id, [FromBody] RentGameRequest rentGameRequest)
    {
        try
        {
            var rentGameResponse = await _gameService.RentGame(id, rentGameRequest.Responsible);

            return Ok(rentGameResponse);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPut("{id}/update")]

    public async Task<IActionResult> UpdateGame(long id, [FromBody] UpdateGameRequest updateGameRequest)
    {
        try
        {
            await _gameService.UpdateGame(id, updateGameRequest);
            return Ok();
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }

}

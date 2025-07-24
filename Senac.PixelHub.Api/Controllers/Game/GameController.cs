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


    [HttpPost("/create")]

    public async Task<IActionResult> CreateGame([FromBody] CreateGameRequest createGameRequest)
    {
        try
        {
            var createGameResponse = await _gameService.CreateGame(createGameRequest);

            return Ok(createGameResponse);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    [HttpPut("{id}/rent")]

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

    [HttpPut("{id}/return")]

    public async Task<IActionResult> ReturnGame(long id)
    {
        try
        {
            var returnGameResponse = await _gameService.ReturnGame(id);

            return Ok(returnGameResponse);

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
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

    [HttpDelete("{id}/delete")]

    public async Task<IActionResult> DeleteGame(long id)
    {
        try
        {
            await _gameService.DeleteGame(id);
            return Ok();
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

}

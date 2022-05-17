using Microsoft.AspNetCore.Mvc;
using SSHTicTacToe.DTO;
using SSHTicTacToe.Services;

namespace SSHTicTacToe.Controllers
{
    [ApiController]
    [Route("api/v1/games")]
    public class TicTacToeGamesController : ControllerBase
    {
        private readonly ILogger<TicTacToeGamesController> _logger;
        private readonly ITicTacToeGameService _ticTacToeGameService;

        public TicTacToeGamesController(
            ILogger<TicTacToeGamesController> logger, 
            ITicTacToeGameService ticTacToeGameService)
        {
            _logger = logger;
            _ticTacToeGameService = ticTacToeGameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGames()
        {
            List<TicTacToeGameDTO> gamesList = await _ticTacToeGameService.GetTicTacToeGameList();

            //Here normally an automated mapper will be used
            List <TicTacToeGameResponseDTO> gamesDTO = new();

            foreach(var item in gamesList)
            {
                gamesDTO.Add(new TicTacToeGameResponseDTO
                {
                    Id = item.Id,
                    Board = item.Board,
                    Status = item.Status,
                    UserIsCrosses = item.UserIsCrosses
                });
            }

            return Ok(gamesDTO);
        }

       [HttpPost]
        public async Task<IActionResult> CreateGame(TicTacToeGameCreationDTO ticTacToeGameCreationDTO)
        {
            var game = await _ticTacToeGameService.CreateTicTacToeGame(ticTacToeGameCreationDTO);
           //in case of error, the error middleware will catch and handle it
            return Ok(game);
        }
 
        [HttpGet("{game_id:Guid}")]
        public async Task<IActionResult> GetGameById(Guid game_id)
        {
           var game = await _ticTacToeGameService.GetTicTacToeGameById(game_id);

            if (game.isError)
            {
                return NotFound(game.Message);
            }
            //usually an auto mapper will be here
            return Ok(new TicTacToeGameResponseDTO { Id = game.Id, Board = game.Board, Status = game.Status});
        }
        [HttpPut("{game_id:Guid}")]
        public async Task<IActionResult> UpdateGame(Guid game_id, UpdateGameDTO updateGameDTO)
        {
            var game = await _ticTacToeGameService.UpdateTicTacToeGame(game_id, updateGameDTO);
            if (game.isError)
            {
                return BadRequest(game.Message);
            }
           //usually an auto mapper will be here
            return Ok(new TicTacToeGameResponseDTO { Id = game.TicTacToeGameDTO.Id, Board = game.TicTacToeGameDTO.Board, Status = game.TicTacToeGameDTO.Status, UserIsCrosses = game.TicTacToeGameDTO.UserIsCrosses});
        }

        [HttpDelete("{game_id:Guid}")]
        public async Task<IActionResult> DeleteGame(Guid game_id)
        {
            _ticTacToeGameService.DeleteTicTacToeGame(game_id);
            //in case or error the error middleware should handle it
            return Ok();
        }
    }
}
using SSHTicTacToe.DTO;

namespace SSHTicTacToe.Services
{
    public interface ITicTacToeGameService
    {
        public Task<List<TicTacToeGameDTO>> GetTicTacToeGameList();
        public Task<TicTacToeGameDTO> GetTicTacToeGameById(Guid uuid);
        public Task<CreateGameResponseDTO> CreateTicTacToeGame(TicTacToeGameCreationDTO ticTacToeGameCreationDTO);
        public Task<UpdateGameResponseDTO> UpdateTicTacToeGame(Guid uuid, UpdateGameDTO updateGameDTO);
        public void DeleteTicTacToeGame(Guid uuid);
    }
}

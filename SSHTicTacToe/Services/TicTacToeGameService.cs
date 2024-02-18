using Microsoft.AspNetCore.Mvc.Rendering;
using SSHTicTacToe.DTO;
using System.Runtime.InteropServices;

namespace SSHTicTacToe.Services
{
    public class TicTacToeGameService : ITicTacToeGameService
    {
        private readonly static List<TicTacToeGameDTO> _games = new List<TicTacToeGameDTO>();
        private readonly static string GAME_URL = "https://localhost:7211/api/v1/games/";
        public Task<List<TicTacToeGameDTO>> GetTicTacToeGameList()
        {
            return Task.FromResult(_games);
        }
        public async Task<TicTacToeGameDTO> GetTicTacToeGameById(Guid uuid)
        {
            
            var game = _games.Where(x => x.Id == uuid).FirstOrDefault();

            if (game == null)//error handling
            {
                game = new TicTacToeGameDTO();
                game.isError = true;
                game.Message = "No game found with this Id";
            }
           return game;
        }
        public async Task<CreateGameResponseDTO> CreateTicTacToeGame(TicTacToeGameCreationDTO newGame)
        {
            bool userHasAMove = false;
            int userMoveIndex = 0;
            bool userIsCrosses = false;

            char[] chBoard = newGame.Board.ToCharArray();
 
            //Checking if user made a first move and checking which pattern 'X' or 'O' has been chosen
            for(int i = 0; i < chBoard.Length; i++)
            {
                if(chBoard[i] != '-')
                {
                    userHasAMove = true;

                    if(chBoard[i] == 'x' || chBoard[i] == 'X')
                    {
                        userIsCrosses = true;
                    }
                    else
                    {
                        userIsCrosses = false;
                    }
                    userMoveIndex = i;
                    break;
                }
            }

            //Assigning PC move randomly in an empty position
            int locationIndex;
            if (userHasAMove == false)
            {
                locationIndex = new Random().Next(0, 9);
            }
            else
            {
                do
                {
                    locationIndex = new Random().Next(0, 9);
                } while (locationIndex == userMoveIndex);
            }

            chBoard[locationIndex] = userIsCrosses == true ? 'O' : 'X';

            //saving in cache
            var game = new TicTacToeGameDTO
            {
                Id = Guid.NewGuid(),
                Board = new string(chBoard),
                Status = GameStatues.RUNNING.ToString(),
                UserIsCrosses = userIsCrosses
            };

            _games.Add(game);

            return new CreateGameResponseDTO { Location = GAME_URL + game.Id };
        }

        public async Task<UpdateGameResponseDTO> UpdateTicTacToeGame(Guid uuid, UpdateGameDTO updateGameDTO)
        {
           char[] newBoard = updateGameDTO.Board.ToCharArray();
           var game = _games.Where(x => x.Id == uuid).FirstOrDefault();
           return await GameEngine(game, newBoard);
        }
      
        public void DeleteTicTacToeGame(Guid uuid)
        {
            _games.RemoveAll(x => x.Id == uuid);
        }

        #region helper methods
        private bool isGameDraw(char[] newBoard)
        {
            string strBoard = new string(newBoard);
            if (!strBoard.Contains('-'))
            {
                return true;
            }
            return false;
        }
        private Winner isGameWon(char[] chBoard)
        {
            if (CheckWinning(chBoard, 0, 1, 2))
            {
                return new Winner { isWin = true, WinnerName = chBoard[0] };
            }

            if (CheckWinning(chBoard, 3, 4, 5))
            {
                return new Winner { isWin = true, WinnerName = chBoard[3] };
            }

            if (CheckWinning(chBoard, 6, 7, 8))
            {
                return new Winner { isWin = true, WinnerName = chBoard[6] };
            }

            if (CheckWinning(chBoard, 0, 3, 6))
            {
                return new Winner { isWin = true, WinnerName = chBoard[0] };
            }

            if (CheckWinning(chBoard, 1, 4, 7))
            {
                return new Winner { isWin = true, WinnerName = chBoard[1] };
            }

            if (CheckWinning(chBoard, 2, 5, 8))
            {
                return new Winner { isWin = true, WinnerName = chBoard[2] };
            }

            if (CheckWinning(chBoard, 0, 4, 8))
            {
                return new Winner { isWin = true, WinnerName = chBoard[0] };
            }

            if (CheckWinning(chBoard, 2, 4, 6))
            {
                return new Winner { isWin = true, WinnerName = chBoard[2] };
            }

            return new Winner { isWin = false };

        }
        private bool CheckWinning(char[] chBoard, int pos1, int pos2, int pos3)
        {
            if (chBoard[pos1] == '-') return false;
            return chBoard[pos1].Equals(chBoard[pos2]) && chBoard[pos2].Equals(chBoard[pos3]);
        }
        /// <summary>
        /// Compare old & new Boards and check if more than one move or a move done in an already exist position done
        /// </summary>
        /// <param name="newBoard"></param>
        /// <param name="oldBoard"></param>
        /// <returns></returns>
        private bool CompareChangesInOldAndNewBoards(char[] newBoard, char[] oldBoard)
        {
            int moves = 0;
            for (int i = 0; i < newBoard.Length;i++)
            {
                if (newBoard[i] == oldBoard[i])
                    continue;
                else
                {
                    if(oldBoard[i] == '-') 
                        moves++;
                }
            }
            return moves == 1;
        }
        private Task<UpdateGameResponseDTO> GameEngine(TicTacToeGameDTO? game, char[] newBoard)
        {
            if (game != null)
            {
                char[] oldBoard = game.Board.ToCharArray();
                //make sure game's rules are followed correctly
                if (!CompareChangesInOldAndNewBoards(newBoard, oldBoard))
                {
                    return Task.FromResult(UpdateGameObj(true, "You inserted a mark in non empty position or more than one mark!", new TicTacToeGameDTO()));
                }

                //check if the user won the game
                var winner = isGameWon(newBoard);
                if (winner.isWin)
                {
                    game.Board = new string(newBoard);
                    game.Status = winner.WinnerName == 'X' || winner.WinnerName == 'x' ? GameStatues.X_WON.ToString() : GameStatues.O_WON.ToString();

                    return Task.FromResult(UpdateGameObj(false, "Game is Won!", game));
                }
                else //pc turn
                {
                    if (isGameDraw(newBoard))
                    {
                        game.Board = new string(newBoard);
                        game.Status = GameStatues.DRAW.ToString();

                        return Task.FromResult(UpdateGameObj(false, "Game is Draw!", game));
                    }
                    //PC make a move
                    int index;
                    do
                    {
                        index = new Random().Next(0, 9);
                    } while (newBoard[index] != '-');

                    newBoard[index] = game.UserIsCrosses == true ? 'O' : 'X';
                    //validate if PC won
                    winner = isGameWon(newBoard);
                    if (winner.isWin)
                    {
                        game.Board = new string(newBoard);
                        game.Status = winner.WinnerName == 'X' || winner.WinnerName == 'x' ? GameStatues.X_WON.ToString() : GameStatues.O_WON.ToString();

                        return Task.FromResult(UpdateGameObj(false, "Game is Won!", game));
                    }

                    game.Board = new string(newBoard);
                    game.Status = GameStatues.RUNNING.ToString();
                    return Task.FromResult(UpdateGameObj(false, "User Turn", game));
                }
            }
            else
            {
                return Task.FromResult(UpdateGameObj(true, "No such game with the uuid: " + game.Id + " provided", null));
            }
        }

        //helper method to reduce repetive code
        private UpdateGameResponseDTO UpdateGameObj(bool isError, string errorMessage, TicTacToeGameDTO game)
        {
            return new UpdateGameResponseDTO
            {
                Message = errorMessage,
                isError = isError,
                TicTacToeGameDTO = game
            };

        }

    #endregion
}
}

using SSHTicTacToe;
using SSHTicTacToe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.TEST.Fixture
{
    public static class GamesFixture
    {
        public static List<TicTacToeGameDTO> GetTestGames() => new()
        {
            new TicTacToeGameDTO
            {
                Board = "O--------",
                Id = Guid.NewGuid(),
                Status = GameStatues.RUNNING.ToString(),
                UserIsCrosses = false
                
            },
            new TicTacToeGameDTO
            {
                Board = "O--------",
                Id = Guid.NewGuid(),
                Status = GameStatues.RUNNING.ToString(),
                UserIsCrosses = false

            },
            new TicTacToeGameDTO
            {
                Board = "X--------",
                Id = Guid.NewGuid(),
                Status = GameStatues.RUNNING.ToString(),
                UserIsCrosses = true

            }

        };
    }
}

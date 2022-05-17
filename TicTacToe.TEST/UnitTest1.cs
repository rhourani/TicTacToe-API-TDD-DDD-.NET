using NUnit.Framework;
using SSHTicTacToe.DTO;
using SSHTicTacToe.Services;
using Xunit;

namespace TicTacToe.TEST
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }


        [Test]
        public void TestCreateGame()
        {
            TicTacToeGameCreationDTO game = new TicTacToeGameCreationDTO
            {
               Board = "---------"
            };
            TicTacToeGameService gameService = new();
            var result = gameService.CreateTicTacToeGame(game);

            Assert.AreNotEqual(new CreateGameResponseDTO(), result);

        }
    }
}
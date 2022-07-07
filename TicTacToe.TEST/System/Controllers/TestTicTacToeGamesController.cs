using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SSHTicTacToe.Controllers;
using SSHTicTacToe.DTO;
using SSHTicTacToe.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.TEST.Fixture;
using Xunit;

namespace TicTacToe.TEST.System.Controller
{
    public class TestTicTacToeGamesController
    {
        [Fact]
        public async Task GetGames_OnSuccsess_ReturnsStatusCode200()
        {
            //Arrange
            var mockTicTacToesService = new Mock<ITicTacToeGameService>();

            mockTicTacToesService
                .Setup(service => service.GetTicTacToeGameList())
                .ReturnsAsync(new List<TicTacToeGameDTO>());

            var sut = new TicTacToeGamesController(mockTicTacToesService.Object);

            //Act

           var result = (OkObjectResult)await sut.GetGames();

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetGames_OnSuccsess_ReturnsListOfGames()
        {
            //Arrange
            var mockTicTacToesService = new Mock<ITicTacToeGameService>();

            mockTicTacToesService
                .Setup(service => service.GetTicTacToeGameList())
                .ReturnsAsync(GamesFixture.GetTestGames());

            var sut = new TicTacToeGamesController(mockTicTacToesService.Object);

            //Act
            var result = await sut.GetGames();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<TicTacToeGameResponseDTO>>();
        }
    }
}
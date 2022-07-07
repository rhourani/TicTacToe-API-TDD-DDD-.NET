using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SSHTicTacToe.DTO;
using SSHTicTacToe.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TicTacToe.TEST.System.Services
{
    public class TestTicTacToeGameService
    {
        [Fact]
        public async Task GetGamesList_WhenCalled_InvokesHttpGetRequest()
        {
            //Arrange
            //TODO: Set the HTTP Request Moq!
            var sut = new TicTacToeGameService();
            //Act
            var result = await sut.GetTicTacToeGameList();
            //Assert
            //TODO: Verify HTTP Request is made!
            result.Should().BeOfType<List<TicTacToeGameDTO>>();
        }

        [Fact]
        public async Task GetGamesList_WhenCalled_ReturnListOfGames()
        {
            //Arrange
            var sut = new TicTacToeGameService();
            //Act
            var result = await sut.GetTicTacToeGameList();
            //Assert
            result.Should().BeOfType<List<TicTacToeGameDTO>>();
        }
    }
}

using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SSHTicTacToe.Controllers;
using SSHTicTacToe.DTO;
using SSHTicTacToe.Services.AuthorizedKeysParserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.TEST.Fixture;
using Xunit;

namespace TicTacToe.TEST.System.Controllers
{
    public class TestAuthorizedKeysParserController
    {
        [Fact]
        public async Task GetKeys_OnSuccsess_ReturnsStatusCode200()
        {
            //Arrange
            var mockAuthorizedKeysParser = new Mock<IAuthorizedKeysParserService>();

            mockAuthorizedKeysParser
                .Setup(service => service.GetAuthorizedKeysList())
                .ReturnsAsync(new List<AuthorizedKeysResponseDto>());

            var sut = new AuthorizedKeysParserController(mockAuthorizedKeysParser.Object);

            //Act

            var result = (OkObjectResult)await sut.GetKeys();

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetKeys_OnSuccsess_ReturnsListOfKeys()
        {
            //Arrange
            var mockAuthorizedKeysParser = new Mock<IAuthorizedKeysParserService>();

            mockAuthorizedKeysParser
                .Setup(service => service.GetAuthorizedKeysList())
                .ReturnsAsync(AuthorizedKeysFixture.GetTestKeys());

            var sut = new AuthorizedKeysParserController(mockAuthorizedKeysParser.Object);

            //Act
            var result = await sut.GetKeys();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<AuthorizedKeysResponseDto>>();
        }

        //TODO Write tests to check the types of the fileds and if there are missing ones
    }
}

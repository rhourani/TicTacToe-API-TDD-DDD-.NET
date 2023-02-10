using SSHTicTacToe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.TEST.Fixture
{
    public static class AuthorizedKeysFixture
    {
        public static List<AuthorizedKeysResponseDto> GetTestKeys() => new()
        {
            new AuthorizedKeysResponseDto
            {
                KeyId = 1,
                KeyData = string.Empty,

            },
            new AuthorizedKeysResponseDto
            {
                KeyId = 2,
                KeyData = string.Empty,

            },
            new AuthorizedKeysResponseDto
            {
                KeyId = 3,
                KeyData = string.Empty,

            }

        };
    }
}

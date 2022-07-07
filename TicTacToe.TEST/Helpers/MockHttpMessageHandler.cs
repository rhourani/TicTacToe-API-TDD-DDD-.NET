using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace TicTacToe.TEST.Helpers
{
    internal static class MockHttpMessageHandler<T>
    {
        internal static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<T> expectedResponse)
        {
            throw new NotImplementedException();
        }
    }
}

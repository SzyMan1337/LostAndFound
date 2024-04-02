using LostAndFound.ProfileService.CoreLibrary.Exceptions;
using LostAndFound.ProfileService.Middleware;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace LostAndFound.ProfileService.UnitTests.Middleware
{
    public class ErrorHandlingMiddlewareTests
    {
        private readonly Mock<HttpContext> _contextMock;
        private readonly ErrorHandlingMiddleware _errorHandlingMiddleware;
        private readonly Mock<RequestDelegate> _requestDelegateMock;

        public ErrorHandlingMiddlewareTests()
        {
            _contextMock = new Mock<HttpContext>();
            _errorHandlingMiddleware = new ErrorHandlingMiddleware();
            _requestDelegateMock = new Mock<RequestDelegate>();
        }

        [Fact]
        public async Task InvokeAsync_WithRequestDelegateThatThrowsUnauthorizedException_SetsStatusCode401()
        {
            _contextMock.SetupSet(context => context.Response.StatusCode = 401)
                .Verifiable();
            _requestDelegateMock
                .Setup(x => x.Invoke(_contextMock.Object))
                .Throws(new UnauthorizedException());

            await _errorHandlingMiddleware.InvokeAsync(_contextMock.Object, _requestDelegateMock.Object);
            _contextMock.VerifyAll();
        }
    }
}

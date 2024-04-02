using LostAndFound.PublicationService.CoreLibrary.Exceptions;
using Serilog;

namespace LostAndFound.PublicationService.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException ex)
            {
                Log.Warning("BadRequestException occured. {message}", ex.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (UnauthorizedException ex)
            {
                Log.Warning("UnauthorizedException occured. {message}", ex.Message);
                context.Response.StatusCode = 401;
            }
            catch (NotFoundException ex)
            {
                Log.Warning("NotFoundException exception occured. {message}", ex.Message);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                Log.Error(ex, "Error occured in API: {ErrorId}", errorId);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    ErrorId = errorId,
                    Message = "Something went wrong in our API. Contact our support team with the ErrorId if the issue persists."
                });
            }
        }
    }
}

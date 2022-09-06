using Notes.Application;
using Notes.Application.Exceptions;

namespace Notes.Api.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Requst
                await next(context);
                // Response
            }
            catch (NotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            catch (ExecutionNotAllowedException)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
            }
            catch (ValidationException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
    }

}

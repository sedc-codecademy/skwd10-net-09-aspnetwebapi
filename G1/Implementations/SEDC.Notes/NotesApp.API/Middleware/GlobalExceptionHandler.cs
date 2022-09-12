using NotesApp.Exceptions;

namespace NotesApp.API.Middleware
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
                await next.Invoke(context);
            }
            catch (UserNotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            catch (UserNotAllowedException)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
            }
            catch (UserValidationException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }
}

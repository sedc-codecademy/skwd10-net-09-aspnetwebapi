using Serilog;
using System.Diagnostics;

namespace NotesApp.API.Middleware
{
    public static class LoggingAndPerformanceMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingAndPerformanceTracking(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    Log.Logger.Debug("Request {@request}", context.Request);
                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    await next();
                    stopWatch.Stop();
                    if (stopWatch.ElapsedMilliseconds > 500)
                    {
                        Log.Logger.Warning("Slow request  {path}", context.Request.Protocol + context.Request.Host + context.Request.Path);
                    }

                }
                catch (Exception ex)
                {
                    Log.Logger.Error("An exception occured", ex);
                    throw;
                }
            });
            return app;
        }
    }
}

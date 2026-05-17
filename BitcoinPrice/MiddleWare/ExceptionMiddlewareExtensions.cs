using Microsoft.AspNetCore.Diagnostics;

namespace BitcoinPrice.MiddleWare
{
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder
        UseGlobalExceptionHandling(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();

                    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

                    if (exceptionHandler != null)
                    {
                        logger.LogError(exceptionHandler.Error,"Unhandled exception.");
                    }

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    var isApiRequest = context.Request.Path.StartsWithSegments("/api");

                    context.Response.Clear();

                    if (isApiRequest)
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";

                        await context.Response
                            .WriteAsJsonAsync(new
                            {
                                Message =
                                    "Unexpected server error."
                            });
                    }
                    else
                    {
                        context.Response.Redirect("/Home/Error");
                    }
                });
            });

            return app;
        }
    }
}

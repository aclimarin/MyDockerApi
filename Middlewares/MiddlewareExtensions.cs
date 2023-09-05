namespace MyDockerApi.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }

    public static IApplicationBuilder UseDbTransaction(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<DbTransactionMiddleware>();
    }
}
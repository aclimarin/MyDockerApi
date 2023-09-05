using Microsoft.EntityFrameworkCore.Storage;
using MyDockerApi.Infra;

namespace MyDockerApi.Middlewares;

public class DbTransactionMiddleware
{
    private readonly RequestDelegate _requestDelegate;
    public DbTransactionMiddleware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }

    public async Task Invoke(HttpContext httpContext, AppDbContext dbContext)
    {
        var httpMethod = httpContext.Request.Method.ToUpper();

        if (httpMethod.Equals(HttpMethod.Get.ToString().ToUpper()))
        {
            await _requestDelegate(httpContext);
            return;
        }

        if (httpMethod.Equals(HttpMethod.Post.ToString().ToUpper())
            || httpMethod.Equals(HttpMethod.Put.ToString().ToUpper())
            || httpMethod.Equals(HttpMethod.Delete.ToString().ToUpper()))
        {
            IDbContextTransaction transaction = null;
            using (transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await _requestDelegate(httpContext);
                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    if (transaction != null)
                        await transaction.RollbackAsync();

                    throw new Exception(e.Message);
                }
            }
        }
    }
}
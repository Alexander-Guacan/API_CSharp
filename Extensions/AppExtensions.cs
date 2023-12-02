using API_CSharp.Middleware;

namespace API_CSharp.Extensions
{
    public static class AppExtensions
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMIddleware>();
        }
    }
}

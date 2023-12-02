using API_CSharp.Wrappers;
using System.Net;
using System.Text.Json;

namespace API_CSharp.Middleware
{
    public class ErrorHandlerMIddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMIddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            } catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>()
                {
                    Succeeded = false,
                    Message = error?.Message
                };

                switch (error)
                {
                    case API_CSharp.Exceptions.ApiException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case KeyNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}

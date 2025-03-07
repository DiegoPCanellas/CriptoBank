using System.Net;

namespace Api.Middlewares
{
    public class UnauthorizedMiddleware
    {
        //delegate contendo o proximo middleware
        private readonly RequestDelegate _next;

        public UnauthorizedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //chamo o próximo middleware para garantir que todo o pipeline de middlewares foi executado antes de manipular a resposta.
            await _next(httpContext);

            if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                httpContext.Response.ContentType = "application/json";
                var result = System.Text.Json.JsonSerializer.Serialize(new { message = $"Acesso negado. Código: {(int)HttpStatusCode.Unauthorized}",
                                                                             success = false});
                await httpContext.Response.WriteAsync(result);
            }
        }
    }

    //Adicionar middleware ao pipeline
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UnauthorizedMiddleware>();
        }
    }
}

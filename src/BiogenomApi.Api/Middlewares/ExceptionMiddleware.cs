using System.Net;
using System.Text.Json;
using BiogenomApi.Services.Exceptions;

namespace BiogenomApi.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        { 
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }
    
    private Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        var code = HttpStatusCode.InternalServerError;
        var message = "Что-то пошло не так";
        switch (e)
        {
            case UserNotFoundException exception:
                code = HttpStatusCode.NotFound;
                message = JsonSerializer.Serialize(exception.Message);
                break;
            case ArgumentOutOfRangeException exception:
                code = HttpStatusCode.BadRequest;
                message = JsonSerializer.Serialize(exception.Message);
                break;
            case ArgumentException exception:
                code = HttpStatusCode.BadRequest;
                message = JsonSerializer.Serialize(exception.Message);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(message);
    }
}
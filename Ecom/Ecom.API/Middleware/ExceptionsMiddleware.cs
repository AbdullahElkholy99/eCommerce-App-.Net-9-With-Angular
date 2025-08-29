using Ecom.API.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace Ecom.API.Middleware;

public class ExceptionsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _environment;
    private readonly IMemoryCache _memoryCache;
    private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);


    public ExceptionsMiddleware(RequestDelegate next, IMemoryCache memoryCache, IHostEnvironment environment)
    {
        _next = next;
        _memoryCache = memoryCache;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {

            ApplySecurity(context);

            if (IsRequestAllowd(context) == false)
            {
                int statusCode = (int)HttpStatusCode.TooManyRequests;

                context.Response.StatusCode = statusCode;

                context.Response.ContentType = "application/json";

                var response = new ApiExceptions(
                    statusCode,
                    "Too many request , please try again later");

                await context.Response.WriteAsJsonAsync(response);

                return;
            }

            await _next(context);
        }
        catch (Exception ex)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;

            context.Response.StatusCode = statusCode;

            context.Response.ContentType = "application/json";

            var response =
                _environment.IsDevelopment() ?
                new ApiExceptions(statusCode, ex.Message, ex.StackTrace) :
                new ApiExceptions(statusCode, ex.Message); 

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsJsonAsync(json);
        }
    }

    private bool IsRequestAllowd(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress.ToString();

        var cachKey = $"Rate : {ip}";

        var dateNow = DateTime.Now;

        //count : number of request in the window time
        var (timesTamp, count) = _memoryCache.GetOrCreate(cachKey, entry =>
        {
            //set the expiration time for the cache entry
            entry.AbsoluteExpirationRelativeToNow = _rateLimitWindow;

            return (dateNow, 0);
        });

        if(dateNow - timesTamp < _rateLimitWindow)
        {
            if (count >= 5)
            {
                return false;
            }
            _memoryCache
                .Set(cachKey, (timesTamp, count += 1),_rateLimitWindow);
           
            return true;
        }
        else
        {
            _memoryCache
                .Set(cachKey, (timesTamp, count), _rateLimitWindow);

        }
        return true;

    }

    // Security Headers middleware asp.net : Read https://medium.com/@dev.mjdhanesh/boosting-security-with-asp-net-core-http-headers-3b26a8d3fdfd
    private void ApplySecurity(HttpContext context)
    {
        context.Response.Headers["X-Content-Type-Options"] = "nosniff";

        context.Response.Headers["X-XSS-Protection"] = "1;mode=block";

        // To Protect from Clickjacking Attack
        context.Response.Headers["X-Frame-Options"] = "Deny";

    }
}

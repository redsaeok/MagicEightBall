using System.Net;
using Magic8Ball.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Bcit.Function;

public class HttpWebApi
{
    private readonly ILogger<HttpWebApi> _logger;

    public HttpWebApi(ILogger<HttpWebApi> logger)
    {
        _logger = logger;
    }

    [Function("HttpWebApi")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }

    [Function("ShakeIt")]
    public async Task<HttpResponseData> ShakeIt([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route="magic8ball")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP GET/posts trigger function processed a request to SHAKE THE EIGHT.");

        var response = req.CreateResponse(HttpStatusCode.OK);

        response.Headers.Add("Content-Type", "application/json");

        var o = new { Response = EightBall.GetResponse() };

        await response.WriteStringAsync(JsonConvert.SerializeObject(o));        

        return response;
    }



}


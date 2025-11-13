// Minimal MCP (Model Context Protocol) server example implemented as
// a small ASP.NET Core minimal API. This exposes a single endpoint
// `/sayHello` that accepts JSON `{ "name": "Alice" }` and returns
// `{ "message": "Hello, Alice!" }`.
//
// How MCP fits in here (brief):
// - MCP (Model Context Protocol) is a communication pattern/protocol used to exchange
//   structured messages, context, and control between orchestration layers, tools,
//   and AI models. An MCP server exposes well-known endpoints that models or tool
//   orchestrators call to retrieve or send model context, execute actions, or
//   exchange messages in a structured format (often JSON).
// - This example shows the simplest HTTP/JSON pattern: an orchestration layer or
//   model adapter can call `/sayHello` with a request payload and receive a
//   deterministic, typed JSON response. Real MCP implementations include versioning,
//   metadata, authentication, streaming, and richer message envelopes.

using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Register Swagger/OpenAPI services so users can explore and test endpoints
// via the auto-generated Swagger UI. This is helpful for MCP demos because
// it provides a quick interactive way to call endpoints and inspect request/response
// shapes without writing a client.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger middleware (serves the generated OpenAPI JSON) and the
// Swagger UI at `/swagger` so developers can try the `sayHello` endpoint.
app.UseSwagger();
app.UseSwaggerUI();

// POST /sayHello
// Request JSON:  { "name": "Alice" }
// Response JSON: { "message": "Hello, Alice!" }
// Accept a typed `HelloRequest` parameter. Minimal APIs bind from the request body
// automatically for complex types which also allows Swashbuckle to generate an
// OpenAPI requestBody schema — so Swagger UI will render an editable JSON body.
app.MapPost("/sayHello", (HelloRequest input) =>
{
    if (input == null || string.IsNullOrWhiteSpace(input.Name))
    {
        return Results.BadRequest(new { error = "Missing 'name' in request body" });
    }

    var output = new HelloResponse { Message = $"Hello, {input.Name}! 👋 Welcome to MCP." };
    return Results.Ok(output);
});

app.MapGet("/", () => Results.Ok(new { info = "MCP demo: POST /sayHello with { name }" }));

app.Run();

internal class HelloRequest
{
    public string? Name { get; set; }
}

internal class HelloResponse
{
    public string? Message { get; set; }
}


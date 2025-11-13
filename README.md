# Let's Learn MCP 🚀

Welcome to the **Let's Learn MCP** demo — a tiny .NET project that shows how
you can build a minimal MCP (Model Context Protocol) server and experiment with
AI integrations. This repo provides a small ASP.NET Core app that exposes a
single endpoint, `POST /sayHello`, which returns a friendly JSON greeting.

**Tone:** beginner-friendly, light, and emoji-approved. 😄

**Quick links**
- Project: `src/McpServerDemo`
- Main file: `src/McpServerDemo/Program.cs`

**What is MCP? (Simple)**
**MCP (Model Context Protocol)** is a lightweight pattern for exchanging structured
messages and context between orchestration layers, tools, and AI models. Think
of it like a small set of agreed-upon HTTP endpoints and JSON shapes that let
components talk to each other in a predictable way — e.g., sending context to
models, requesting an action, or returning structured results.

In plain words: MCP helps different pieces of an AI system agree on how to ask
for things and how to get answers back.

**How this example helps**
- Shows a minimal HTTP/JSON MCP-style server you can run locally.
- Demonstrates how an orchestration layer or an AI adapter can call an endpoint
	(here `/sayHello`) and get a typed JSON response.
- Includes Swagger UI so you can play with the endpoint without writing a client.

**Run it locally (step-by-step)**
1. Make sure you have the .NET SDK installed (recommended: .NET 10 or .NET 9).
2. Open a terminal and run:

```powershell
cd c:\Users\User\lets-learn-mcp\src\McpServerDemo
dotnet run --framework net10.0
```

3. Open your browser to http://localhost:5000/swagger to use the Swagger UI.

Alternative: use `net9.0` if you only have .NET 9 installed:
```powershell
dotnet run --framework net9.0
```

**Call the `/sayHello` endpoint**

Using PowerShell (Invoke-RestMethod):
```powershell
Invoke-RestMethod -Method POST -Uri http://localhost:5000/sayHello \
	-Body (@{ name = 'Alice' } | ConvertTo-Json) -ContentType 'application/json'
```

Using curl:
```bash
curl -X POST http://localhost:5000/sayHello \
	-H "Content-Type: application/json" \
	-d '{"name":"Alice"}'
```

**Example output**
When the request is valid you should get a 200 response with JSON like:

```json
{
	"message": "Hello, Alice! 👋 Welcome to MCP."
}
```

If you omit the `name` field or send invalid JSON, the server will return a
400 error with a helpful message.

**Why this is useful for AI**
- An MCP server lets orchestration layers (or AI clients) call deterministic
	endpoints to retrieve or update context.
- You can connect this server to an AI client that calls `/sayHello` as an
	example action, or expand the API to perform more complex tasks (search,
	tool execution, or structured data extraction).

**Future ideas** ✨
- Add an MCP envelope with metadata (version, request id, timestamps).
- Add authentication and per-request metadata (who called, model preferences).
- Connect the server to an AI client or a Copilot Studio agent to trigger
	actions from model outputs.
- Add streaming endpoints or WebSocket support for real-time model interactions.
- Add examples and tests, or containerize with a Dockerfile for easy sharing.

**Contributing / Notes**
- This is a demo for .NET Conf-style learning — keep it simple and tweak away!
- If you want, I can add example AI client code (Node/Python/C#) or a Docker
	setup so you can run the demo anywhere.

Happy hacking — and let me know what feature you'd like next! 🛠️😊

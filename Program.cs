using System.Net;
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWebSockets();

app.MapGet("/", async context =>
{
    await Console.Out.WriteLineAsync("Socket");
    if (!context.WebSockets.IsWebSocketRequest)
        await context.Response.WriteAsync("Use WebSockets");
    else
    {
        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        while (true)
        {
            await webSocket.SendAsync(
                Encoding.UTF8.GetBytes("Ola"), WebSocketMessageType.Text, true, CancellationToken.None);
            await Task.Delay(2000);
        }
    }
});

await app.RunAsync();
namespace Tasks.McpServer;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tasks.McpServer.ApiClient;
using Tasks.McpServer.Resources;

public class Program
{
    static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Logging.AddConsole(options =>
        {
            options.LogToStandardErrorThreshold = LogLevel.Trace;
        });

        builder.Services
            .AddApiClients()
            .AddMcpServer(options =>
            {
                options.ServerInfo = new ModelContextProtocol.Protocol.Implementation
                {
                    Name = "Tasks & Repo Auditor",
                    Description = "A Model Context Protocol server that provides tools to manage tasks and audit repositories.",
                    Version = "1.0.0"
                };
            })
            .WithStdioServerTransport()
            .WithToolsFromAssembly()
            .WithPromptsFromAssembly()
            .WithResourcesFromAssembly();

        await builder
            .Build()
            .RunAsync();
    }
}

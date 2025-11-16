namespace aspire_mcp_server.McpServer.Tools;

using aspire_mcp_server.McpServer.ApiClient;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

[McpServerToolType]
public static class TasksTools
{
    [McpServerTool, Description("Get all tasks.")]
    public static async Task<string> GetAllTasksAsync(TasksApiClient client)
    {
        var weather = await client.GetAllTasksAsync();

        return JsonSerializer.Serialize(weather);
    }
}

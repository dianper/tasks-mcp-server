namespace aspire_mcp_server.McpServer.ApiClient;

using System.Net.Http;
using System.Net.Http.Json;
using Tasks.McpServer.Models;

public class TasksApiClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<TaskItem>> GetAllTasksAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<TaskItem>>("/api/tasks") ?? [];
    }
}

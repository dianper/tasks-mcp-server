namespace Tasks.McpServer.ApiClient;

using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using Tasks.McpServer.Models;

public class TasksApiClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<TaskItem>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<TaskItem>>("/api/tasks") ?? [];
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<TaskItem?>($"/api/tasks/{id}");
    }

    public async Task<TaskItem> CreateAsync(TaskItem input)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/tasks", input);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TaskItem>()
            ?? throw new InvalidOperationException("Failed to deserialize created task.");
    }

    public async Task<TaskItem?> UpdateAsync(int id, TaskItem input)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/tasks/{id}", input);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TaskItem>()
            ?? throw new InvalidOperationException("Failed to deserialize updated task.");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"/api/tasks/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return false;
        }

        response.EnsureSuccessStatusCode();

        return true;
    }

    public async Task<List<TaskItem>> SearchAsync(string query)
    {
        return await _httpClient.GetFromJsonAsync<List<TaskItem>>($"/api/tasks/search/{WebUtility.UrlEncode(query)}") ?? [];
    }

    public async Task<List<TaskItem>> GetOverdueAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<TaskItem>>("/api/tasks/overdue") ?? [];
    }
}

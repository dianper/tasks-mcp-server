namespace Tasks.McpServer.ApiClient;

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Tasks.McpServer.Models;

public class GitHubApiClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<GitHubIssue>> GetTopIssuesAsync(string owner, string repo, int count = 5)
    {
        var requestUri = $"/repos/{owner}/{repo}/issues?state=open&sort=created&direction=desc&per_page={count}";

        return await _httpClient.GetFromJsonAsync<List<GitHubIssue>>(requestUri) ?? [];
    }

    public async Task<GitHubRepoInfo?> GetRepoDetailsAsync(string owner, string repo)
    {
        return await _httpClient.GetFromJsonAsync<GitHubRepoInfo>($"/repos/{owner}/{repo}");
    }

    public async Task<string> GetRepoReadmeAsync(string owner, string repo)
    {
        var requestUri = $"/repos/{owner}/{repo}/readme";
        var response = await _httpClient.GetAsync(requestUri);
        
        if (!response.IsSuccessStatusCode)
        {
            return $"Error fetching README: {response.ReasonPhrase}";
        }
        
        var readmeContent = await response.Content.ReadAsStringAsync();
        return readmeContent;
    }
}

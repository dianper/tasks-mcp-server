namespace Tasks.McpServer.Tools;

using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;
using Tasks.McpServer.ApiClient;

[McpServerToolType]
public static class GitHubTools
{
    [McpServerTool, Description("Fetches the most recent open issues from a specific GitHub repository.")]
    public static async Task<string> GetLatestIssuesAsync(
        GitHubApiClient client,
        [Description("The repository owner (e.g., 'dotnet')")] string owner,
        [Description("The repository name (e.g., 'runtime')")] string repo)
    {
        var issues = await client.GetTopIssuesAsync(owner, repo);
        return JsonSerializer.Serialize(issues);
    }

    [McpServerTool, Description("Gets general information and statistics about a GitHub repository.")]
    public static async Task<string> GetRepoStatsAsync(
        GitHubApiClient client,
        string owner,
        string repo)
    {
        var stats = await client.GetRepoDetailsAsync(owner, repo);
        return JsonSerializer.Serialize(stats);
    }
}

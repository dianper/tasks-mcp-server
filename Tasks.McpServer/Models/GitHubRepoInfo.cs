namespace Tasks.McpServer.Models;

using System.Text.Json.Serialization;

public record GitHubRepoInfo(
    [property: JsonPropertyName("name")] string Name, 
    [property: JsonPropertyName("description")] string Description, 
    [property: JsonPropertyName("stargazers_count")] int StargazersCount, 
    [property: JsonPropertyName("open_issues_count")] int OpenIssuesCount);

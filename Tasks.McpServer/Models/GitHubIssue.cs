namespace Tasks.McpServer.Models;

using System.Text.Json.Serialization;

public record GitHubIssue(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("html_url")] string HtmlUrl,
    [property: JsonPropertyName("state")] string State,
    [property: JsonPropertyName("created_at")] DateTime CreatedAt);

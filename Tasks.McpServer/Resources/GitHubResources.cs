namespace Tasks.McpServer.Resources;

using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using Tasks.McpServer.ApiClient;

[McpServerResourceType]
public class GitHubResources
{
    [McpServerResource(
        UriTemplate = "framework://{owner}/{repo}/readme",
        Name = "GitHub Repository README",
        MimeType = "text/markdown")]
    [Description("The README file of a GitHub repository")]
    public static async Task<ResourceContents> GetReadmeContentAsync(
        GitHubApiClient client,
        string owner,
        string repo)
    {
        var readme = await client.GetRepoReadmeAsync(owner, repo);

        return new TextResourceContents
        {
            Text = readme,
            Uri = $"framework://{owner}/{repo}/readme"
        };
    }
}

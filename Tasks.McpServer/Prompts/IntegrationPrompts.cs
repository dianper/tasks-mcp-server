namespace Tasks.McpServer.Prompts;

using ModelContextProtocol.Server;
using System.ComponentModel;

[McpServerPromptType]
public static class IntegrationPrompts
{
    [McpServerPrompt(Name = "auto-sync-repo"), Description("Automatically finds the latest issues in a repo and creates tasks for them.")]
    public static string GetAutoSyncPrompt(string repoPath) 
    {
        var parts = repoPath.Split('/');
        var owner = parts[0];
        var repo = parts.Length > 1 ? parts[1] : "";

        return $"Please use 'SyncLatestIssuesToTasksAsync' to pull the 3 most recent issues from '{owner}/{repo}' and add them to my task list. " +
                "After that, give me a summary of what was imported.";
    }
}

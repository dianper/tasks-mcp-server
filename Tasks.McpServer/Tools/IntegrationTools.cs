namespace Tasks.McpServer.Tools;

using ModelContextProtocol.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Tasks.McpServer.ApiClient;
using Tasks.McpServer.Models;

[McpServerToolType]
public static class IntegrationTools
{
    [McpServerTool, Description("Fetches the latest open issues from a repository and automatically creates tasks for each one in the local system.")]
    public static async Task<string> SyncLatestIssuesToTasksAsync(
        GitHubApiClient githubClient,
        TasksApiClient tasksClient,
        [Description("The owner of the GitHub repo (e.g., 'microsoft')")] string owner,
        [Description("The name of the repo (e.g., 'dotnet')")] string repo,
        [Description("How many issues to sync (default is 3)")] int count = 3)
    {
        var issues = await githubClient.GetTopIssuesAsync(owner, repo, count);

        if (issues.Count == 0)
        {
            return $"No open issues found for {owner}/{repo}.";
        }

        var createdIds = new List<int>();

        foreach (var issue in issues)
        {
            var newTask = new TaskItem
            {
                Title = $"[GH] {issue.Title}",
                Description = $"From: {owner}/{repo}\nLink: {issue.HtmlUrl}\nCreated at: {issue.CreatedAt}",
                IsDone = false,
                CreatedAt = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(7)
            };

            var created = await tasksClient.CreateAsync(newTask);
            createdIds.Add(created.Id);
        }

        return $"Successfully synced {createdIds.Count} issues from {owner}/{repo} to your Task API. New Task IDs: {string.Join(", ", createdIds)}";
    }
}

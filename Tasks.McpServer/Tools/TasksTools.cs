namespace Tasks.McpServer.Tools;

using Tasks.McpServer.ApiClient;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;
using Tasks.McpServer.Models;

[McpServerToolType]
public static class TasksTools
{
    [McpServerTool, Description("Retrieve a complete list of all tasks. Use this to get an overview of the current workload.")]
    public static async Task<string> GetAllTasksAsync(TasksApiClient client)
    {
        var tasks = await client.GetAllAsync();

        return JsonSerializer.Serialize(tasks);
    }

    [McpServerTool, Description("Fetch a specific task by its unique identifier. Useful for viewing or editing a particular task.")]
    public static async Task<string> GetTaskByIdAsync(
        TasksApiClient client,
        [Description("The unique ID of the task to retrieve")] int id)
    {
        var task = await client.GetByIdAsync(id);
        return task != null
            ? JsonSerializer.Serialize(task)
            : $"Task with ID {id} not found.";
    }

    [McpServerTool, Description("Search for tasks that match the given query. Useful for finding tasks by keywords in title or description.")]
    public static async Task<string> SearchTasksAsync(
        TasksApiClient client,
        [Description("The search query to filter tasks")] string query)
    {
        var tasks = await client.SearchAsync(query);
        return JsonSerializer.Serialize(tasks);
    }

    [McpServerTool, Description("Creates a new task. The model will automatically handle Id and CreatedAt. You only need to provide Title, and optionally Description, IsDone, and DueDate.")]
    public static async Task<string> CreateTaskAsync(
        TasksApiClient client,
        [Description("The task details. Focus on Title and optionally Description or DueDate.")] TaskItem task)
    {
        var created = await client.CreateAsync(task);
        return $"Task '{created.Title}' successfully created with ID {created.Id}.";
    }

    [McpServerTool, Description("Updates an existing task. Use this to change the Title, Description, or mark a task as done (IsDone).")]
    public static async Task<string> UpdateTaskAsync(
        TasksApiClient client,
        [Description("The unique ID of the task to update")] int id,
        [Description("The updated task object with the new values")] TaskItem task)
    {
        var updated = await client.UpdateAsync(id, task);
        return updated != null
            ? $"Task {id} updated successfully."
            : $"Task {id} not found.";
    }

    [McpServerTool, Description("Fetches all tasks that have passed their due date. Use this to help the user identify urgent overdue work.")]
    public static async Task<string> GetOverdueTasksAsync(TasksApiClient client)
    {
        var overdue = await client.GetOverdueAsync();
        return overdue.Count == 0
            ? "No overdue tasks found. Everything is on track!"
            : JsonSerializer.Serialize(overdue);
    }

    [McpServerTool, Description("Deletes a specific task from the system using its unique identifier.")]
    public static async Task<string> DeleteTaskAsync(
        TasksApiClient client,
        [Description("The numeric ID of the task to be deleted")] int id)
    {
        var success = await client.DeleteAsync(id);
        return success ? $"Task {id} was successfully deleted." : $"Task {id} could not be found.";
    }
}

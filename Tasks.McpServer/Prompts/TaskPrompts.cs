namespace Tasks.McpServer.Prompts;

using ModelContextProtocol.Server;
using System.ComponentModel;

[McpServerPromptType]
public static class TaskPrompts
{
    [McpServerPrompt(Name = "daily-planner"), Description("Helps the user organize their day by analyzing all tasks and identifying priorities.")]
    public static string GetDailyPlannerPrompt()
    {
        return
            "Please start by listing all my current tasks using 'GetAllTasksAsync' and check for any overdue items with 'GetOverdueTasksAsync'. " +
            "Based on the results, summarize my top 3 priorities for today and ask if I want to create any new tasks to fill gaps in my schedule.";
    }
}

namespace Tasks.McpServer.ApiClient;

using Microsoft.Extensions.DependencyInjection;

public static class ApiClientExtensions
{
    // TODO: Move to appsettings.json
    private const string TasksApiBaseAddress = "https://localhost:7376";
    private const string GitHubApiBaseAddress = "https://api.github.com";
    private const int DefaultTimeoutInSeconds = 30;

    public static IServiceCollection AddApiClients(this IServiceCollection services)
    {
        services.AddHttpClient<TasksApiClient>(client =>
        {
            client.BaseAddress = new(TasksApiBaseAddress);
            client.Timeout = TimeSpan.FromSeconds(DefaultTimeoutInSeconds);
        });

        services.AddHttpClient<GitHubApiClient>(client =>
        {
            client.BaseAddress = new Uri(GitHubApiBaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new("application/vnd.github+json"));
            client.DefaultRequestHeaders.UserAgent.Add(new("DianperTasksMcpServer", "1.0"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-GitHub-Api-Version", "2022-11-28");
            client.Timeout = TimeSpan.FromSeconds(DefaultTimeoutInSeconds);
        });

        return services;
    }
}

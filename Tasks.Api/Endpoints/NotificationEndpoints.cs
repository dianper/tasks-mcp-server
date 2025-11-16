namespace Tasks.Api.Endpoints;

using Tasks.Api.Services;

public static class NotificationEndpoints
{
    public static void MapNotificationEndpoints(this WebApplication app)
    {
        var notifications = app.MapGroup("/api/notifications");

        notifications.MapGet("/", (NotificationService notifications) =>
        {
            return Results.Ok(notifications.GetAll());
        });
    }
}

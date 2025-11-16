namespace Tasks.Api.Endpoints;

using Tasks.Api.Models;
using Tasks.Api.Services;

public static class TaskEndpoints
{
    public static void MapTasksEndpoints(this WebApplication app)
    {
        var tasks = app.MapGroup("/api/tasks");

        tasks.MapGet("/", (TaskService tasks) =>
        {
            return Results.Ok(tasks.GetAll());
        });

        tasks.MapGet("/{id:int}", (int id, TaskService tasks) =>
        {
            var task = tasks.Get(id);
            return task is not null
                ? Results.Ok(task)
                : Results.NotFound();
        });

        tasks.MapPost("/", (TaskItem input, TaskService tasks, NotificationService notifications) =>
        {
            if (string.IsNullOrWhiteSpace(input.Title))
            {
                return Results.BadRequest("Title is required.");
            }

            var created = tasks.Create(input);
            notifications.Add($"Task created: '{created.Title}' (id {created.Id})");

            return Results.Created($"/tasks/{created.Id}", created);
        });

        tasks.MapPut("/{id:int}", (int id, TaskItem input, TaskService tasks, NotificationService notifications) =>
        {
            var updated = tasks.Update(id, input);
            if (updated is null)
            {
                return Results.NotFound();
            }

            notifications.Add($"Task updated: '{updated.Title}' (id {updated.Id})");

            return Results.Ok(updated);
        });

        tasks.MapDelete("/{id:int}", (int id, TaskService tasks, NotificationService notifications) =>
        {
            var removed = tasks.Delete(id);
            if (!removed) return Results.NotFound();

            notifications.Add($"Task removed (id {id})");
            return Results.NoContent();
        });

        tasks.MapGet("/search/{query}", (string query, TaskService tasks) =>
        {
            return Results.Ok(tasks.Search(query));
        });

        tasks.MapGet("/overdue", (TaskService tasks) =>
        {
            return Results.Ok(tasks.GetOverdue());
        });
    }
}

namespace Tasks.Api.Services;

using Tasks.Api.Models;

public class TaskService
{
    private static readonly List<TaskItem> _tasks = [];
    private static int _idCounter = 1;

    public IEnumerable<TaskItem> GetAll() => _tasks;

    public TaskItem? Get(int id) => _tasks.FirstOrDefault(t => t.Id == id);

    public TaskItem Create(TaskItem input)
    {
        input.Id = _idCounter++;
        _tasks.Add(input);
        return input;
    }

    public TaskItem? Update(int id, TaskItem input)
    {
        var existing = Get(id);
        if (existing == null)
        {
            return null;
        }

        existing.Title = input.Title ?? existing.Title;
        existing.Description = input.Description ?? existing.Description;
        existing.IsDone = input.IsDone;
        existing.DueDate = input.DueDate ?? existing.DueDate;

        return existing;
    }

    public bool Delete(int id)
    {
        var task = Get(id);
        if (task == null)
        {
            return false;
        }

        return _tasks.Remove(task);
    }

    public IEnumerable<TaskItem> Search(string query)
    {
        return _tasks.Where(t =>
            t.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            (t.Description?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false)
        );
    }

    public IEnumerable<TaskItem> GetOverdue()
    {
        var now = DateTime.UtcNow;
        return _tasks.Where(t => t.DueDate.HasValue && t.DueDate < now && !t.IsDone);
    }
}

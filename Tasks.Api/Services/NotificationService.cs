namespace Tasks.Api.Services;

using Tasks.Api.Models;

public class NotificationService
{
    private static readonly List<NotificationItem> _notifications = new();
    private static int _idCounter = 1;

    public void Add(string message)
    {
        _notifications.Add(new NotificationItem
        {
            Id = _idCounter++,
            Message = message
        });
    }

    public IEnumerable<NotificationItem> GetAll() => _notifications;
}

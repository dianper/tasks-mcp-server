namespace Tasks.Api.Models;

public class NotificationItem
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Message { get; set; } = string.Empty;
}

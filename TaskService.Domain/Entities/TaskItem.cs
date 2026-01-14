namespace TaskService.Domain.Entities;

public class TaskItem
{
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private TaskItem() { } // Mongo

    public TaskItem(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public void Complete()
    {
        IsCompleted = true;
    }
}

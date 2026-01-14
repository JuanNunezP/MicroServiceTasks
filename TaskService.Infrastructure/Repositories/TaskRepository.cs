using MongoDB.Driver;
using TaskService.Application.Tasks.Interfaces;
using TaskService.Domain.Entities;
using TaskService.Infrastructure.Settings;

namespace TaskService.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly IMongoCollection<TaskItem> _collection;

    public TaskRepository(MongoSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var db = client.GetDatabase(settings.Database);
        _collection = db.GetCollection<TaskItem>(settings.TasksCollection);
    }

    public async Task AddAsync(TaskItem task)
        => await _collection.InsertOneAsync(task);

    public async Task<TaskItem?> GetByIdAsync(string id)
        => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<List<TaskItem>> GetAllAsync()
        => await _collection.Find(_ => true).SortByDescending(x => x.CreatedAt).ToListAsync();

    public async Task UpdateAsync(TaskItem task)
        => await _collection.ReplaceOneAsync(x => x.Id == task.Id, task);
}

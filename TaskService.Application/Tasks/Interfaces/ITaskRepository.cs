using TaskService.Domain.Entities;

namespace TaskService.Application.Tasks.Interfaces;

public interface ITaskRepository
{
    Task AddAsync(TaskItem task);
    Task<TaskItem?> GetByIdAsync(string id);
    Task<List<TaskItem>> GetAllAsync();
    Task UpdateAsync(TaskItem task);
}

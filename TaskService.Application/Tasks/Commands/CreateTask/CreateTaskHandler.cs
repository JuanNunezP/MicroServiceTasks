using TaskService.Application.Tasks.Interfaces;
using TaskService.Domain.Entities;

namespace TaskService.Application.Tasks.Commands.CreateTask;

public class CreateTaskHandler
{
    private readonly ITaskRepository _repo;

    public CreateTaskHandler(ITaskRepository repo) => _repo = repo;

    public async Task<string> HandleAsync(CreateTaskCommand command)
    {
        var task = new TaskItem(command.Title,command.Description);
        await _repo.AddAsync(task);
        return task.Id;
    }
}

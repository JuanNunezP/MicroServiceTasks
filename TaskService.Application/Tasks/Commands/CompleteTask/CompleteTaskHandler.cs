using TaskService.Application.Tasks.Interfaces;

namespace TaskService.Application.Tasks.Commands.CompleteTask
{
    public class CompleteTaskHandler
    {
        private readonly ITaskRepository _repo;
        public CompleteTaskHandler(ITaskRepository repo) => _repo = repo;
        public async Task<bool> HandleAsync(CompleteTaskCommand command)
        {
            var task = await _repo.GetByIdAsync(command.TaskId);
            if (task is null) return false;

            task.Complete();
            await _repo.UpdateAsync(task);

            return true;
        }

    }
}

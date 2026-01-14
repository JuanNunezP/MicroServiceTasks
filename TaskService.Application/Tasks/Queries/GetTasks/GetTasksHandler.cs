using System;
using System.Collections.Generic;
using System.Text;
using TaskService.Application.Tasks.Interfaces;
using TaskService.Domain.Entities;

namespace TaskService.Application.Tasks.Queries.GetTasks
{
    public class GetTasksHandler {


        private readonly ITaskRepository _repo;

        public GetTasksHandler(ITaskRepository repo) => _repo = repo;

        public async Task<List<TaskItem>> HandleAsync(GetTasksQuery _)
            => await _repo.GetAllAsync();
    }
}

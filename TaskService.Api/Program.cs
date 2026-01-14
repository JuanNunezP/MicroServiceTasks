using FluentValidation;
using TaskService.Api.Middlewares;
using TaskService.Application.Tasks.Commands.CompleteTask;
using TaskService.Application.Tasks.Commands.CreateTask;
using TaskService.Application.Tasks.Interfaces;
using TaskService.Application.Tasks.Queries.GetTasks;
using TaskService.Infrastructure.Repositories;
using TaskService.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration
    .GetSection("Mongo")
    .Get<MongoSettings>()!;

builder.Services.AddSingleton(mongoSettings);
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<CreateTaskHandler>();
builder.Services.AddScoped<CompleteTaskHandler>();
builder.Services.AddScoped<GetTasksHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskValidator>();


var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();

app.MapPost("/tasksCreate",
    async (CreateTaskCommand cmd, CreateTaskHandler handler) =>
        Results.Ok(await handler.HandleAsync(cmd)))
   .AddEndpointFilter<ValidationFilter<CreateTaskCommand>>();


app.MapPut("/tasks/complete", async (CompleteTaskCommand cmd, CompleteTaskHandler handler)
    => (await handler.HandleAsync(cmd)) ? Results.Ok() : Results.NotFound());

app.MapGet("/tasks", async (GetTasksHandler handler)
    => Results.Ok(await handler.HandleAsync(new GetTasksQuery())));

app.Run();

using Tasks.Api.Endpoints;
using Tasks.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<TaskService>();
builder.Services.AddSingleton<NotificationService>();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapTasksEndpoints();
app.MapNotificationEndpoints();

app.Run();

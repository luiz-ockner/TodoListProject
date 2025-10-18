using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Application.Behaviors;
using TodoList.Application.Commands;
using TodoList.Application.Interfaces;
using TodoList.Application.Queries;
using TodoList.Infrastructure.Data;
using TodoList.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviço de Banco de Dados (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 1. Registro do FluentValidation: Encontra todos os validadores na assembly da Application
builder.Services.AddValidatorsFromAssembly(typeof(CreateTaskCommand).Assembly);

// 2. Registro do Pipeline Behavior: Adiciona o ValidationBehavior ao pipeline do MediatR
// Usa a injeção de dependência para garantir que o MediatR utilize este Behavior antes de chamar o Handler.
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Adicionar CORS para permitir requisições do React
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy =>
        {
            // Substitua "*" pelo seu endereço de frontend (e.g., "http://localhost:5173") em produção
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Adicionar MediatR, especificando onde procurar os Handlers (na Application)
builder.Services.AddMediatR(typeof(GetTasksQuery).Assembly);

// Adicionar serviços de autorização
builder.Services.AddAuthorization();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(); // Adiciona serviços do Swagger

var app = builder.Build();

// Aplica as migrações (cria/atualiza o banco) ao iniciar
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        if (exception is FluentValidation.ValidationException validationException)
        {
            // Define o status code 400 Bad Request
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            // Formata os erros de validação
            var errors = validationException.Errors
                .Select(error => new { Field = error.PropertyName, Message = error.ErrorMessage })
                .ToList();

            await context.Response.WriteAsJsonAsync(new
            {
                Title = "Uma ou mais erros de validação ocorreram.",
                Status = 400,
                Errors = errors
            });
            return;
        }

    });
});

app.UseHttpsRedirection();

// Usar CORS antes de authorization/controllers
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();


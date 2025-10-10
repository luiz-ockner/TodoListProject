using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Application.Interfaces;
using TodoList.Application.Queries;
using TodoList.Infrastructure.Data;
using TodoList.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviço de Banco de Dados (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.UseHttpsRedirection();

// Usar CORS antes de authorization/controllers
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();


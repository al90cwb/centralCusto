using Microsoft.EntityFrameworkCore;
using api.model;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner.
builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=centralCustoDb.db"));

var app = builder.Build();

// Configurar o pipeline de requisições.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Habilita o Swagger UI em ambientes de desenvolvimento.
    app.UseSwaggerUI();  // Adiciona a interface do Swagger.
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

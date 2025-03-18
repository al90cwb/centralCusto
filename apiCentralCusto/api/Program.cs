using Microsoft.EntityFrameworkCore;
using api.model;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner.
builder.Services.AddControllers();

//adicionando cors
builder.Services.AddCors(
    options => 
        options.AddPolicy("Acesso Total",configs=>configs
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod() )
);

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=centralCustoDb.db"));

var app = builder.Build();


//Requisição - URL e método / verbo http
app.MapGet("/", () => "API de Central de Custos");

// Configurar o pipeline de requisições.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Habilita o Swagger UI em ambientes de desenvolvimento.
    app.UseSwaggerUI();  // Adiciona a interface do Swagger.
}

app.UseHttpsRedirection();



app.MapControllers();

app.UseCors("Acesso Total");
app.Run();

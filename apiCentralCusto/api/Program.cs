using api.Data;
using api.Services; // Certifique-se de adicionar o using para o namespace do CentralCustoService
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner.
builder.Services.AddControllers();

// Adicionando CORS
builder.Services.AddCors(
    options => 
        options.AddPolicy("Acesso Total", configs => configs
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod())
);

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do DbContext com SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=centralCustoDb.db"));

// Registrar o CentralCustoService no contêiner de DI
builder.Services.AddScoped<CentralCustoService>();

var app = builder.Build();

// Requisição - URL e método / verbo http
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
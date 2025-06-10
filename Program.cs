using DotNetEnv;
using Sistema_Gerenciamento.Data;

// 1️⃣ Carrega as variáveis do .env
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// 2️⃣ Injeta a classe Database (singleton)
builder.Services.AddSingleton<Database>();

// 3️⃣ Configura MVC + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4️⃣ Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

// 5️⃣ Mapeia os controllers
app.MapControllers();

app.Run();

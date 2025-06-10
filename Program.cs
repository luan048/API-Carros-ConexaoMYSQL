using DotNetEnv;
using Sistema_Gerenciamento.Data;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Database>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();

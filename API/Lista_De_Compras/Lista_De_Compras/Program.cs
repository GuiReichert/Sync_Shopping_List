using Lista_De_Compras.Context;
using Lista_De_Compras.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


DotNetEnv.Env.Load();

builder.Services.AddDbContext<Lista_De_Compras_Context>(x => x.UseSqlServer(Environment.GetEnvironmentVariable("Db_Connection")));
builder.Services.AddScoped<IProdutoService, ProdutoService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.AspNetCore.Connections;
using Senac.PixelHub.Domain.Repositories.Games;
using Senac.PixelHub.Domain.Services.Games;
using Senac.PixelHub.Infrastructure.DatabaseConfiguration;
using Senac.PixelHub.Infrastructure.Repositories;
using Senac.PixelHub.Infrastructure.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IGameServices, GameService>();
builder.Services.AddScoped<IGameRepository, GameRepository>();


builder.Services.AddScoped<IDbConnectionFactory>(x =>
{
    return new DbConnectionFactory("Server=(localdb)\\MSSQLLocalDB; Database=PixelHub; Trusted_Connection=True");
});






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

using Api;
using Api.Services;
using Api.Services.Interfaces;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;
IConfiguration config = builder.Configuration;

services.AddControllers();

services.AddDbContext<DataContext>(options => options.UseNpgsql(config.GetConnectionString("TodoDb")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddAutoMapper(typeof(MappingProfile));

services.AddScoped<IRepository<Todo>, Repository<Todo>>();
services.AddScoped<IRepository<Comment>, Repository<Comment>>();

services.AddScoped<ITodoService, TodoService>();

WebApplication app = builder.Build();

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
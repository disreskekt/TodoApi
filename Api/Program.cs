using Api;
using Api.Services;
using Api.Services.Interfaces;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;using Serilog;
using Serilog.Core;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;
IConfiguration config = builder.Configuration;

builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration)
    .WriteTo.PostgreSQL(config.GetConnectionString("TodoDb"), "Logs", needAutoCreateTable: true));

services.AddControllers();

services.AddDbContext<DataContext>(options => options.UseNpgsql(config.GetConnectionString("TodoDb")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddAutoMapper(typeof(MappingProfile));

services.AddScoped<IRepository<Todo>, Repository<Todo>>();
services.AddScoped<IRepository<Comment>, Repository<Comment>>();

services.AddScoped<ITodoService, TodoService>();
services.AddScoped<ICommentService, CommentService>();
services.AddScoped<IValidationService<Todo>, ValidationService<Todo>>();

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
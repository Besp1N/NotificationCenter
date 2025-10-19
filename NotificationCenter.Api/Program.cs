using System.Reflection.Metadata;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NotificationCenter.Application;
using NotificationCenter.Application.DependencyInjection;
using NotificationCenter.Infrastructure;
using NotificationCenter.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

var connectionString = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddInfrastructure(builder.Configuration, connectionString!);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
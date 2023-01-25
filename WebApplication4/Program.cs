using System.Configuration;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Implementations;
using Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.Configure<DaDataConfig>(builder.Configuration.GetSection("DaDataConfig"));

var con = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(con));
builder.Services.AddTransient<ILoggerRepository, LoggerRepository>();
builder.Services.AddTransient<ILoggerMessager, LoggerMessager>();
builder.Services.AddTransient<IOpenStreetMapService, OpenStreetMapService>();

builder.Services.AddTransient<IDadataService, DadataService>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient();
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
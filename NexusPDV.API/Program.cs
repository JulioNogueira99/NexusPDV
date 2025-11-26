using Microsoft.EntityFrameworkCore;
using NexusPDV.API.Controllers;
using NexusPDV.Application.Services;
using NexusPDV.Domain.Interfaces;
using NexusPDV.Infrastructure.Context;
using NexusPDV.Infrastructure.Repositories;
using FluentValidation; 
using NexusPDV.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddValidatorsFromAssemblyContaining<PlaceOrderValidator>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "NexusPDV API",
        Version = "v1",
        Description = "API de Ponto de Venda do Júlio"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NexusPDV API v1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
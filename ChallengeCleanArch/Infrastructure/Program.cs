using Application.BusinessLogic;
using Domain.Interfaces.Application;
using Domain.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.DAL;
using Persistence.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductsPersistence, ProductsPersistence>();
builder.Services.AddScoped<IProductsApplication, ProductsApplication>();

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

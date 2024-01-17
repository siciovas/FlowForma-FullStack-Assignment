using Microsoft.EntityFrameworkCore;
using Products.Core.Interfaces;
using Products.Infrastructure.Database;
using Products.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IIngredientRep, IngredientRepository>();
builder.Services.AddScoped<IFoodRep, FoodRepository>();
builder.Services.AddScoped<IClothesRep, ClothesRepository>();
builder.Services.AddScoped<IDeviceRep, DeviceRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Products.Core.Interfaces;
using Products.Infrastructure.Database;
using Products.Infrastructure.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddNewtonsoftJson(x => 
{
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    x.SerializerSettings.Converters.Add(new StringEnumConverter());
});
 
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
builder.Services.AddScoped<IMaterialRep, MaterialRepository>();

builder.Services.AddCors(o => o.AddPolicy("Policy", builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseCors("Policy");

app.Run();

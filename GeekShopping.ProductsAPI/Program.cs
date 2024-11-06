using GeekShopping.ProductsAPI;
using GeekShopping.ProductsAPI.Config;
using GeekShopping.ProductsAPI.Models.Context;
using GeekShopping.ProductsAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connection = builder.Configuration.GetSection("ConnectionStrings")["MySQLConnection"];
builder.Services.AddDbContext<MySQLContext>(options =>
    options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 40))));

// AutoMapper
var mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<DatabaseHealthCheck>();
builder.Services.AddLogging();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

await Task.Run(() =>
{
    using (var scope = app.Services.CreateScope())
    {
        var dbHealthCheck = scope.ServiceProvider.GetRequiredService<DatabaseHealthCheck>();
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        if (dbHealthCheck.IsConnectionSuccessful())
            logger.LogInformation("DB connection is OK");
        else
            logger.LogInformation("DB connection is not OK");
    }

    return Task.CompletedTask;
});

app.Run();
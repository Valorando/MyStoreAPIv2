using AddOrderService.Services;
using AddOrderService.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");
var requestsSection = configuration.GetSection("Requests");

var sqlRequests = new Dictionary<string, string>();
foreach (var request in requestsSection.GetChildren())
{
    sqlRequests[request.Key] = request.Value;
}

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .MinimumLevel.Information()
        .WriteTo.Console()
        .WriteTo.File("Logs\\log.txt", rollingInterval: RollingInterval.Day);
});

// Add services to the container.
builder.Services.AddScoped<IOrderService, OrderService>(provider => new OrderService(connectionString, sqlRequests));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

app.Run();

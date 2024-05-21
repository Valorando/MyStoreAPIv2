using MyStoreGatewayAPI.Interfaces;
using MyStoreGatewayAPI.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
var requestsSection = configuration.GetSection("Requests");

var httpRequests = new Dictionary<string, string>();
foreach (var request in requestsSection.GetChildren())
{
    httpRequests[request.Key] = request.Value;
}

HttpClient client = new HttpClient();

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .MinimumLevel.Information()
        .WriteTo.Console()
        .WriteTo.File("Logs\\log.txt", rollingInterval: RollingInterval.Day);
});

// Add services to the container.
builder.Services.AddScoped<IGetProductsService, GetProductsService>(provider => new GetProductsService(httpRequests, client));
builder.Services.AddScoped<IAddOrderService, AddOrderService>(provider => new AddOrderService(httpRequests, client));

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

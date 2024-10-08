using Microsoft.OpenApi.Models;
using ProductService.API.Extensions;
using ProductService.API.Rabbitmq;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddProductServiceExtensions();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<RabbitMQPublisher>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product Service API", // Fixed typo in "Poduct"
        Version = "v1",
        Description = "Product Service API for our Microservices Architecture", // Fixed typo in "APi"
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync(); // Changed to RunAsync

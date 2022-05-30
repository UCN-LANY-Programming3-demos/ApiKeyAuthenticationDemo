using KeyAuthenticationWithMiddleware.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ApiKeyAuthenticateMiddleware>(); // Adds the custom authentication middleware to the pipeline

app.MapControllers();

app.Run();

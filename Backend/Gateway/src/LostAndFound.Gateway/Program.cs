using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
Log.Information("Starting web application");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

builder.Configuration
    .AddJsonFile("ocelot.json")
    .AddJsonFile($"configuration.{builder.Environment.EnvironmentName}.json");

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddCors();

builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

var app = builder.Build();

app.UseSwaggerForOcelotUI(opt => {}, uiOpt => {
    uiOpt.DocumentTitle = "LostAndFound system - Api Gateway";
    uiOpt.RoutePrefix = string.Empty;
});

app.UseRouting();

app.UseCors(options => options
    .WithOrigins(new[] { builder.Configuration["ReactClient:Url"] })
    .WithExposedHeaders("X-Pagination")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/healthcheck");
    endpoints.MapControllers();
});

app.UseWebSockets();
app.UseOcelot().Wait();

app.Run();

// Make the implicit Program class public so test projects can access it
#pragma warning disable CA1050 // Declare types in namespaces
public partial class Program { }
#pragma warning restore CA1050 // Declare types in namespaces
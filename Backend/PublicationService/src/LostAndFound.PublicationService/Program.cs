using LostAndFound.PublicationService.Core;
using LostAndFound.PublicationService.Core.FluentValidators;
using LostAndFound.PublicationService.CoreLibrary.Settings;
using LostAndFound.PublicationService.DataAccess;
using LostAndFound.PublicationService.DataAccess.DatabaseSeeder.Interfaces;
using LostAndFound.PublicationService.Middleware;
using LostAndFound.PublicationService.ThirdPartyServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
Log.Information("Starting web application");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

var authenticationSettings = new AuthenticationSettings();
builder.Configuration.Bind(AuthenticationSettings.SettingName, authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddHealthChecks();
builder.Services.AddControllers(setupAction =>
{
    setupAction.Filters.Add(
        new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
    setupAction.Filters.Add(
        new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
    setupAction.Filters.Add(
        new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
}).AddJsonOptions(opt => { opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); }); ;

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddFluentValidators();
builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddCoreServices();
builder.Services.AddThirdPartyServices(builder.Configuration);

builder.Services.AddResponseCaching();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(config =>
    {
        config.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.AccessTokenSecret)),
            ValidIssuer = authenticationSettings.Issuer,
            ValidAudience = authenticationSettings.Audience,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc(
        "v1",
        new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = "LostAndFound Publication Service",
            Version = "v1",
            Description = "Publication service is part of LostAndFound system. It provides functionalities for the publication of advertisements about found or lost items.",
        });

    var currentAssembly = Assembly.GetExecutingAssembly();
    var xmlDocs = currentAssembly.GetReferencedAssemblies()
        .Union(new AssemblyName[] { currentAssembly.GetName() })
        .Select(a => Path.Combine(AppContext.BaseDirectory, $"{a.Name}.xml"))
        .Where(f => File.Exists(f)).ToArray();
    foreach (var xmlDoc in xmlDocs)
    {
        setupAction.IncludeXmlComments(xmlDoc);
    }

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setupAction.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

var app = builder.Build();

SeedDbCollections();

app.UseRouting();

app.UseResponseCaching();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(setupAction =>
{
    setupAction.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "LostAndFound Publication Service");
    setupAction.RoutePrefix = string.Empty;
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/healthcheck");
    endpoints.MapControllers();
});

app.Run();


void SeedDbCollections()
{
    using var scope = app.Services.CreateScope();
    var dbSeeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();
    dbSeeder?.SeedCategoriesCollection();
}

// Make the implicit Program class public so test projects can access it
#pragma warning disable CA1050 // Declare types in namespaces
public partial class Program { }
#pragma warning restore CA1050 // Declare types in namespaces

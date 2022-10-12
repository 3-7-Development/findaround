using System.Reflection;
using findaroundAPI.Entities;
using findaroundAPI.Helpers;
using findaroundAPI.Utilities;
using findaroundAPI.Middlewares;
using findaroundAPI.Services;
using findaroundAPI.Config;
using Newtonsoft.Json;
using findaroundShared.Models.Dtos;
using findaroundAPI.Models.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using findaroundAPI.Authorization.Handlers;

var builder = WebApplication.CreateBuilder(args);

var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});

builder.Services.AddScoped<IAuthorizationHandler, UserOperationAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, PostOperationAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, CommentOperationAuthorizationHandler>();

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(ServiceLifetime.Transient);
builder.Services.AddScoped<DatabaseSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Configuring Middlewares
builder.Services.AddScoped<ErrorHandlingMiddleware>();

// Data models validators
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<LoginUserDto>, LoginUserDtoValidator>();

// Configure Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddHttpContextAccessor();

// Configure inside dependencies
DbConnectionUtilities.FilePath = builder.Configuration["DbConfigFile"];

HostCertConfig.CertPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Config/", @builder.Configuration["CertFileName"]);
HostCertConfig.CertPass = HostCertConfig.ReadPassFromFile(builder);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
    options.ListenAnyIP(443, listOpt =>
    {
        listOpt.UseHttps(HostCertConfig.CertPath, HostCertConfig.CertPass);
    });
});

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();

seeder.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public static class HostCertConfig
{
    public static string CertPath { get; set; }
    public static string CertPass { get; set; }

    public static string ReadPassFromFile(WebApplicationBuilder builder)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string json = string.Empty;

        var path = @builder.Configuration["CertPassword"];
        using (var stream = assembly.GetManifestResourceStream(path))
        {
            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }
        }

        var config = JsonConvert.DeserializeObject<SecretsConfig>(json);

        return config.CertPassword;
    }
}

public class SecretsConfig
{
    public string CertPassword { get; set; }
}
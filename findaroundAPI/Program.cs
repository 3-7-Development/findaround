using System.Reflection;
using findaroundAPI.Entities;
using findaroundAPI.Helpers;
using findaroundAPI.Utilities;
using findaroundAPI.Middlewares;
using findaroundAPI.Services;
using Newtonsoft.Json;
using findaroundShared.Models.Dtos;
using findaroundAPI.Models.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(ServiceLifetime.Transient);
builder.Services.AddScoped<DatabaseSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();

// Adding services
builder.Services.AddScoped<IUserService, UserService>();

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
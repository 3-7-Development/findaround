using findaroundAPI.Entities;
using findaroundAPI.Helpers;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(ServiceLifetime.Transient);
builder.Services.AddScoped<DatabaseSeeder>();

HostCertConfig.CertPath = Environment.ExpandEnvironmentVariables(@builder.Configuration["CertPath"]);
HostCertConfig.CertPass = Environment.ExpandEnvironmentVariables(@builder.Configuration["CertPassword"]);

var folder = Environment.GetEnvironmentVariable("HOME");
if (!string.IsNullOrWhiteSpace(folder))
{
    var path = $"{folder}/findaround/config/json/secrets.json";

    string json;
    //var config = new SecretsConfig();

    using (var reader = new StreamReader(path))
    {
        json = reader.ReadToEnd();
    }

    var config = JsonConvert.DeserializeObject<SecretsConfig>(json);
    HostCertConfig.CertPass = config.CertPassword;
}

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public static class HostCertConfig
{
    public static string CertPath { get; set; }
    public static string CertPass { get; set; }
}

public class SecretsConfig
{
    public string CertPassword { get; set; }
}
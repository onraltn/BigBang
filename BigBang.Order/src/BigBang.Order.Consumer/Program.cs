

using BigBang.Order.Consumer;
using BigBang.Order.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder();
var env = builder.Environment;
builder.Configuration.SetBasePath(env.ContentRootPath);
builder.Configuration.AddJsonFile(
    "appsettings.json",
    optional: false,
    reloadOnChange: true);
builder.Configuration.AddJsonFile(
    $"appsettings.{env.EnvironmentName}.json",
    optional: true,
    reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

builder.Services.RegisterHost(builder.Configuration);
builder.Services.RegisterPersistence();

var app = builder.Build();

app.ConfigureApp(env);
app.Run();
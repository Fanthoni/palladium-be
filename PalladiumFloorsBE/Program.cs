using static Startup;
using static Catalog;
using System;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

app.MapGet("/catalog", () => {
    return GetCatalogs();
});

startup.Configure(app, app.Environment);


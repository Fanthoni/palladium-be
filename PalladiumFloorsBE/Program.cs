using static Startup;
using static Catalog;
using System;
using System.Linq;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

app.MapGet("/catalog", (IMongoDatabase database) => {
    var catalog = new Catalog(database);
    var items = catalog.GetCatalogs();
    return Results.Ok(items);
});

startup.Configure(app, app.Environment);


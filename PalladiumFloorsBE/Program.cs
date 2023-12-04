using static Startup;
using static CatalogController;
using System;
using System.Linq;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
var database = app.Services.GetRequiredService<IMongoDatabase>();
var catalog = new CatalogController(database);


app.MapGet("/catalog", () => {
    var items = catalog.GetCatalogs();
    return Results.Ok(items);
});

app.MapGet("/catalog/categories/{catalogId}", (string catalogId) => {
    var categories = catalog.GetCatalogCategories(database, catalogId);
    return Results.Ok(categories);
});

startup.Configure(app, app.Environment);


using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
var database = app.Services.GetRequiredService<IMongoDatabase>();

app.MapGet("/catalog", (CatalogController catalog) => {
    var items = catalog.GetCatalogs();
    return Results.Ok(items);
});

app.MapGet("/catalog/categories/{catalogId}", (string catalogId, CatalogController catalog) => {
    var categories = catalog.GetCatalogCategories(database, catalogId);
    return Results.Ok(categories);
});

app.MapPost("/sendMail", ([FromBody] SendMailRequest mailRequest, MailController mail) => {
    mail.SendEmail(mailRequest.Message, mailRequest.Email, mailRequest.Name);
    return Results.Ok();
});

startup.Configure(app, app.Environment);


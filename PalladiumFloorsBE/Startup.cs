using MongoDB.Driver;

public class Startup {
    public IConfiguration Configuration {
        get;
    }

    public Startup(IConfiguration config) {
        Configuration = config;
    }

    public void ConfigureServices(IServiceCollection services) {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAuthorization();
        this.ConfigureDatabase(services);

        services.AddControllers();
        services.AddScoped<CatalogController>();
        services.AddScoped<MailController>();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env) {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        } else {
            app.UseHttpsRedirection();
        }
        app.Run();
    }

    private void ConfigureDatabase(IServiceCollection services) {
        // var mongoDbSettings = Configuration.GetSection("MongoDbSettings");
        // string? connectionString = mongoDbSettings["ConnectionString"];
        // string? databaseName = mongoDbSettings["DatabaseName"];
        string? connectionString = Configuration["DbConnectionString"];
        string? databaseName = Configuration["DbDatabaseName"];

        var dbClient = new MongoClient(connectionString);
        var database = dbClient.GetDatabase(databaseName);
        services.AddSingleton(database);
    }
}
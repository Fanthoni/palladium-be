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

        this.ConfigureCorsOrigin(services);
    }

    public void Configure(WebApplication app, IWebHostEnvironment env) {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        } else {
            app.UseHttpsRedirection();
        }
        app.UseCors("AllowSpecificOrigin");
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

    private void ConfigureCorsOrigin(IServiceCollection services) {
        string allowedOrigin = Configuration["AllowOrigin"] ?? "http://localhost:3000";

        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.WithOrigins(allowedOrigin)
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });
    }
}
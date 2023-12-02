using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
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
        this.ConfigureDatabase(services);
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
        var mongoDbSettings = Configuration.GetSection("MongoDbSettings");
        string? connectionString = mongoDbSettings["ConnectionString"];
        string? databaseName = mongoDbSettings["DatabaseName"];

        var dbClient = new MongoClient(connectionString);
        var database = dbClient.GetDatabase(databaseName);
        services.AddSingleton(database);

        // to return the id string instead of the ObjectId
        BsonClassMap.RegisterClassMap<CatalogItem>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(c => c.Id)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
        });
    }
}
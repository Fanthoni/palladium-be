public class Startup {
    public IConfiguration configRoot {
        get;
    }

    public Startup(IConfiguration config) {
        configRoot = config;
    }

    public void ConfigureServices(IServiceCollection services) {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env) {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.Run();
    }
}
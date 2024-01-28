using NSE.Identidade.API.Configuration;

namespace NSE.Identidade.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = AddBuilderConfig(args);

        var configuration = builder.Configuration;

        // Add services to the container.
        builder.Services.AddIdentityConfiguration(configuration);

        builder.Services.AddAPIConfiguration();

        builder.Services.AddSwaggerConfiguration();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    public static WebApplicationBuilder AddBuilderConfig(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
            .AddEnvironmentVariables();

        if (builder.Environment.IsDevelopment())
            builder.Configuration.AddUserSecrets<Program>();

        return builder;
    }
}

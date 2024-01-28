namespace NSE.Identidade.API.Configuration;

public static class APIConfig
{
    public static IServiceCollection AddAPIConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }
}

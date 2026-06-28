using InfnetStreaming.Data;
using Microsoft.EntityFrameworkCore;

namespace InfnetStreaming.API.Configurations
{
    public static class ConfiguracoesConexao
    {
        public static IServiceCollection AdicionarConexoesApp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbConnection(configuration);
            return services;
        }

        public static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InfnetStreamingDbContext>(options =>
                options.UseInMemoryDatabase("DbInfnetStreaming")
            );
            return services;
        }
    }
}

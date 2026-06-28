using InfnetStreaming.API.Filter;
using InfnetStreaming.API.JsonPolicies;

namespace InfnetStreaming.API.Configurations
{
    public static class ConfiguracaoControllers
    {
        public static IServiceCollection AdicionarEConfigurarControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
               options.Filters.Add(typeof(APIGlobalExceptionFilter)))
                   .AddJsonOptions(x => x.JsonSerializerOptions.PropertyNamingPolicy = new JsonSnakeCasePolicy());
            services.AdicionarDocumentacao();
            return services;
        }

        private static IServiceCollection AdicionarDocumentacao(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        public static WebApplication UsarDocumentacao(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "swagger"));
            }
            return app;
        }
    }
}

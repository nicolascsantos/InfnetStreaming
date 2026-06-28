namespace InfnetStreaming.API.Configurations
{
    public static class ConfiguracaoCors
    {
        public const string PolicyName = "FrontEnd";

        public static IServiceCollection AdicionarCors(this IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddPolicy(PolicyName, policy =>
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod()));
            return services;
        }
    }
}

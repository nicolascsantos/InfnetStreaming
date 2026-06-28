using InfnetStreaming.API.Auth;
using InfnetStreaming.Application.Services.Usuario.LogarUsuario;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InfnetStreaming.API.Configurations
{
    public static class ConfiguracaoAuth
    {
        public static IServiceCollection AdicionarAutenticacao(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGeradorToken, GeradorToken>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!))
                    };
                });

            return services;
        }
    }
}

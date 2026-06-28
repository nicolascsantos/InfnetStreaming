using InfnetStreaming.Application.Services.Assinatura.AssinarPlano;
using InfnetStreaming.Application.Services.Busca.BuscarBandas;
using InfnetStreaming.Application.Services.Busca.BuscarMusicas;
using InfnetStreaming.Application.Services.Transacao.AutorizarTransacao;
using InfnetStreaming.Application.Services.Usuario.CriarUsuario;
using InfnetStreaming.Application.Services.Usuario.FavoritarBanda;
using InfnetStreaming.Application.Services.Usuario.FavoritarMusica;
using InfnetStreaming.Application.Services.Usuario.LogarUsuario;
using InfnetStreaming.Data.Repositorios;
using InfnetStreaming.Domain.Repositorios;

namespace InfnetStreaming.API.Configurations
{
    public static class ConfiguracaoServicos
    {
        public static IServiceCollection AdicionarServicos(this IServiceCollection services)
        {
            services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
            services.AddScoped<IRepositorioBanda, RepositorioBanda>();
            services.AddScoped<IRepositorioMusica, RepositorioMusica>();
            services.AddScoped<IRepositorioPlano, RepositorioPlano>();
            services.AddScoped<IRepositorioTransacao, RepositorioTransacao>();

            services.AddScoped<CriarUsuarioService>();
            services.AddScoped<LogarUsuarioService>();
            services.AddScoped<AssinarPlanoService>();
            services.AddScoped<AutorizarTransacaoService>();
            services.AddScoped<FavoritarMusicaService>();
            services.AddScoped<FavoritarBandaService>();
            services.AddScoped<BuscarBandasService>();
            services.AddScoped<BuscarMusicasService>();

            return services;
        }
    }
}

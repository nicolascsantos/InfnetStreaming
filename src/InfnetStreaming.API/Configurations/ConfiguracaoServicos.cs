using InfnetStreaming.Application.Services.Assinatura.AssinarPlano;
using InfnetStreaming.Application.Services.Banda.ObterBanda;
using InfnetStreaming.Application.Services.Busca.BuscarBandas;
using InfnetStreaming.Application.Services.Busca.BuscarMusicas;
using InfnetStreaming.Application.Services.Plano.ListarPlanos;
using InfnetStreaming.Application.Services.Playlist.AdicionarMusicaPlaylist;
using InfnetStreaming.Application.Services.Playlist.CriarPlaylist;
using InfnetStreaming.Application.Services.Playlist.ListarPlaylists;
using InfnetStreaming.Application.Services.Playlist.ObterPlaylist;
using InfnetStreaming.Application.Services.Playlist.RemoverMusicaPlaylist;
using InfnetStreaming.Application.Services.Transacao.AutorizarTransacao;
using InfnetStreaming.Application.Services.Usuario.CriarUsuario;
using InfnetStreaming.Application.Services.Usuario.FavoritarBanda;
using InfnetStreaming.Application.Services.Usuario.FavoritarMusica;
using InfnetStreaming.Application.Services.Usuario.LogarUsuario;
using InfnetStreaming.Application.Services.Usuario.ObterFavoritos;
using InfnetStreaming.Application.Services.Usuario.ObterUsuario;
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
            services.AddScoped<IRepositorioPlaylist, RepositorioPlaylist>();

            services.AddScoped<CriarUsuarioService>();
            services.AddScoped<LogarUsuarioService>();
            services.AddScoped<AssinarPlanoService>();
            services.AddScoped<AutorizarTransacaoService>();
            services.AddScoped<FavoritarMusicaService>();
            services.AddScoped<FavoritarBandaService>();
            services.AddScoped<BuscarBandasService>();
            services.AddScoped<BuscarMusicasService>();
            services.AddScoped<ListarPlanosService>();
            services.AddScoped<ObterUsuarioService>();
            services.AddScoped<ObterFavoritosService>();
            services.AddScoped<ObterBandaService>();
            services.AddScoped<CriarPlaylistService>();
            services.AddScoped<ListarPlaylistsService>();
            services.AddScoped<ObterPlaylistService>();
            services.AddScoped<AdicionarMusicaPlaylistService>();
            services.AddScoped<RemoverMusicaPlaylistService>();

            return services;
        }
    }
}

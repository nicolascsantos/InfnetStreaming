using InfnetStreaming.Domain.Repositorios;

namespace InfnetStreaming.Application.Services.Usuario.ObterFavoritos
{
    public class ObterFavoritosService
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioMusica _repositorioMusica;
        private readonly IRepositorioBanda _repositorioBanda;

        public ObterFavoritosService(
            IRepositorioUsuario repositorioUsuario,
            IRepositorioMusica repositorioMusica,
            IRepositorioBanda repositorioBanda)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioMusica = repositorioMusica;
            _repositorioBanda = repositorioBanda;
        }

        public async Task<ObterFavoritosOutput> Executar(Guid usuarioId, CancellationToken cancellationToken)
        {
            var usuario = await _repositorioUsuario.GetComFavoritos(usuarioId, cancellationToken);

            var musicaIds = usuario.MusicasFavoritas.Select(f => f.MusicaId).ToList();
            var bandaIds = usuario.BandasFavoritas.Select(f => f.BandaId).ToList();

            var musicas = await _repositorioMusica.ListarPorIds(musicaIds, cancellationToken);
            var bandas = await _repositorioBanda.ListarPorIds(bandaIds, cancellationToken);

            var musicasOutput = musicas
                .Select(m => new MusicaFavoritaOutput(m.Id, m.Nome, m.Duracao))
                .ToList();

            var bandasOutput = bandas
                .Select(b => new BandaFavoritaOutput(b.Id, b.Nome))
                .ToList();

            return new ObterFavoritosOutput(musicasOutput, bandasOutput);
        }
    }
}

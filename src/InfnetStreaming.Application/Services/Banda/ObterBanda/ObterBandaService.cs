using InfnetStreaming.Domain.Repositorios;

namespace InfnetStreaming.Application.Services.Banda.ObterBanda
{
    public class ObterBandaService
    {
        private readonly IRepositorioBanda _repositorioBanda;

        public ObterBandaService(IRepositorioBanda repositorioBanda)
            => _repositorioBanda = repositorioBanda;

        public async Task<ObterBandaOutput> Executar(Guid bandaId, CancellationToken cancellationToken)
        {
            var banda = await _repositorioBanda.GetComDetalhes(bandaId, cancellationToken);

            return new ObterBandaOutput(
                banda.Id,
                banda.Nome,
                banda.DataFormacao,
                banda.Integrantes
                    .Select(i => new IntegranteOutput(i.Id, i.Nome, i.DataDeNascimento))
                    .ToList(),
                banda.Generos.Select(g => g.Nome).ToList(),
                banda.Albuns
                    .OrderBy(a => a.DataLancamento)
                    .Select(a => new AlbumOutput(
                        a.Id,
                        a.Nome,
                        a.DataLancamento,
                        a.Musicas
                            .OrderBy(m => m.OrdemMusica)
                            .Select(m => new MusicaAlbumOutput(m.Id, m.Nome, m.Duracao, m.OrdemMusica))
                            .ToList()
                    ))
                    .ToList(),
                banda.Singles
                    .Select(s => new SingleOutput(s.Id, s.Nome, s.Duracao))
                    .ToList()
            );
        }
    }
}

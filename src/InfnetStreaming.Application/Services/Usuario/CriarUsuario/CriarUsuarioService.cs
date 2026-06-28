using InfnetStreaming.Domain.Excecoes;
using InfnetStreaming.Domain.Repositorios;
using InfnetStreaming.Domain.Validacao;

namespace InfnetStreaming.Application.Services.Usuario.CriarUsuario
{
    public class CriarUsuarioService
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioPlano _repositorioPlano;

        public CriarUsuarioService(IRepositorioUsuario repositorioUsuario, IRepositorioPlano repositorioPlano)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioPlano = repositorioPlano;
        }

        public async Task<CriarUsuarioOutput> Executar(CriarUsuarioInput input, CancellationToken cancellationToken)
        {
            ValidacaoDominio.NotNullOrEmpty(input.Nome, "nome");
            ValidacaoDominio.NotNullOrEmpty(input.Username, "username");
            ValidacaoDominio.MinLength(input.Senha, 6, "senha");

            if (await _repositorioUsuario.ExisteUsername(input.Username, cancellationToken))
                throw new ProblemaRelacionamentoException($"Username '{input.Username}' já está em uso.");

            await _repositorioPlano.Get(input.PlanoId, cancellationToken);

            var senhaHash = SenhaHelper.Hash(input.Senha);
            var usuario = new Domain.Entities.Usuario(input.Nome, input.Username, senhaHash, input.PlanoId);

            await _repositorioUsuario.Adicionar(usuario, cancellationToken);

            return new CriarUsuarioOutput(usuario.Id, usuario.Nome, usuario.Username, usuario.PlanoId, usuario.DataCriada);
        }
    }
}

using InfnetStreaming.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfnetStreaming.Data.Seeder
{
    public static class InfnetStreamingSeeder
    {
        public static void Seed(DbContext context)
        {
            SeedPlanos(context);
            SeedBandas(context);
            SeedUsuarios(context);
            SeedTransacoes(context);
        }

        private static void SeedPlanos(DbContext context)
        {
            if (context.Set<Plano>().Any()) return;

            context.Set<Plano>().AddRange(
                new Plano("Básico", 9.90m),
                new Plano("Premium", 19.90m)
            );
            context.SaveChanges();
        }

        private static void SeedBandas(DbContext context)
        {
            if (context.Set<Banda>().Any()) return;

            var beatles = new Banda("The Beatles", new DateTime(1960, 8, 1));
            beatles.AdicionarGenero(new Genero("Rock", DateTime.Now));
            beatles.AdicionarGenero(new Genero("Pop", DateTime.Now));
            beatles.AdicionarIntegrante(new Integrante("John Lennon", new DateTime(1940, 10, 9)));
            beatles.AdicionarIntegrante(new Integrante("Paul McCartney", new DateTime(1942, 6, 18)));
            beatles.AdicionarIntegrante(new Integrante("George Harrison", new DateTime(1943, 2, 25)));
            beatles.AdicionarIntegrante(new Integrante("Ringo Starr", new DateTime(1940, 7, 7)));

            var abbeyRoad = new Album("Abbey Road", new DateTime(1969, 9, 26));
            abbeyRoad.AdicionarMusica(new Musica("Come Together", TimeSpan.FromSeconds(259), new List<Guid>(), 1));
            abbeyRoad.AdicionarMusica(new Musica("Something", TimeSpan.FromSeconds(182), new List<Guid>(), 2));
            abbeyRoad.AdicionarMusica(new Musica("Here Comes the Sun", TimeSpan.FromSeconds(185), new List<Guid>(), 3));
            beatles.AdicionarAlbum(abbeyRoad);

            context.Set<Banda>().Add(beatles);
            context.SaveChanges();
        }

        private static void SeedUsuarios(DbContext context)
        {
            if (context.Set<Usuario>().Any()) return;

            var planoId = context.Set<Plano>().First().Id;
            context.Set<Usuario>().Add(new Usuario("Admin", "admin", "Admin123!", planoId));
            context.SaveChanges();
        }

        private static void SeedTransacoes(DbContext context)
        {
            if (context.Set<Transacao>().Any()) return;

            var usuario = context.Set<Usuario>().First();
            var plano = context.Set<Plano>().First(p => p.Id == usuario.PlanoId);

            var transacao = new Transacao(usuario.Id, plano.Id, plano.Valor);
            transacao.Aprovar();
            context.Set<Transacao>().Add(transacao);
            context.SaveChanges();
        }
    }
}

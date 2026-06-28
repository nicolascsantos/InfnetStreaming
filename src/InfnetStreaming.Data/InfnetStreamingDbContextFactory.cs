using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InfnetStreaming.Data
{
    public class InfnetStreamingDbContextFactory : IDesignTimeDbContextFactory<InfnetStreamingDbContext>
    {
        public InfnetStreamingDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<InfnetStreamingDbContext>()
                .UseSqlServer("Server=NICOLAS-PC;Database=dbInfnetStreaming;User Id=sa;Password=03112010;TrustServerCertificate=True;")
                .Options;

            return new InfnetStreamingDbContext(options);
        }
    }
}

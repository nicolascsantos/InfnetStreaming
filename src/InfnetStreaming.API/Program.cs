using InfnetStreaming.API.Configurations;
using InfnetStreaming.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AdicionarEConfigurarControllers()
    .AdicionarConexoesApp(builder.Configuration)
    .AdicionarServicos()
    .AdicionarAutenticacao(builder.Configuration)
    .AdicionarCors();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<InfnetStreamingDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Falha ao aplicar migrations ou seed. Verifique a conexão com o banco de dados.");
    }
}

app.UsarDocumentacao();

app.UseCors(ConfiguracaoCors.PolicyName);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

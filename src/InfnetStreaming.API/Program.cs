using InfnetStreaming.API.Configurations;
using InfnetStreaming.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AdicionarEConfigurarControllers()
    .AdicionarConexoesApp(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<InfnetStreamingDbContext>();
    context.Database.Migrate();
}

app.UsarDocumentacao();

app.UseAuthorization();

app.MapControllers();

app.Run();

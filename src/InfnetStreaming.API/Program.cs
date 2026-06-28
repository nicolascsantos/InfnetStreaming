using InfnetStreaming.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AdicionarEConfigurarControllers();
builder.Services.AdicionarConexoesApp(builder.Configuration);

var app = builder.Build();

app.UsarDocumentacao();

app.UseAuthorization();

app.MapControllers();

app.Run();

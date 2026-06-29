# InfnetStreaming

Plataforma de streaming musical desenvolvida como projeto acadêmico do Instituto Infnet. Permite cadastro de usuários, busca de bandas e músicas, gerenciamento de playlists, favoritos e assinaturas de planos.

## Tecnologias

| Camada | Tecnologia |
|---|---|
| Backend | .NET 10 · ASP.NET Core · EF Core 10 |
| Banco de Dados | SQL Server |
| Autenticação | JWT Bearer (PBKDF2 + HS256) |
| Frontend | Angular 22 · TypeScript 6 |
| Documentação | Swagger / OpenAPI |

## Arquitetura

O backend segue **Domain-Driven Design (DDD)** organizado em quatro projetos:

```
src/
├── InfnetStreaming.Domain      # Entidades, regras de negócio, interfaces de repositório
├── InfnetStreaming.Data        # EF Core, migrations, implementações de repositório
├── InfnetStreaming.Application # Application services (casos de uso)
└── InfnetStreaming.API         # Controllers REST, autenticação JWT, Swagger
src/
└── InfnetStreaming.Web         # SPA Angular 22
```

## Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local ou remoto)
- [Node.js 22+](https://nodejs.org/) com npm 11+
- Angular CLI 22: `npm install -g @angular/cli`

---

## Backend

### 1. Configurar a connection string

Edite `src/InfnetStreaming.API/appsettings.json` com os dados do seu SQL Server:

```json
{
  "ConnectionStrings": {
    "InfnetStreamingConnString": "Server=SEU_SERVIDOR;Database=dbInfnetStreaming;User Id=SEU_USUARIO;Password=SUA_SENHA;TrustServerCertificate=True;"
  },
  "Jwt": {
    "SecretKey": "InfnetStreaming-JwtSecretKey-MustBe32CharsLong!",
    "Issuer": "InfnetStreaming",
    "Audience": "InfnetStreamingUsers",
    "ExpiresInHours": "24"
  }
}
```

### 2. Build

```bash
dotnet build InfnetStreaming.slnx
```

### 3. Executar

```bash
dotnet run --project src/InfnetStreaming.API/InfnetStreaming.API.csproj
```

## ⚠️⚠️ As migrations são aplicadas automaticamente na inicialização. O banco de dados é criado caso não exista. ⚠️⚠️



A API estará disponível em: `http://localhost:5103`

Documentação Swagger (ambiente Development): `http://localhost:5103/swagger`

### Migrations manuais (opcional)

```bash
# Criar nova migration
dotnet ef migrations add <NomeDaMigration> \
  --project src/InfnetStreaming.Data \
  --startup-project src/InfnetStreaming.Data

# Aplicar migrations manualmente
dotnet ef database update \
  --project src/InfnetStreaming.Data \
  --startup-project src/InfnetStreaming.Data
```

---

## Frontend

### 1. Instalar dependências

```bash
cd src/InfnetStreaming.Web
npm install
```

### 2. Executar

```bash
npm start
```

A aplicação estará disponível em: `http://localhost:4200`

> O frontend consome a API em `http://localhost:5103`. Certifique-se de que o backend está rodando antes de acessar a aplicação.

### Build de produção

```bash
npm run build
```

---

## Funcionalidades

- **Autenticação** — cadastro de conta, login com JWT
- **Planos** — listagem e assinatura de planos de acesso
- **Busca** — pesquisa paginada de bandas e músicas
- **Detalhe de banda** — integrantes, gêneros, álbuns e singles
- **Favoritos** — favoritar/desfavoritar músicas e bandas
- **Playlists** — criar, listar, adicionar e remover músicas
- **Transações** — histórico e autorização de pagamentos

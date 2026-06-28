# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Commands

```bash
# Build the solution
dotnet build InfnetStreaming.slnx

# Build a specific project
dotnet build src/InfnetStreaming.Data/InfnetStreaming.Data.csproj

# EF Core migrations (run from solution root)
dotnet ef migrations add <MigrationName> --project src/InfnetStreaming.Data --startup-project src/InfnetStreaming.Data

# Apply migrations
dotnet ef database update --project src/InfnetStreaming.Data --startup-project src/InfnetStreaming.Data
```

Tests project is defined in the solution (`/tests/` folder) but not yet created.

## Architecture

The solution follows **Domain-Driven Design (DDD)** with two projects:

### `InfnetStreaming.Domain`
Pure domain layer — no infrastructure dependencies.

- **`SeedWork/`**: Base classes. `Entidade` holds a `Guid Id` (auto-generated). `RaizDeAgregacao` extends `Entidade` to mark aggregate roots.
- **`Entities/`**: All domain entities. Aggregate roots are `Banda` and `Usuario`; the rest are child entities.
- **`Validacao/ValidacaoDominio`**: Static validation helpers that throw `ValidacaoDominioException` on failure. Entities call these in a `Validar()` method from their constructor.
- **`Excecoes/ValidacaoDominioException`**: The single domain exception type.

### `InfnetStreaming.Data`
EF Core 10 persistence layer. Depends on `InfnetStreaming.Domain`.

- **`InfnetStreamingDbContext`**: Registers all `IEntityTypeConfiguration<T>` implementations via `ApplyConfiguration` in `OnModelCreating`.
- **`Configurations/`**: One `IEntityTypeConfiguration<T>` class per entity. Shadow foreign keys (e.g., `"BandaId"`, `"UsuarioId"`) are used for child entity ownership since entities expose no FK properties. Private backing collections are mapped via `.HasField("_fieldName")`.

### Domain Model Relationships
- `Banda` (aggregate root) owns `Album[]`, `Integrante[]`, `Genero[]`; has many-to-many `Musica` (singles) via join table `MusicaBanda`.
- `Album` owns `Musica[]`.
- `Usuario` (aggregate root) owns `Playlist[]` and `MusicaFavorita[]`.
- `Playlist` owns `Musica[]`.
- `Musica.BandaIds` is a computed in-memory collection ignored by EF (mapped through the join table instead).

### Conventions
- Entity collections are `private readonly List<T>` fields exposed as `IReadOnlyCollection<T>` properties. Mutations go through explicit `Adicionar*` / `Remover*` methods on the aggregate root.
- Protected parameterless constructors exist on all entities solely for EF Core materialization.
- The codebase is in Portuguese (`Banda`, `Musica`, `Integrante`, etc.) — keep new code consistent with this naming convention.

# Takumi — isolated OpenMU (Docker)

Goal: run OpenMU in Docker with a **dedicated PostgreSQL database** (`openmu_takumi`) so it does not mix with a vanilla `openmu` deployment on the same host.

## Quick start

From this folder:

1. Create HTTP basic auth for the admin UI (same pattern as `deploy/all-in-one`):

   ```bash
   printf 'admin:$(openssl passwd -apr1 openmu)\n' > .htpasswd
   ```

2. Start:

   ```bash
   docker compose up -d --no-build
   ```

3. Admin panel (through nginx): **http://localhost:9180/**  
   Game-related ports are published with an offset (see `docker-compose.yml`), e.g. connect **44505** maps to container **44405**.

4. PostgreSQL is exposed on host **54434** → container **5432** for backups or tooling.

## Database name and configuration

- Postgres creates database **`openmu_takumi`** (`POSTGRES_DB`).
- The compose file mounts **`ConnectionSettings.takumi.xml`** to `/app/ConnectionSettings.xml` so **published `munique/openmu` images** use that DB name (Docker Hub images may not yet include the `DB_NAME` env patch).
- When you run a **local build** from this repository, you can also rely on **`DB_NAME=openmu_takumi`** (see `ConfigFileDatabaseConnectionStringProvider`) without mounting the XML.

## Migrating data from Takumi (SQL Server / `.bak`)

## Recommended database strategy (important)

Do **not** clone the full Takumi MSSQL schema as the main OpenMU runtime schema.

- OpenMU runtime must keep its own EF schemas (`data`, `config`, `account`, `friend`, `guild`, ...).
- Legacy Takumi tables should live in a separate staging schema: **`takumi_legacy`**.
- Migrate incrementally by domain (accounts -> characters -> inventory -> guild/trade), not one big-bang.

This folder now includes `initdb/001-create-takumi-legacy-schema.sql` and mounts `./initdb` into Postgres (`/docker-entrypoint-initdb.d`) so `takumi_legacy` is prepared on first DB init.

If you already created the volume before this change, recreate DB volume once:

```bash
docker compose down -v
docker compose up -d --no-build
```

Takumi’s MuOnline stack uses **Microsoft SQL Server** and a **different schema** than OpenMU’s Entity Framework PostgreSQL model.

- There is **no** one-shot “restore .bak into Postgres” for OpenMU.
- Typical path:
  1. Let OpenMU **initialize** the PostgreSQL schema (first startup / admin “Install”).
  2. Implement **targeted migration** (accounts, characters, guilds, etc.) with scripts or a small ETL that maps legacy tables to OpenMU entities.

Keep SQL Server + `.bak` as a **source of truth** until those migrations exist.

## Next step: Takumi logic in .NET

Place custom protocol or gameplay code in a **separate folder or plugin assembly** in your OpenMU fork so the boundary stays clear (see Takumi repo `docs/SERVER-PORT-PLAN.md` and related checklists).

## Related

- Default all-in-one layout: `deploy/all-in-one/`
- Environment variables: root `QuickStart.md` (`DB_HOST`, `DB_NAME`, `DB_ADMIN_USER`, `DB_ADMIN_PW`)
- Takumi extension assembly (referenced by Startup): `src/Takumi/MUnique.OpenMU.Takumi/` — build your own image from this repo to include `MUnique.OpenMU.Takumi.dll` in `/app`.

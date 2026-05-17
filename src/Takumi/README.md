# Takumi extensions (`src/Takumi`)

Isolated area for **Takumi-specific** server work on top of OpenMU: protocol quirks, game plugins, SQL→PG migration helpers, and experiments ported from the legacy C++ stack.

## Layout

| Path | Purpose |
|------|--------|
| `MUnique.OpenMU.Takumi/` | Single extension assembly referenced by **Startup**. Put `[PlugIn]` classes, adapters, and Takumi-only types here. |
| `MUnique.OpenMU.Takumi/GameLogic/PlugIns/` | Game-side plugins (`IChatCommandPlugIn`, `IPeriodicTaskPlugIn`, …). Example: `ChatCommands/TakumiPortStatusChatCommandPlugIn.cs` (`/takumiport`) — shows migration status from the in-code registry. |
| `MUnique.OpenMU.Takumi/Networking/` | Packet shaping, encoders, or helpers aligned with `GameServer` / `Network` (add `ProjectReference` when needed). |
| `MUnique.OpenMU.Takumi/Persistence/` | ETL scripts (as `.cs` utilities), mapping from legacy SQL Server concepts to OpenMU persistence—not duplicate EF contexts. |

## Wiring

- **Startup** references this project and executes `_ = typeof(TakumiAssembly);` so the assembly loads before `PlugInManager` scans types.
- Official **`munique/openmu` Docker images** do not include this assembly until you build and publish your own image from this fork.

## Docker DB isolation

Use `deploy/takumi-openmu/` for a dedicated PostgreSQL database (`openmu_takumi`) alongside vanilla OpenMU.


## Current migration module

- `Networking/ConnectServer/TakumiConnectServerProtocol.cs`: classifier for legacy ConnectServer packets (`C1 05`, `C1 F4 02/03/06`) to keep protocol-port work explicit and testable.

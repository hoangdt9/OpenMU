-- Creates a dedicated schema for mirrored MSSQL (Takumi legacy) tables.
-- This DOES NOT replace OpenMU EF schemas (data/config/account/friend/guild).
--
-- Runs automatically only on first Postgres volume initialization.

CREATE SCHEMA IF NOT EXISTS takumi_legacy;

CREATE TABLE IF NOT EXISTS takumi_legacy.migration_run
(
    id BIGSERIAL PRIMARY KEY,
    source_system TEXT NOT NULL DEFAULT 'mssql_takumi',
    batch_name TEXT NOT NULL,
    started_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    finished_at TIMESTAMPTZ NULL,
    status TEXT NOT NULL,
    notes TEXT NULL
);

CREATE TABLE IF NOT EXISTS takumi_legacy.migration_checkpoint
(
    key TEXT PRIMARY KEY,
    value TEXT NOT NULL,
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

COMMENT ON SCHEMA takumi_legacy IS 'Staging/mirror schema for legacy Takumi MSSQL data during incremental migration.';

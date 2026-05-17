// <copyright file="LegacyPortRegistry.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace MUnique.OpenMU.Takumi.Migration;

/// <summary>
/// Central migration registry to keep the C++ -> OpenMU port incremental and explicit.
/// </summary>
public static class LegacyPortRegistry
{
    private static readonly IReadOnlyList<LegacyFeaturePort> Entries =
    [
        new("ConnectServerProtocol.cpp", "Server list handshake and client routing", LegacySubsystem.ConnectServer, "Networking/ConnectServer", LegacyPortStatus.InProgress),
        new("ServerList.cpp", "Server list loading and publish", LegacySubsystem.ConnectServer, "Networking/ConnectServer", LegacyPortStatus.NotStarted),
        new("ClientManager.cpp", "Session/client lifecycle", LegacySubsystem.ConnectServer, "Networking/ConnectServer", LegacyPortStatus.NotStarted),
        new("SocketManager.cpp", "TCP transport handling", LegacySubsystem.ConnectServer, "Networking/Common", LegacyPortStatus.NotStarted),

        new("DataServerProtocol.cpp", "Data persistence packet flow", LegacySubsystem.DataServer, "Persistence/DataServerAdapter", LegacyPortStatus.NotStarted),
        new("JoinServerProtocol.cpp", "Cross-server account/session coordination", LegacySubsystem.JoinServer, "Networking/JoinServer", LegacyPortStatus.NotStarted),

        new("Attack.cpp", "Combat damage flow", LegacySubsystem.GameServer, "GameLogic/Combat", LegacyPortStatus.NotStarted),
        new("ItemManager.cpp", "Item creation, options, storage", LegacySubsystem.GameServer, "GameLogic/Items", LegacyPortStatus.NotStarted),
        new("CommandManager.cpp", "GM/player command execution", LegacySubsystem.GameServer, "GameLogic/Commands", LegacyPortStatus.InProgress),
        new("EventManager.cpp", "Global event scheduler", LegacySubsystem.GameServer, "GameLogic/Events", LegacyPortStatus.NotStarted),
        new("ObjectManager.cpp", "World object lifecycle", LegacySubsystem.GameServer, "GameLogic/World", LegacyPortStatus.NotStarted),
        new("MapServerManager.cpp", "Map server routing", LegacySubsystem.GameServer, "Networking/GameServer", LegacyPortStatus.NotStarted),
    ];

    /// <summary>
    /// Gets all registered migration entries.
    /// </summary>
    public static IReadOnlyList<LegacyFeaturePort> GetAll() => Entries;

    /// <summary>
    /// Returns entries for a specific subsystem.
    /// </summary>
    /// <param name="subsystem">The subsystem to filter by.</param>
    public static IEnumerable<LegacyFeaturePort> ForSubsystem(LegacySubsystem subsystem)
        => Entries.Where(entry => entry.Subsystem == subsystem);

    /// <summary>
    /// Gets status counts used by diagnostics and commands.
    /// </summary>
    public static IEnumerable<(LegacyPortStatus Status, int Count)> GetStatusCounts()
        => Entries
            .GroupBy(entry => entry.Status)
            .Select(group => (group.Key, group.Count()))
            .OrderBy(tuple => tuple.Key);
}

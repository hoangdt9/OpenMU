// <copyright file="LegacySubsystem.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace MUnique.OpenMU.Takumi.Migration;

/// <summary>
/// Legacy C++ server subsystem buckets from Takumi source tree.
/// </summary>
public enum LegacySubsystem
{
    /// <summary>
    /// Legacy connect server process.
    /// </summary>
    ConnectServer,

    /// <summary>
    /// Legacy data server process.
    /// </summary>
    DataServer,

    /// <summary>
    /// Legacy join server process.
    /// </summary>
    JoinServer,

    /// <summary>
    /// Legacy game server process.
    /// </summary>
    GameServer,
}

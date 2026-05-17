// <copyright file="TakumiConnectServerRequestType.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace MUnique.OpenMU.Takumi.Networking.ConnectServer;

/// <summary>
/// Known request types used by Takumi connect-server protocol flow.
/// </summary>
public enum TakumiConnectServerRequestType
{
    /// <summary>
    /// Unknown or currently unsupported packet.
    /// </summary>
    Unknown,

    /// <summary>
    /// Request for patch/ftp metadata (C1 05).
    /// </summary>
    PatchInfo,

    /// <summary>
    /// Request for game-server list (C1 F4 02/06).
    /// </summary>
    ServerList,

    /// <summary>
    /// Request for selected game-server connection endpoint (C1 F4 03).
    /// </summary>
    ServerInfo,
}

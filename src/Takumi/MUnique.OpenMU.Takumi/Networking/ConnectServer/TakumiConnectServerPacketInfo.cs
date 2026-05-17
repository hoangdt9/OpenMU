// <copyright file="TakumiConnectServerPacketInfo.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace MUnique.OpenMU.Takumi.Networking.ConnectServer;

/// <summary>
/// Structured view of a raw connect-server packet.
/// </summary>
/// <param name="RequestType">Classified request type.</param>
/// <param name="Header">Packet header byte (usually C1/C2).</param>
/// <param name="MainCode">Main message code.</param>
/// <param name="SubCode">Sub message code (if available).</param>
/// <param name="Length">Packet byte length.</param>
public sealed record TakumiConnectServerPacketInfo(
    TakumiConnectServerRequestType RequestType,
    byte Header,
    byte MainCode,
    byte? SubCode,
    int Length);

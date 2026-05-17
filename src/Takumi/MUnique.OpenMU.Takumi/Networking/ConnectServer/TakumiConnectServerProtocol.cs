// <copyright file="TakumiConnectServerProtocol.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace MUnique.OpenMU.Takumi.Networking.ConnectServer;

/// <summary>
/// Lightweight classifier for Takumi legacy ConnectServer packets (ported from protocol behavior in ConnectServerProtocol.cpp).
/// </summary>
public static class TakumiConnectServerProtocol
{
    private const byte HeaderC1 = 0xC1;
    private const byte HeaderC2 = 0xC2;

    private const byte PatchMainCode = 0x05;
    private const byte ServerMainCode = 0xF4;

    private static readonly HashSet<byte> ServerListSubCodes = [0x02, 0x06];
    private static readonly HashSet<byte> ServerInfoSubCodes = [0x03];

    /// <summary>
    /// Tries to classify a raw connect-server packet.
    /// </summary>
    /// <param name="packet">The raw packet bytes.</param>
    /// <returns>A classified packet view.</returns>
    public static TakumiConnectServerPacketInfo Parse(ReadOnlySpan<byte> packet)
    {
        if (packet.Length < 3)
        {
            return new(TakumiConnectServerRequestType.Unknown, 0, 0, null, packet.Length);
        }

        var header = packet[0];
        var mainCode = packet[2];
        byte? subCode = packet.Length >= 4 ? packet[3] : null;

        if (header is not (HeaderC1 or HeaderC2))
        {
            return new(TakumiConnectServerRequestType.Unknown, header, mainCode, subCode, packet.Length);
        }

        if (mainCode == PatchMainCode)
        {
            return new(TakumiConnectServerRequestType.PatchInfo, header, mainCode, subCode, packet.Length);
        }

        if (mainCode != ServerMainCode || subCode is null)
        {
            return new(TakumiConnectServerRequestType.Unknown, header, mainCode, subCode, packet.Length);
        }

        if (ServerListSubCodes.Contains(subCode.Value))
        {
            return new(TakumiConnectServerRequestType.ServerList, header, mainCode, subCode, packet.Length);
        }

        if (ServerInfoSubCodes.Contains(subCode.Value))
        {
            return new(TakumiConnectServerRequestType.ServerInfo, header, mainCode, subCode, packet.Length);
        }

        return new(TakumiConnectServerRequestType.Unknown, header, mainCode, subCode, packet.Length);
    }

    /// <summary>
    /// Returns true when the packet requests server-list data.
    /// </summary>
    /// <param name="packet">The raw packet bytes.</param>
    public static bool IsServerListRequest(ReadOnlySpan<byte> packet)
        => Parse(packet).RequestType == TakumiConnectServerRequestType.ServerList;

    /// <summary>
    /// Returns true when the packet requests selected server endpoint info.
    /// </summary>
    /// <param name="packet">The raw packet bytes.</param>
    public static bool IsServerInfoRequest(ReadOnlySpan<byte> packet)
        => Parse(packet).RequestType == TakumiConnectServerRequestType.ServerInfo;
}

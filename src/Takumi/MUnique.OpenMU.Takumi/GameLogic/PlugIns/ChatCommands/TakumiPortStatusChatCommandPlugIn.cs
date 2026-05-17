// <copyright file="TakumiPortStatusChatCommandPlugIn.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace MUnique.OpenMU.Takumi.GameLogic.PlugIns.ChatCommands;

using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using MUnique.OpenMU.DataModel.Entities;
using MUnique.OpenMU.GameLogic;
using MUnique.OpenMU.GameLogic.PlugIns.ChatCommands;
using MUnique.OpenMU.PlugIns;
using MUnique.OpenMU.Takumi.Migration;

/// <summary>
/// Shows migration progress of legacy C++ features tracked in <see cref="LegacyPortRegistry"/>.
/// </summary>
[Guid("11D943F2-64F5-4882-AE38-73FE5B582E61")]
[PlugIn]
[Display(Name = "Takumi migration status command", Description = "Shows C++ to OpenMU migration progress.")]
[ChatCommandHelp(Command, "Shows migration status counts for Takumi C++ -> OpenMU port.", null)]
public sealed class TakumiPortStatusChatCommandPlugIn : IChatCommandPlugIn
{
    private const string Command = "/takumiport";

    /// <inheritdoc />
    public string Key => Command;

    /// <inheritdoc />
    public CharacterStatus MinCharacterStatusRequirement => CharacterStatus.GameMaster;

    /// <inheritdoc />
    public async ValueTask HandleCommandAsync(Player player, string command)
    {
        var entries = LegacyPortRegistry.GetAll();
        await player.ShowBlueMessageAsync($"Takumi port registry: {entries.Count} tracked features.").ConfigureAwait(false);

        foreach (var (status, count) in LegacyPortRegistry.GetStatusCounts())
        {
            await player.ShowBlueMessageAsync($"- {status}: {count}").ConfigureAwait(false);
        }
    }
}

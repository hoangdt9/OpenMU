// <copyright file="LegacyPortStatus.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace MUnique.OpenMU.Takumi.Migration;

/// <summary>
/// Progress status of a migrated legacy feature.
/// </summary>
public enum LegacyPortStatus
{
    /// <summary>
    /// No migration work has started yet.
    /// </summary>
    NotStarted,

    /// <summary>
    /// Migration is actively being implemented.
    /// </summary>
    InProgress,

    /// <summary>
    /// Implementation exists but functional parity checks are still pending.
    /// </summary>
    ValidationPending,

    /// <summary>
    /// Feature has been ported and validated against expected behavior.
    /// </summary>
    Ported,
}

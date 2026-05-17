// <copyright file="LegacyFeaturePort.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace MUnique.OpenMU.Takumi.Migration;

/// <summary>
/// Declares one migration work item from legacy C++ source to OpenMU extension code.
/// </summary>
/// <param name="LegacySource">Legacy source file name (Takumi C++).</param>
/// <param name="Feature">Feature or bounded context name.</param>
/// <param name="Subsystem">Legacy subsystem where the code currently lives.</param>
/// <param name="TargetArea">Target namespace/folder in OpenMU Takumi extension.</param>
/// <param name="Status">Current migration state.</param>
public sealed record LegacyFeaturePort(
    string LegacySource,
    string Feature,
    LegacySubsystem Subsystem,
    string TargetArea,
    LegacyPortStatus Status);

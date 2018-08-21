﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityPerformanceBenchmarkReporter.Entities;

namespace UnityPerformanceBenchmarkReporter
{
    public class MetadataValidator
    {
        public string[] PlayerSystemInfoResultFiles = Array.Empty<string>();
        public readonly Dictionary<string, Dictionary<string, string>> MismatchedPlayerSystemInfoValues = new Dictionary<string, Dictionary<string, string>>();
        public string[] PlayerSettingsResultFiles = Array.Empty<string>();
        public readonly Dictionary<string, Dictionary<string, string>> MismatchedPlayerSettingsValues = new Dictionary<string, Dictionary<string, string>>();
        public string[] QualitySettingsResultFiles = Array.Empty<string>();
        public readonly Dictionary<string, Dictionary<string, string>> MismatchedQualitySettingsValues = new Dictionary<string, Dictionary<string, string>>();
        public string[] ScreenSettingsResultFiles = Array.Empty<string>();
        public readonly Dictionary<string, Dictionary<string, string>> MismatchedScreenSettingsValues = new Dictionary<string, Dictionary<string, string>>();
        public string[] BuildSettingsResultFiles = Array.Empty<string>();
        public readonly Dictionary<string, Dictionary<string, string>> MismatchedBuildSettingsValues = new Dictionary<string, Dictionary<string, string>>();
        public string[] EditorVersionResultFiles = Array.Empty<string>();
        public readonly Dictionary<string, Dictionary<string, string>> MismatchedEditorVersionValues = new Dictionary<string, Dictionary<string, string>>();

        public bool MismatchesExist => PlayerSystemInfoResultFiles.Any() ||
                                       PlayerSystemInfoResultFiles.Any() ||
                                       QualitySettingsResultFiles.Any() ||
                                       ScreenSettingsResultFiles.Any() ||
                                       BuildSettingsResultFiles.Any() ||
                                       EditorVersionResultFiles.Any();

        public void ValidatePlayerSystemInfo(PerformanceTestRun testRun1, PerformanceTestRun testRun2, string firstTestRunResultPath, string xmlFileNamePath)
        {
            if (testRun1.PlayerSystemInfo.DeviceModel != testRun2.PlayerSystemInfo.DeviceModel)
            {
                AddMismatchPlayerMetadata(
                    ref PlayerSystemInfoResultFiles,
                    MismatchedPlayerSystemInfoValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.PlayerSystemInfo.DeviceModel),
                    testRun1.PlayerSystemInfo.DeviceModel,
                    testRun2.PlayerSystemInfo.DeviceModel);
            }
            if (testRun1.PlayerSystemInfo.GraphicsDeviceName != testRun2.PlayerSystemInfo.GraphicsDeviceName)
            {
                AddMismatchPlayerMetadata(
                    ref PlayerSystemInfoResultFiles,
                    MismatchedPlayerSystemInfoValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.PlayerSystemInfo.GraphicsDeviceName),
                    testRun1.PlayerSystemInfo.GraphicsDeviceName,
                    testRun2.PlayerSystemInfo.GraphicsDeviceName);
            }
            if (testRun1.PlayerSystemInfo.ProcessorCount != testRun2.PlayerSystemInfo.ProcessorCount)
            {
                AddMismatchPlayerMetadata(
                    ref PlayerSystemInfoResultFiles,
                    MismatchedPlayerSystemInfoValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.PlayerSystemInfo.ProcessorCount),
                    testRun1.PlayerSystemInfo.ProcessorCount.ToString(),
                    testRun2.PlayerSystemInfo.ProcessorCount.ToString());
            }
            if (testRun1.PlayerSystemInfo.ProcessorType != testRun2.PlayerSystemInfo.ProcessorType)
            {
                AddMismatchPlayerMetadata(
                    ref PlayerSystemInfoResultFiles,
                    MismatchedPlayerSystemInfoValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.PlayerSystemInfo.ProcessorType),
                    testRun1.PlayerSystemInfo.ProcessorType,
                    testRun2.PlayerSystemInfo.ProcessorType);
            }
            if (testRun1.PlayerSystemInfo.XrDevice != testRun2.PlayerSystemInfo.XrDevice)
            {
                AddMismatchPlayerMetadata(
                    ref PlayerSystemInfoResultFiles, 
                    MismatchedPlayerSystemInfoValues, 
                    firstTestRunResultPath, 
                    xmlFileNamePath, 
                    nameof(testRun1.PlayerSystemInfo.XrDevice), 
                    testRun1.PlayerSystemInfo.XrDevice, 
                    testRun2.PlayerSystemInfo.XrDevice);
            }
            if (testRun1.PlayerSystemInfo.XrModel != testRun2.PlayerSystemInfo.XrModel)
            {
                AddMismatchPlayerMetadata(
                    ref PlayerSystemInfoResultFiles, 
                    MismatchedPlayerSystemInfoValues, 
                    firstTestRunResultPath, 
                    xmlFileNamePath, 
                    nameof(testRun1.PlayerSystemInfo.XrModel), 
                    testRun1.PlayerSystemInfo.XrModel, 
                    testRun2.PlayerSystemInfo.XrModel);
            }
        }

        public void ValidatePlayerSettings(PerformanceTestRun testRun1, PerformanceTestRun testRun2, string firstTestRunResultPath, string xmlFileNamePath)
        {
            if (testRun1.PlayerSettings.AndroidMinimumSdkVersion != testRun2.PlayerSettings.AndroidMinimumSdkVersion)
            {
                AddMismatchPlayerMetadata(
                    ref PlayerSettingsResultFiles,
                    MismatchedPlayerSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.PlayerSettings.AndroidMinimumSdkVersion),
                    testRun1.PlayerSettings.AndroidMinimumSdkVersion,
                    testRun2.PlayerSettings.AndroidMinimumSdkVersion);
            }
            if (testRun1.PlayerSettings.AndroidTargetSdkVersion != testRun2.PlayerSettings.AndroidTargetSdkVersion)
            {
                AddMismatchPlayerMetadata(
                    ref PlayerSettingsResultFiles,
                    MismatchedPlayerSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.PlayerSettings.AndroidTargetSdkVersion),
                    testRun1.PlayerSettings.AndroidTargetSdkVersion,
                    testRun2.PlayerSettings.AndroidTargetSdkVersion);
            }

            var srcEnabledXrTargetsString = 
                testRun1.PlayerSettings.EnabledXrTargets != null && testRun1.PlayerSettings.EnabledXrTargets.Any() ? 
                    string.Join(',', testRun1.PlayerSettings.EnabledXrTargets.ToArray()) : 
                    string.Empty;
            var targetEnabledXrTargetsString = 
                testRun2.PlayerSettings.EnabledXrTargets != null && testRun2.PlayerSettings.EnabledXrTargets.Any() ? 
                    string.Join(',', testRun2.PlayerSettings.EnabledXrTargets.ToArray()) : 
                    string.Empty;

            if (srcEnabledXrTargetsString != targetEnabledXrTargetsString)
            {
                AddMismatchPlayerMetadata(
                    ref PlayerSettingsResultFiles,
                    MismatchedPlayerSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.PlayerSettings.EnabledXrTargets),
                    srcEnabledXrTargetsString,
                    targetEnabledXrTargetsString);
            }

            if (testRun1.PlayerSettings.GpuSkinning != testRun2.PlayerSettings.GpuSkinning)
            {
                AddMismatchPlayerMetadata(
                    ref PlayerSettingsResultFiles,
                    MismatchedPlayerSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.PlayerSettings.GpuSkinning),
                    testRun1.PlayerSettings.GpuSkinning.ToString(),
                    testRun2.PlayerSettings.GpuSkinning.ToString());
            }
            if (testRun1.PlayerSettings.GraphicsApi != testRun2.PlayerSettings.GraphicsApi)
            {
                AddMismatchPlayerMetadata(
                    ref PlayerSettingsResultFiles,
                    MismatchedPlayerSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.PlayerSettings.GraphicsApi),
                    testRun1.PlayerSettings.GraphicsApi,
                    testRun2.PlayerSettings.GraphicsApi);
            }
            if (testRun1.PlayerSettings.GraphicsJobs != testRun2.PlayerSettings.GraphicsJobs)
            {
                AddMismatchPlayerMetadata(
                    ref PlayerSettingsResultFiles,
                    MismatchedPlayerSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.PlayerSettings.GraphicsJobs),
                    testRun1.PlayerSettings.GraphicsJobs.ToString(),
                    testRun2.PlayerSettings.GraphicsJobs.ToString());
            }
            if (testRun1.PlayerSettings.MtRendering != testRun2.PlayerSettings.MtRendering)
            {
                AddMismatchPlayerMetadata(
                    ref PlayerSettingsResultFiles,
                    MismatchedPlayerSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.PlayerSettings.MtRendering),
                    testRun1.PlayerSettings.MtRendering.ToString(),
                    testRun2.PlayerSettings.MtRendering.ToString());
            }
            if (testRun1.PlayerSettings.RenderThreadingMode != testRun2.PlayerSettings.RenderThreadingMode)
            {
                AddMismatchPlayerMetadata(
                    ref PlayerSettingsResultFiles,
                    MismatchedPlayerSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.PlayerSettings.RenderThreadingMode),
                    testRun1.PlayerSettings.RenderThreadingMode,
                    testRun2.PlayerSettings.RenderThreadingMode);
            }
            if (testRun1.PlayerSettings.ScriptingBackend != testRun2.PlayerSettings.ScriptingBackend)
            {
                AddMismatchPlayerMetadata(
                    ref PlayerSettingsResultFiles,
                    MismatchedPlayerSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.PlayerSettings.ScriptingBackend),
                    testRun1.PlayerSettings.ScriptingBackend,
                    testRun2.PlayerSettings.ScriptingBackend);
            }
            if (testRun1.PlayerSettings.StereoRenderingPath != testRun2.PlayerSettings.StereoRenderingPath)
            {
                AddMismatchPlayerMetadata(
                    ref PlayerSettingsResultFiles,
                    MismatchedPlayerSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.PlayerSettings.StereoRenderingPath),
                    testRun1.PlayerSettings.StereoRenderingPath,
                    testRun2.PlayerSettings.StereoRenderingPath);
            }
            if (testRun1.PlayerSettings.VrSupported != testRun2.PlayerSettings.VrSupported)
            {
                AddMismatchPlayerMetadata(
                    ref PlayerSettingsResultFiles,
                    MismatchedPlayerSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.PlayerSettings.VrSupported),
                    testRun1.PlayerSettings.VrSupported.ToString(),
                    testRun2.PlayerSettings.VrSupported.ToString());
            }
        }

        public void ValidateQualitySettings(PerformanceTestRun testRun1, PerformanceTestRun testRun2, string firstTestRunResultPath, string xmlFileNamePath)
        {
            if (testRun1.QualitySettings.AnisotropicFiltering != testRun2.QualitySettings.AnisotropicFiltering)
            {
                AddMismatchPlayerMetadata(
                    ref QualitySettingsResultFiles,
                    MismatchedQualitySettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.QualitySettings.AnisotropicFiltering),
                    testRun1.QualitySettings.AnisotropicFiltering,
                    testRun2.QualitySettings.AnisotropicFiltering);
            }
            if (testRun1.QualitySettings.AntiAliasing != testRun2.QualitySettings.AntiAliasing)
            {
                AddMismatchPlayerMetadata(
                    ref QualitySettingsResultFiles,
                    MismatchedQualitySettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.QualitySettings.AntiAliasing),
                    testRun1.QualitySettings.AntiAliasing.ToString(),
                    testRun2.QualitySettings.AntiAliasing.ToString());
            }
            if (testRun1.QualitySettings.BlendWeights != testRun2.QualitySettings.BlendWeights)
            {
                AddMismatchPlayerMetadata(
                    ref QualitySettingsResultFiles,
                    MismatchedQualitySettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.QualitySettings.BlendWeights),
                    testRun1.QualitySettings.BlendWeights,
                    testRun2.QualitySettings.BlendWeights);
            }
            if (testRun1.QualitySettings.ColorSpace != testRun2.QualitySettings.ColorSpace)
            {
                AddMismatchPlayerMetadata(
                    ref QualitySettingsResultFiles,
                    MismatchedQualitySettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.QualitySettings.ColorSpace),
                    testRun1.QualitySettings.ColorSpace,
                    testRun2.QualitySettings.ColorSpace);
            }
            if (testRun1.QualitySettings.Vsync != testRun2.QualitySettings.Vsync)
            {
                AddMismatchPlayerMetadata(
                    ref QualitySettingsResultFiles,
                    MismatchedQualitySettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.QualitySettings.Vsync),
                    testRun1.QualitySettings.Vsync.ToString(),
                    testRun2.QualitySettings.Vsync.ToString());
            }
        }

        public void ValidateScreenSettings(PerformanceTestRun testRun1, PerformanceTestRun testRun2, string firstTestRunResultPath, string xmlFileNamePath)
        {
            if (testRun1.ScreenSettings.Fullscreen != testRun2.ScreenSettings.Fullscreen)
            {
                AddMismatchPlayerMetadata(
                    ref ScreenSettingsResultFiles,
                    MismatchedScreenSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.ScreenSettings.Fullscreen),
                    testRun1.ScreenSettings.Fullscreen.ToString(),
                    testRun2.ScreenSettings.Fullscreen.ToString());
            }
            if (testRun1.ScreenSettings.ScreenHeight != testRun2.ScreenSettings.ScreenHeight)
            {
                AddMismatchPlayerMetadata(
                    ref ScreenSettingsResultFiles,
                    MismatchedScreenSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.ScreenSettings.ScreenHeight),
                    testRun1.ScreenSettings.ScreenHeight.ToString(),
                    testRun2.ScreenSettings.ScreenHeight.ToString());
            }
            if (testRun1.ScreenSettings.ScreenRefreshRate != testRun2.ScreenSettings.ScreenRefreshRate)
            {
                AddMismatchPlayerMetadata(
                    ref ScreenSettingsResultFiles,
                    MismatchedScreenSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.ScreenSettings.ScreenRefreshRate),
                    testRun1.ScreenSettings.ScreenRefreshRate.ToString(),
                    testRun2.ScreenSettings.ScreenRefreshRate.ToString());
            }
            if (testRun1.ScreenSettings.ScreenWidth != testRun2.ScreenSettings.ScreenWidth)
            {
                AddMismatchPlayerMetadata(
                    ref ScreenSettingsResultFiles,
                    MismatchedScreenSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.ScreenSettings.ScreenWidth),
                    testRun1.ScreenSettings.ScreenWidth.ToString(),
                    testRun2.ScreenSettings.ScreenWidth.ToString());
            }
        }

        public void ValidateBuildSettings(PerformanceTestRun testRun1, PerformanceTestRun testRun2, string firstTestRunResultPath, string xmlFileNamePath)
        {
            if (testRun1.BuildSettings.Platform != testRun2.BuildSettings.Platform)
            {
                AddMismatchPlayerMetadata(
                    ref BuildSettingsResultFiles,
                    MismatchedBuildSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.BuildSettings.Platform),
                    testRun1.BuildSettings.Platform,
                    testRun2.BuildSettings.Platform);
            }

            if (testRun1.BuildSettings.BuildTarget != testRun2.BuildSettings.BuildTarget)
            {
                AddMismatchPlayerMetadata(
                    ref BuildSettingsResultFiles,
                    MismatchedBuildSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.BuildSettings.BuildTarget),
                    testRun1.BuildSettings.BuildTarget,
                    testRun2.BuildSettings.BuildTarget);
            }

            if (testRun1.BuildSettings.DevelopmentPlayer != testRun2.BuildSettings.DevelopmentPlayer)
            {
                AddMismatchPlayerMetadata(
                    ref BuildSettingsResultFiles,
                    MismatchedBuildSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.BuildSettings.DevelopmentPlayer),
                    testRun1.BuildSettings.DevelopmentPlayer.ToString(),
                    testRun2.BuildSettings.DevelopmentPlayer.ToString());
            }

            if (testRun1.BuildSettings.AndroidBuildSystem != testRun2.BuildSettings.AndroidBuildSystem)
            {
                AddMismatchPlayerMetadata(
                    ref BuildSettingsResultFiles,
                    MismatchedBuildSettingsValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.BuildSettings.AndroidBuildSystem),
                    testRun1.BuildSettings.AndroidBuildSystem,
                    testRun2.BuildSettings.AndroidBuildSystem);
            }
        }

        public void ValidateEditorVersion(PerformanceTestRun testRun1, PerformanceTestRun testRun2, string firstTestRunResultPath, string xmlFileNamePath)
        {
            if (testRun1.EditorVersion.FullVersion != testRun2.EditorVersion.FullVersion)
            {
                AddMismatchPlayerMetadata(
                    ref EditorVersionResultFiles,
                    MismatchedEditorVersionValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.EditorVersion.FullVersion),
                    testRun1.EditorVersion.FullVersion,
                    testRun2.EditorVersion.FullVersion);
            }

            if (testRun1.EditorVersion.Branch != testRun2.EditorVersion.Branch)
            {
                AddMismatchPlayerMetadata(
                    ref EditorVersionResultFiles,
                    MismatchedEditorVersionValues,
                    firstTestRunResultPath,
                    xmlFileNamePath,
                    nameof(testRun1.EditorVersion.Branch),
                    testRun1.EditorVersion.Branch,
                    testRun2.EditorVersion.Branch);
            }
        }

        private void AddMismatchPlayerMetadata(
            ref string[] infoResultFiles,
            Dictionary<string, Dictionary<string, string>> mismatchedPlayerValues,
            string firstTestRunResultPath,
            string xmlFileNamePath,
            string playerInfoName,
            string refPlayerInfoValue,
            string mismatchedPlayerInfoValue)
        {
            EnsureResultFileTracked(ref infoResultFiles, firstTestRunResultPath);
            EnsureResultFileTracked(ref infoResultFiles, xmlFileNamePath);

            if (!mismatchedPlayerValues.ContainsKey(playerInfoName))
            {
                mismatchedPlayerValues.Add(
                    playerInfoName,
                    new Dictionary<string, string>
                    {
                        {firstTestRunResultPath, refPlayerInfoValue}
                    });
            }

            mismatchedPlayerValues[playerInfoName].Add(xmlFileNamePath, mismatchedPlayerInfoValue ?? string.Empty);
        }

        private void EnsureResultFileTracked(ref string[] infoResultFiles, string resultPath)
        {
            if (!infoResultFiles.Contains(resultPath))
            {
                Array.Resize(ref infoResultFiles, infoResultFiles.Length + 1);
                infoResultFiles[infoResultFiles.Length - 1] = resultPath;
            }
        }
    }
}

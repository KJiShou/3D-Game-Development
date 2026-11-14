using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class ReleaseBuild
{
    public static void Build()
    {
        var target = EditorUserBuildSettings.activeBuildTarget;
        if (target != BuildTarget.StandaloneWindows64 && target != BuildTarget.WebGL)
            throw new InvalidOperationException("Release target must be Windows64 or WebGL.");
        string platform = target == BuildTarget.WebGL ? "WebGL" : "Windows64";
        string folder = Path.Combine("Builds", "v" + PlayerSettings.bundleVersion, platform);
        Directory.CreateDirectory(folder);
        if (target == BuildTarget.WebGL)
            PlayerSettings.WebGL.decompressionFallback = true;
        var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
        {
            scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(scene => scene.path).ToArray(),
            locationPathName = target == BuildTarget.WebGL ? folder : Path.Combine(folder, "ChichisBizarreAdventure.exe"),
            target = target,
            options = BuildOptions.None
        });
        var summary = report.summary;
        File.WriteAllText(Path.Combine("Builds", platform + "-summary.txt"),
            $"Result: {summary.result}\nErrors: {summary.totalErrors}\nWarnings: {summary.totalWarnings}\nBytes: {summary.totalSize}\nDuration: {summary.totalTime}\n");
        if (summary.result != BuildResult.Succeeded)
            throw new Exception(platform + " release build failed: " + summary.result);
        Debug.Log("RELEASE_BUILD_SUCCEEDED: " + folder);
    }
}

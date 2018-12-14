using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;

class AutoBuildScript
{
    static string[] SCENES = FindEnabledEditorScenes();

    static string APP_NAME = "YourProject";
    static string TARGET_DIR = "target";

    [MenuItem("Custom/CI/Build Windows")]
    static void PerformWindowsBuild()
    {
        string target_dir = APP_NAME + ".exe";
        GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }

    static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
    {
#pragma warning disable CS0618 // Type or member is obsolete
        EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
#pragma warning restore CS0618 // Type or member is obsolete
        BuildReport res = BuildPipeline.BuildPlayer(scenes, target_dir, build_target, build_options);
        if (res.name.Length > 0)
        {
            throw new System.Exception("BuildPlayer failure: " + res);
        }
    }
}


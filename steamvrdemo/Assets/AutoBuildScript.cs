using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace AutoBuilding
{
    class AutoBuildScript
    {
        static string[] SCENES = FindEnabledEditorScenes();
        static string TARGET_DIR = @"C:\Users\DemoPC\Desktop\build\build";

        [MenuItem("Custom/CI/Win")]
        static void PerformWindowsBuild()
        {
            string ext = ".exe";
            GenericBuild(SCENES, TARGET_DIR + ext, BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64, BuildOptions.None);
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

        static void GenericBuild(string[] scenes, string target_dir, BuildTargetGroup buildTargetGroup, BuildTarget build_target, BuildOptions build_options)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(buildTargetGroup, build_target);
            BuildPipeline.BuildPlayer(scenes, target_dir, build_target, build_options);
        }
    }
}
#endif
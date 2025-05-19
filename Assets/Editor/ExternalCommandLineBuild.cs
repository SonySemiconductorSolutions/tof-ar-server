/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2018,2019,2020,2021 Sony Semiconductor Solutions Corporation.
 *
 */
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ExternalCommandLineBuild
{
    static void PerformBuild()
    {
#if UNITY_2018_1_OR_NEWER
        //needed for some libraries
        PlayerSettings.allowUnsafeCode = true;
#endif
#if UNITY_ANDROID
        PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android, ApiCompatibilityLevel.NET_4_6);
#elif UNITY_IOS
        PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.iOS, ApiCompatibilityLevel.NET_4_6);
#endif


        string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
        List<string> allDefines = definesString.Split(';').ToList();

        Debug.Log("allDefines " + string.Join(";", allDefines.ToArray()));

        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", allDefines.ToArray()));

        string dir = Application.dataPath + "/DebugServer/Scenes";
        string subDir = "Assets/DebugServer/Scenes/";
        if (!Directory.Exists(dir))
        {
            dir = Application.dataPath + "/Scenes";
            subDir = "Assets/Scenes/";
        }
        
        string mainScene = GetArg("-mainScene");
        string scenesInOrder = GetArg("-scenesInOrder");

        List<string> scenes = new List<string>();
        if (scenesInOrder != null)
        {
            foreach (var name in scenesInOrder.Split(' '))
            {
                scenes.Add(subDir + name);
            }
        }
        else
        {
            var info = new DirectoryInfo(dir);
            var fileInfo = info.GetFiles();
            foreach (var file in fileInfo)
            {
                if (file.Extension == ".unity")
                {
                    if (mainScene != null && mainScene == file.Name)
                    {
                        scenes.Insert(0, subDir + file.Name);
                    }
                    else
                    {
                        scenes.Add(subDir + file.Name);
                    }
                }
            }
        }

#if UNITY_ANDROID
        EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
        BuildPipeline.BuildPlayer(scenes.ToArray(), GetArg("-apkOutputPath"), BuildTarget.Android, BuildOptions.None);
#elif UNITY_IOS
        BuildPipeline.BuildPlayer(scenes.ToArray(), GetArg("-apkOutputPath"), BuildTarget.iOS, BuildOptions.None);
#endif
    }

    private static string GetArg(string name)
    {
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name && args.Length > i + 1)
            {
                return args[i + 1];
            }
        }
        return null;
    }
}

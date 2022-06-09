/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2018,2019,2020,2021 Sony Semiconductor Solutions Corporation.
 *
 */
using System;
using System.IO;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
#if UNITY_2018_3_OR_NEWER
using UnityEditor.Build.Reporting;
#endif
#endif

#if UNITY_2018_3_OR_NEWER
public class OutputBuildDateTime : IPreprocessBuildWithReport
#else
    public class OutputBuildDateTime : IPreprocessBuild
#endif
{
    const string ResouceDirPath = "Assets/Resources";
    const string FileName = "BuildDateTime.txt";

    public int callbackOrder
    {
        get { return 0; }
    }

#if UNITY_2018_3_OR_NEWER
    public void OnPreprocessBuild(BuildReport report)
#else
    public void OnPreprocessBuild(BuildTarget target, string path)
#endif
    {
        try
        {
            if (!Directory.Exists(ResouceDirPath))
            {
                Directory.CreateDirectory(ResouceDirPath);
            }

            string filePath = Path.Combine(ResouceDirPath, FileName);
            using (var stream = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                var text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                stream.Write(text);
                stream.Close();
            }
        }
        catch (ArgumentException e)
        {
            Debug.LogError(e);
        }
        catch (IOException e)
        {
            Debug.LogError(e);
        }
        finally
        {
            AssetDatabase.Refresh();
        }
    }
}
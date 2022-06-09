/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2018,2019,2020,2021 Sony Semiconductor Solutions Corporation.
 *
 */

using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BuildInfo : MonoBehaviour
{

    public Text buildInfoText;

#if UNITY_EDITOR
    string filePath = $"Assets{Path.DirectorySeparatorChar}Resources{Path.DirectorySeparatorChar}BuildDateTime.txt";
#elif UNITY_ANDROID || UNITY_IOS
    string fileName = "BuildDateTime";
#endif

    void Awake()
    {
#if UNITY_EDITOR
        buildInfoText.text = string.Format("Build Version: {0}", ReadText(filePath));
#elif UNITY_ANDROID || UNITY_IOS
        TextAsset savedTextFile = Resources.Load<TextAsset>(fileName);
        buildInfoText.text = string.Format("Build Version: {0}", savedTextFile.text);
#endif
    }

#if UNITY_EDITOR
    public string ReadText(string textFile)
    {
        using (StreamReader reader = new StreamReader(textFile))
        {
            string textRead = TofAr.V0.EditorUtils.ReadOutput(reader);
            return textRead;
        }
    }
#endif
}

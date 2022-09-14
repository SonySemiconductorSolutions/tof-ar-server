/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2018,2019,2020,2021 Sony Semiconductor Solutions Corporation.
 *
 */
using UnityEngine;
using UnityEngine.UI;

public class GetUnityEditorVersion : MonoBehaviour
{
    public Text unityVersionText;

    void Start()
    {
        unityVersionText.text = "Unity Editor version: " + Application.unityVersion;
    }


}

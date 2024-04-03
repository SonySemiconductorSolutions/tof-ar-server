/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2018,2019,2020,2021,2023 Sony Semiconductor Solutions Corporation.
 *
 */
using UnityEngine;
using UnityEngine.UI;

namespace TofArServer
{
    public class GetUnityEditorVersion : MonoBehaviour
    {
        public Text unityVersionText;

        void Start()
        {
            unityVersionText.text = "Unity Editor version: " + Application.unityVersion;
        }
    }
}

﻿/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2022,2023 Sony Semiconductor Solutions Corporation.
 *
 */

using UnityEngine;
using UnityEngine.UI;

namespace TofArServer
{
    [RequireComponent(typeof(Text))]
    public class AppVersion : MonoBehaviour
    {
        void Start()
        {
            var txt = GetComponent<Text>();
            string str = Application.version;
            txt.text = $"App Version : {str}";
        }
    }
}

﻿/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2022,2023 Sony Semiconductor Solutions Corporation.
 *
 */

using TofAr.V0;
using TofAr.V0.Tof;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TofArServer
{
    public class DepthSceneManager : MonoBehaviour
    {
        public void BackButton()
        {
            TofArTofManager.Instance.StopStream();

            Destroy(TofArTofManager.Instance.gameObject);
            Destroy(TofArManager.Instance.gameObject);

            SceneManager.LoadSceneAsync("Main");
        }
    }
}

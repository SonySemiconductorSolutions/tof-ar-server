/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2022 Sony Semiconductor Solutions Corporation.
 *
 */

using System.Collections;
using System.Collections.Generic;
using TofAr.V0;
using TofAr.V0.Tof;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DepthSceneManager : MonoBehaviour
{
    public void BackButton()
    {
        TofArTofManager.Instance.StopStream();

        Destroy(TofArManager.Instance.gameObject);
        Destroy(TofArManager.Instance.gameObject);

        SceneManager.LoadSceneAsync("Main");
    }
}

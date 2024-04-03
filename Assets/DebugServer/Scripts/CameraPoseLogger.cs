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
    public class CameraPoseLogger : MonoBehaviour
    {
        public Text message;

        void Update()
        {
            var t = Camera.main.transform;
            var euler = t.rotation.eulerAngles;
            this.message.text = $"Pos: x:{t.position.x,8:0.000} y:{t.position.y,8:0.000} z:{t.position.z,8:0.000}\n" +
                                $"Rot: x:{euler.x,8:0.000} y:{euler.y,8:0.000} z:{euler.z,8:0.000}\n" +
                                $"Accel: x:{Input.acceleration.x,8:0.000} y:{Input.acceleration.y,8:0.000} z:{Input.acceleration.z,8:0.000}";
        }
    }
}

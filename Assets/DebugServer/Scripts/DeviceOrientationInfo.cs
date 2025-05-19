/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2023,2024 Sony Semiconductor Solutions Corporation.
 *
 */

using TofAr.V0;
using UnityEngine;
using UnityEngine.UI;

namespace TofArServer
{
    public class DeviceOrientationInfo : MonoBehaviour
    {
        private Text txt;

        private DeviceOrientation callbackCurrentOrientation = DeviceOrientation.Unknown;

        private void Start()
        {
            txt = GetComponent<Text>();
            callbackCurrentOrientation = TofArManager.Instance?.GetProperty<DeviceOrientationsProperty>()?.deviceOrientation ?? DeviceOrientation.Unknown;

            TofArManager.Instance.preInternalSessionStart.AddListener(() =>
            {
                TofArManager.OnDeviceOrientationUpdated += OnTofArDeviceRotated;
            });
            TofArManager.Instance.postInternalSessionStop.AddListener(() =>
            {
                TofArManager.OnDeviceOrientationUpdated -= OnTofArDeviceRotated;
            });
        }

        private void Update()
        {
            UpdateText();
        }

        void UpdateText()
        {
            string str = $"Device Orientation\n" +
                         $"Current: {Input.deviceOrientation} | Sent to client: {callbackCurrentOrientation}";
            txt.text = str;
        }

        private void OnTofArDeviceRotated(DeviceOrientation prev, DeviceOrientation current)
        {
            callbackCurrentOrientation = current;
        }
    }
}

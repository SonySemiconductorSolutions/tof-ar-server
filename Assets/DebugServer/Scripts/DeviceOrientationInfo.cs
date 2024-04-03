/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2023 Sony Semiconductor Solutions Corporation.
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

        private void OnEnable()
        {
            TofArManager.OnDeviceOrientationUpdated += OnTofArDeviceRotated;
        }

        private void OnDisable()
        {
            TofArManager.OnDeviceOrientationUpdated -= OnTofArDeviceRotated;
        }

        private void Start()
        {
            txt = GetComponent<Text>();
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

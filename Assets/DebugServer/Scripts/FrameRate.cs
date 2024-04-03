/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2018,2019,2020,2021,2023 Sony Semiconductor Solutions Corporation.
 *
 */
using TofAr.V0.Tof;
using UnityEngine;
using UnityEngine.UI;

namespace TofArServer
{
    public class FrameRate : MonoBehaviour
    {
        public Text txtFrameRate;
        public Slider sliderFrameRate;

        private void Start()
        {
            OnEndDragging();
        }

        public void ValueChanged(float val)
        {
            txtFrameRate.text = sliderFrameRate.value.ToString("N0");
        }

        public void OnEndDragging()
        {
            txtFrameRate.text = sliderFrameRate.value.ToString("N0");
            TofArTofManager.Instance.SetProperty<FrameRateProperty>(new FrameRateProperty() { desiredFrameRate = sliderFrameRate.value });
        }
    }
}

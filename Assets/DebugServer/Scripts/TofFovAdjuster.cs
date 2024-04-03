/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2022,2023 Sony Semiconductor Solutions Corporation.
 *
 */

using TofAr.V0.Tof;

namespace TofArServer
{
    public class TofFovAdjuster : FovAdjuster
    {
        protected override void OnLoadCalib(CalibrationSettingsProperty calibration)
        {
            var intrinsics = calibration.d;
            fy = intrinsics.fy;
            if (fy > 0)
            {
                height = calibration.depthHeight;
                Adjust();
            }
        }
    }
}


/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2022,2023 Sony Semiconductor Solutions Corporation.
 *
 */

#if UNITY_IOS
using System.Runtime.InteropServices;
#endif

namespace TofArServer
{
    /// <summary>
    /// システムの熱状態
    /// </summary>
    public enum ThermalState
    {
        Nominal,
        Fair,
        Serious,
        Critical
    }

    public static class ProcessInfoGetter
    {
        /// <summary>
        /// システムの熱状態を取得する
        /// </summary>
        public static ThermalState ThermalState
        {
            get
            {
#if !UNITY_EDITOR && UNITY_IOS
            return (ThermalState) ThermalStateNative();
#else
                return ThermalState.Nominal;
#endif
            }
        }

#if UNITY_IOS
    [DllImport("__Internal", EntryPoint = "thermalState")]
    static extern int ThermalStateNative();
#endif
    }
}


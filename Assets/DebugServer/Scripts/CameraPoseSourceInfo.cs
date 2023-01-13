/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2022 Sony Semiconductor Solutions Corporation.
 *
 */

using System.Collections;
using System.Collections.Generic;
using TofAr.V0.Slam;
using UnityEngine;
using UnityEngine.UI;

public class CameraPoseSourceInfo : MonoBehaviour
{
    /// <summary>
    /// 表示を更新する間隔(単位:s)
    /// </summary>
    public float TimeUpdate = 1;

    /// <summary>
    /// テキストの色
    /// </summary>
    public Color[] colors = new Color[]
    {
        Color.blue,
        Color.green,
        Color.yellow,
        Color.cyan
    };

    Text txt;
    float time;

    /// <summary>
    /// Scriptが有効化された時に実行される動作(Unity標準関数)
    /// </summary>
    void OnEnable()
    {
        time = 0;
    }

    /// <summary>
    /// アプリ起動後(Awake後)の動作(Unity標準関数)
    /// </summary>
    void Start()
    {
        txt = GetComponent<Text>();
    }

    /// <summary>
    /// メインスレッドで定期実行される動作(Unity標準関数)
    /// </summary>
    void Update()
    {
        time += Time.deltaTime;
        if (time >= TimeUpdate)
        {
            UpdateText();
            time -= TimeUpdate;
        }
    }

    /// <summary>
    /// テキストを更新する
    /// </summary>
    void UpdateText()
    {
        var state = TofArSlamManager.Instance.CameraPoseSource;

        var color = colors[(int)state];
        var colorHtml = ColorUtility.ToHtmlStringRGB(color);
        string str = $"Camera Pose Source: <color=#{colorHtml}>{state.ToString()}</color>";
        txt.text = str;
    }

}

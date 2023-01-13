/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2022 Sony Semiconductor Solutions Corporation.
 *
 */

using UnityEngine;
using UnityEngine.UI;


public class ThermalStateInfo : MonoBehaviour
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
        Color.red
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
#if !UNITY_IOS
            this.gameObject.SetActive(false);       
#endif
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
        var state = ProcessInfoGetter.ThermalState;

        var color = colors[(int)state];
        var colorHtml = ColorUtility.ToHtmlStringRGB(color);
        string str = $"Thermal State: <color=#{colorHtml}>{state.ToString()}</color>";
        txt.text = str;
    }
}

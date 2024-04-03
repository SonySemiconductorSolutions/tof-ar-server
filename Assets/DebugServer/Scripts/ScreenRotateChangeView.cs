/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2023 Sony Semiconductor Solutions Corporation.
 *
 */

using TofAr.V0;
using TofArSettings.UI;
using UnityEngine;

namespace TofArServer
{
    public class ScreenRotateChangeView : MonoBehaviour
    {
        public RectTransform info;
        public RectTransform state;

        private void OnEnable()
        {
            TofArManager.OnScreenOrientationUpdated += OnTofArScreenRotated;
            ChangeRectTransformAnchors(Screen.orientation);
        }

        private void OnDisable()
        {
            TofArManager.OnScreenOrientationUpdated -= OnTofArScreenRotated;
        }

        private void OnTofArScreenRotated(ScreenOrientation prev, ScreenOrientation current)
        {
            ChangeRectTransformAnchors(current);
        }

        private void ChangeRectTransformAnchors(ScreenOrientation orientation)
        {
            if (orientation == ScreenOrientation.LandscapeLeft || orientation == ScreenOrientation.LandscapeRight)
            {
                info.anchorMin = new Vector2(0, 0.2f);
                info.anchorMax = new Vector2(0.5f, 1.0f);

                state.anchorMin = new Vector2(0.5f, 0);
                state.anchorMax = new Vector2(1.0f, 1.0f);
                state.offsetMin = new Vector2(0, 0);
            }
            else
            {
                info.anchorMin = new Vector2(0, 0.55f);
                info.anchorMax = new Vector2(1.0f, 1.0f);

                state.anchorMin = new Vector2(0, 0);
                state.anchorMax = new Vector2(1.0f, 0.55f);
                state.offsetMin = new Vector2(0, 80);
            }
        }
    }
}

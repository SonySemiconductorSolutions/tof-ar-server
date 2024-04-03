/*
 * SPDX-License-Identifier: (Apache-2.0 OR GPL-2.0-only)
 *
 * Copyright 2023 Sony Semiconductor Solutions Corporation.
 *
 */

using UnityEngine;

namespace TofArServer
{
    public class SafeArea : MonoBehaviour
    {
        protected Rect area;
        protected Vector2 latestSafeAreaSize;

        [SerializeField]
        private RectTransform safeAreaRt;

        private Vector2 SafeAreaSize
        {
            get
            {
                return (safeAreaRt) ? new Vector2(safeAreaRt.rect.width,
                    safeAreaRt.rect.height) : Vector2.zero;
            }
        }

        private void Start()
        {
            AdjustSafeArea(Screen.safeArea);
        }

        private void Update()
        {
            AdjustSafeArea(Screen.safeArea);
        }

        private void AdjustSafeArea(Rect newArea)
        {
            if (area == newArea && latestSafeAreaSize == SafeAreaSize)
            {
                return;
            }

            if (Application.isEditor)
            {
                return;
            }

            area = newArea;

            float scWidth = Screen.width;
            float scHeight = Screen.height;

            var anchorMin = area.position;
            var anchorMax = area.position + area.size;
            anchorMin.x /= scWidth;
            anchorMin.y /= scHeight;
            anchorMax.x /= scWidth;
            anchorMax.y /= scHeight;
            safeAreaRt.anchoredPosition = Vector2.zero;
            safeAreaRt.anchorMin = anchorMin;
            safeAreaRt.anchorMax = anchorMax;

            latestSafeAreaSize = SafeAreaSize;
        }
    }
}

using System;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

namespace Utility
{
    public class UnityFuncs
    {
        private const double AfterResizeDelayMs = 500;
        private static DateTime LastModified;
        
        
        private static int LastWidth = 0;
        private static int LastHeight = 0;

        public static void EnforceAspectRatio()
        {
            if (Screen.fullScreen) return;
            
            int width = Screen.width;
            int height = Screen.height;

            DateTime now = DateTime.Now;
            if (width != LastWidth || height != LastHeight)
            {
                LastModified = now;
            }

            if (now.Subtract(LastModified).TotalMilliseconds > AfterResizeDelayMs)
            {
                ApplyAspectRatio(width, height);
            }
            
            LastWidth = width;
            LastHeight = height;
        }

        public static void ApplyAspectRatio(int width, int height)
        {
            int adjustedHeight = Mathf.RoundToInt(width / (16f / 9f));
            if (height != adjustedHeight)
            {
                Screen.SetResolution(Screen.width, adjustedHeight, false);
            }
        }
    }
}
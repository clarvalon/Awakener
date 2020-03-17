// Module_ScreenShot - Type "override" followed by space to see list of C# methods to implement
using static Awakener.GlobalBase;
using System.Diagnostics;
using static Awakener.Module_ScreenShot;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using MessagePack;
using Clarvalon.XAGE.Global;

namespace Awakener
{
    public partial class Module_ScreenShot
    {
        // Fields
        public String previousValue;
        public int count;
        public Overlay overlay;
        public String screenshotMode;

        // Methods
        public void SetScreenshotMode(String mode)
        {
            screenshotMode = mode;
        }

        public void UpdateCountAndOverlay(bool showOverlay)
        {
            count += 1;
            if (!showOverlay)
                return;
            String text = StringFormatAGS("%04d", count + 1);
            overlay.SetText(50, 0, 65535, text);
        }

        public void ScreenGrabOnChange(String mode, bool showOverlay)
        {
            if (previousValue == null)
                previousValue = "";
            if (screenshotMode == null)
                return;
            if (overlay == null && showOverlay)
                overlay = Overlay.CreateTextual(2, 2, 50, eFontFont0, 1, "", "overlay");
            if (mode == "PLAYER")
            {
                String newValue = StringFormatAGS("%d %d %d %d", player.x, player.y, player.View, player.Loop);
                if (newValue != previousValue)
                {
                    UpdateCountAndOverlay(showOverlay);
                    previousValue = newValue;
                    newValue = StringFormatAGS("%04d x%d y%d v%d l%d", count, player.x, player.y, player.View, player.Loop);
                    String fileName = StringFormatAGS("player %s.BMP", newValue);
                    SaveScreenShot(fileName);
                }
            }
            else if (mode == "PLAYERFRAME")
            {
                String newValue = StringFormatAGS("%d %d %d %d %d", player.x, player.y, player.View, player.Loop, player.Frame);
                if (newValue != previousValue)
                {
                    UpdateCountAndOverlay(showOverlay);
                    previousValue = newValue;
                    newValue = StringFormatAGS("%04d x%d y%d v%d l%d f%d", count, player.x, player.y, player.View, player.Loop, player.Frame);
                    String fileName = StringFormatAGS("playerframe %s.BMP", newValue);
                    SaveScreenShot(fileName);
                }
            }
            else if (mode == "EVERYFRAME")
            {
                UpdateCountAndOverlay(showOverlay);
                String fileName = StringFormatAGS("everyframe %04d.BMP", count);
                SaveScreenShot(fileName);
            }
        }

        public void repeatedly_execute_always()
        {
            ScreenGrabOnChange(screenshotMode, true);
        }

    }

    #region Globally Exposed Items

    public partial class GlobalBase
    {
        // Expose ScreenShot methods so they can be used without instance prefix
        public static void SetScreenshotMode(String mode)
        {
            ScreenShot.SetScreenshotMode(mode);
        }


    }

    #endregion

}

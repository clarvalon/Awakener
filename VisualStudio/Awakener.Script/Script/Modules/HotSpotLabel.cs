// Module_HotSpotLabel - Type "override" followed by space to see list of C# methods to implement
using static Awakener.GlobalBase;
using System.Diagnostics;
using static Awakener.Module_HotSpotLabel;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using MessagePack;
using Clarvalon.XAGE.Global;

namespace Awakener
{
    public partial class Module_HotSpotLabel
    {
        // Fields

        // Methods
        public void HotspotLabelTextSetter()
        {
            InventoryItem item = InventoryItem.GetAtScreenXY(mouse.x, mouse.y);
            if (item != null)
            {
                HotspotLabel.Text = "";
                return;
            }
            LocationType lt = GetLocationType(mouse.x, mouse.y);
            if (lt == eLocationNothing)
            {
                HotspotLabel.Text = Game.GetLocationName(mouse.x, mouse.y);
                return;
            }
            if (player.ActiveInventory != null)
            {
                if (lt == eLocationCharacter)
                    HotspotLabel.Text = StringFormatAGS("Give %s to %s", player.ActiveInventory.Name, Game.GetLocationName(mouse.x, mouse.y));
                else 
                    HotspotLabel.Text = StringFormatAGS("Use %s on %s", player.ActiveInventory.Name, Game.GetLocationName(mouse.x, mouse.y));
            }
            else 
                HotspotLabel.Text = Game.GetLocationName(mouse.x, mouse.y);
        }

        public void UpdateHotSpotLabel()
        {
            HotspotLabelTextSetter();
            int newX = mouse.x - (HotspotLabel.Width / 2);
            int newY = mouse.y;
            newY -= 20;
            if (CurrentInputType == UserInputType.Touch)                newY -= 10;
            if (newY < 2)
                newY = 2;
            int textWidth = GetTextWidth(HotspotLabel.Text, eFontFont3);
            int labelWidth = HotspotLabel.Width;
            int halfDiff = (labelWidth - textWidth) / 2;
            int halfTextWidth = textWidth / 2;
            if (newX + halfDiff < 2)
                newX = 2 - halfDiff;
            if (newX + (HotspotLabel.Width / 2) + (textWidth / 2) > 318)
                newX = 318 - (textWidth / 2) - (HotspotLabel.Width / 2);
            HotspotLabel.SetPosition(newX,  newY);
        }

    }

    #region Globally Exposed Items

    public partial class GlobalBase
    {
        // Expose HotSpotLabel methods so they can be used without instance prefix
        public static void HotspotLabelTextSetter()
        {
            HotSpotLabel.HotspotLabelTextSetter();
        }

        public static void UpdateHotSpotLabel()
        {
            HotSpotLabel.UpdateHotSpotLabel();
        }


    }

    #endregion

}

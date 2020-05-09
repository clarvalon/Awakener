// Module_Inventory - Type "override" followed by space to see list of C# methods to implement
using static Awakener.GlobalBase;
using System.Diagnostics;
using static Awakener.Module_Inventory;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using MessagePack;
using Microsoft.Xna.Framework.Input;
using Clarvalon.XAGE.Global;

namespace Awakener
{
    public partial class Module_Inventory
    {
        // Fields
        public int InvTransparencyBlocking = 80;
        public int InvTransparencyActive;
        public int InvTransparencyTransitionRate = 5;

        // Methods
        public void InventoryGraphicSelect()
        {
            if (player.ActiveInventory == iSalt)
                iSalt.Graphic = 374;
            else 
                iSalt.Graphic = 110;
            if (player.ActiveInventory == iChip)
                iChip.Graphic = 378;
            else 
                iChip.Graphic = 103;
            if (player.ActiveInventory == iDagger)
                iDagger.Graphic = 379;
            else 
                iDagger.Graphic = 105;
            if (player.ActiveInventory == iFlag)
                iFlag.Graphic = 380;
            else 
                iFlag.Graphic = 107;
            if (player.ActiveInventory == iHalberd)
                iHalberd.Graphic = 381;
            else 
                iHalberd.Graphic = 108;
            if (player.ActiveInventory == iPennies)
                iPennies.Graphic = 382;
            else 
                iPennies.Graphic = 109;
            if (player.ActiveInventory == iCola)
                iCola.Graphic = 383;
            else 
                iCola.Graphic = 104;
            if (player.ActiveInventory == iSpirit)
                iSpirit.Graphic = 384;
            else 
                iSpirit.Graphic = 111;
            if (player.ActiveInventory == iDollar)
                iDollar.Graphic = 385;
            else 
                iDollar.Graphic = 106;
            if (player.ActiveInventory == iNote)
                iNote.Graphic = 386;
            else 
                iNote.Graphic = 119;
        }

        public void SelectInventory(InventoryItem item)
        {
            PlaySound(17);
            player.ActiveInventory = item;
            InventoryGraphicSelect();
        }

        public void DeSelectInventory()
        {
            PlaySound(18);
            player.ActiveInventory = null;
            InventoryGraphicSelect();
        }

        public int GetActiveInventoryPosition()
        {
            if (InventoryWindow1.ItemCount == 0)
                return -1;
            int i = 0;
            while (i < InventoryWindow1.ItemCount)
            {
                InventoryItem inv = InventoryWindow1.ItemAtIndex[i];
                if (player.ActiveInventory == inv)
                    return i;
                i += 1;
            }
            return -1;
        }

        public void SelectNextInventoryItem()
        {
            if (InventoryWindow1.ItemCount == 0)
                return;
            if (player.ActiveInventory == null)
            {
                SelectInventory(InventoryWindow1.ItemAtIndex[0]);
            }
            else 
            {
                int activeInvPos = GetActiveInventoryPosition();
                activeInvPos += 1;
                if (activeInvPos >= InventoryWindow1.ItemCount)
                    DeSelectInventory();
                else 
                    SelectInventory(InventoryWindow1.ItemAtIndex[activeInvPos]);
            }
        }

        public void SelectPreviousInventoryItem()
        {
            if (InventoryWindow1.ItemCount == 0)
                return;
            if (player.ActiveInventory == null)
            {
                SelectInventory(InventoryWindow1.ItemAtIndex[InventoryWindow1.ItemCount - 1]);
            }
            else 
            {
                int activeInvPos = GetActiveInventoryPosition();
                activeInvPos -= 1;
                if (activeInvPos < 0)
                    DeSelectInventory();
                else 
                    SelectInventory(InventoryWindow1.ItemAtIndex[activeInvPos]);
            }
        }

        public void ExtensionMethod_ChangeTransparency(GUI thisItem, int amount)
        {
            int newTransparency = thisItem.Transparency + amount;
            if (newTransparency > 100)
                newTransparency = 100;
            if (newTransparency < 0)
                newTransparency = 0;
            thisItem.Transparency = newTransparency;
        }

        public void TransitionInventoryVisible()
        {
            if (gGuibar.Transparency > InvTransparencyActive)
            {
                gGuibar.ChangeTransparency(-InvTransparencyTransitionRate);
            }
        }

        public void TransitionInventoryInvisible()
        {
            if (gGuibar.Transparency < InvTransparencyBlocking)
            {
                gGuibar.ChangeTransparency(InvTransparencyTransitionRate);
            }
        }

    }

    #region Globally Exposed Items

    public partial class GlobalBase
    {
        // Expose Inventory methods so they can be used without instance prefix
        public static void InventoryGraphicSelect()
        {
            Inventory.InventoryGraphicSelect();
        }

        public static void SelectInventory(InventoryItem item)
        {
            Inventory.SelectInventory(item);
        }

        public static void DeSelectInventory()
        {
            Inventory.DeSelectInventory();
        }

        public static int GetActiveInventoryPosition()
        {
            return Inventory.GetActiveInventoryPosition();
        }

        public static void SelectNextInventoryItem()
        {
            Inventory.SelectNextInventoryItem();
        }

        public static void SelectPreviousInventoryItem()
        {
            Inventory.SelectPreviousInventoryItem();
        }

        public static void TransitionInventoryVisible()
        {
            Inventory.TransitionInventoryVisible();
        }

        public static void TransitionInventoryInvisible()
        {
            Inventory.TransitionInventoryInvisible();
        }


    }

    #endregion

    #region Extension Methods Wrapper (AGS workaround)

    public static partial class ExtensionMethods
    {
        public static void ChangeTransparency(this GUI thisItem, int amount)
        {
            GlobalBase.Inventory.ExtensionMethod_ChangeTransparency(thisItem, amount);
        }

    }

    #endregion

}

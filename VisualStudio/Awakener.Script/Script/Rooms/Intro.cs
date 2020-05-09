// Room_Intro - Type "override" followed by space to see list of C# methods to implement
using static Awakener.GlobalBase;
using static Awakener.Room_Intro;
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
    public partial class Room_Intro // 2
    {
        // Fields
        public bool saveGameExists;

        // Methods
        public override void room_Load()
        {
            oTitle.Transparency = 100;
            cFadi.Transparency = 100;
            oAwakener.Transparency = 100;
            oContinue.Transparency = 100;
            oNewgame.Transparency = 100;
            oQuit.Transparency = 100;
            oPorted.Transparency = 100;
            gGuibar.Visible = false;
            if (Game.GetSaveSlotDescription(1) != null)
            {
                saveGameExists = true;
                oContinue.Visible = true;
            }
        }

        public override void room_FirstLoad()
        {
            int trans = oTitle.Transparency;
            if (!saveGameExists)
            {
                StartCutscene(eSkipESCOnly);
                Wait(40);
                while (trans > 0)
                {
                    trans = trans - 5;
                    oTitle.Transparency = trans;
                    Wait(1);
                }
                Wait(120);
                while (trans < 100)
                {
                    trans = trans + 5;
                    oTitle.Transparency = trans;
                    Wait(1);
                }
                EndCutscene();
                oTitle.Graphic = 368;
                StartCutscene(eSkipESCOnly);
                Wait(40);
                while (trans > 0)
                {
                    trans = trans - 5;
                    oTitle.Transparency = trans;
                    oPorted.Transparency = trans;
                    Wait(1);
                }
                Wait(120);
                while (trans < 100)
                {
                    trans = trans + 5;
                    oTitle.Transparency = trans;
                    oPorted.Transparency = trans;
                    Wait(1);
                }
                EndCutscene();
                Wait(40);
            }
            oTitle.Visible = false;
            while (trans > 0)
            {
                trans = trans - 5;
                oAwakener.Transparency = trans;
                oNewgame.Transparency = trans;
                oContinue.Transparency = trans;
                oQuit.Transparency = trans;
                Wait(1);
            }
        }

        public override void oNewgame_Interact()
        {
            cFadi.ChangeRoom(1, 353, 168);
        }

        public override void oContinue_Interact()
        {
            RestoreGameSlot(1);
        }

        public override void oQuit_Interact()
        {
            QuitGame(0);
        }

        public override void room_RepExec()
        {
            Object obj = Object.GetAtScreenXY(mouse.x, mouse.y);
            if (obj == oNewgame)
                oNewgame.Graphic = 376;
            else 
                oNewgame.Graphic = 366;
            if (obj == oContinue)
                oContinue.Graphic = 375;
            else 
                oContinue.Graphic = 359;
            if (obj == oQuit)
                oQuit.Graphic = 377;
            else 
                oQuit.Graphic = 367;
        }

    }

    #region Globally Exposed Items

    public partial class GlobalBase
    {

    }

    #endregion

}

// Room_Outro - Type "override" followed by space to see list of C# methods to implement
using static Awakener.GlobalBase;
using static Awakener.Room_Outro;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using MessagePack;
using Clarvalon.XAGE.Global;

namespace Awakener
{
    public partial class Room_Outro // 3
    {
        // Fields

        // Methods
        public override void room_Load()
        {
            cFadi.Transparency = 100;
            oWords.Transparency = 100;
            gGuibar.Visible = false;
            oQuit.Transparency = 100;
        }

        public override void repeatedly_execute_always()
        {
        }

        public override void room_FirstLoad()
        {
            StartCutscene(eSkipESCOnly);
            Wait(80);
            int trans = oWords.Transparency;
            while (trans > 0)
            {
                trans = trans - 5;
                oWords.Transparency = trans;
                Wait(1);
            }
            Wait(160);
            while (trans < 100)
            {
                trans = trans + 5;
                oWords.Transparency = trans;
                Wait(1);
            }
            EndCutscene();
            oWords.Graphic = 363;
            StartCutscene(eSkipESCOnly);
            Wait(80);
            while (trans > 0)
            {
                trans = trans - 5;
                oWords.Transparency = trans;
                Wait(1);
            }
            Wait(160);
            while (trans < 100)
            {
                trans = trans + 5;
                oWords.Transparency = trans;
                Wait(1);
            }
            EndCutscene();
            oWords.Graphic = 360;
            StartCutscene(eSkipESCOnly);
            Wait(80);
            while (trans > 0)
            {
                trans = trans - 5;
                oWords.Transparency = trans;
                Wait(1);
            }
            Wait(160);
            while (trans < 100)
            {
                trans = trans + 5;
                oWords.Transparency = trans;
                Wait(1);
            }
            EndCutscene();
            oWords.Graphic = 361;
            oWords.Y = 230;
            StartCutscene(eSkipESCOnly);
            Wait(80);
            while (trans > 0)
            {
                trans = trans - 5;
                oWords.Transparency = trans;
                Wait(1);
            }
            Wait(160);
            while (trans < 100)
            {
                trans = trans + 5;
                oWords.Transparency = trans;
                Wait(1);
            }
            EndCutscene();
            oWords.Graphic = 364;
            oQuit.Visible = true;
            oWords.Y = 200;
            Wait(80);
            while (trans > 0)
            {
                trans = trans - 5;
                oWords.Transparency = trans;
                oQuit.Transparency = trans;
                Wait(1);
            }
        }

        public override void oQuit_Interact()
        {
            QuitGame(0);
        }

        public override void room_RepExec()
        {
            Object obj = Object.GetAtScreenXY(mouse.x, mouse.y);
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

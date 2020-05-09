// Conv_dSnacks - Type "override" followed by space to see list of C# methods to implement
using static Awakener.GlobalBase;
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
    public partial class Conv_dSnacks
    {
        // Methods
        public override void OptionS()
        {
            cFadi.Say("Good morning, Miss.");
            cSnacks.Say("Hey.");
            GLOBAL.dialog_request(2);
        }

        public override void Option1()
        {
            cSnacks.Say("Chips.");
            GLOBAL.dialog_request(2);
            cFadi.Say("Are they good?");
            cSnacks.Say("Nope.");
            GLOBAL.dialog_request(2);
            dSnacks.SetOptionState(1, DialogOptionState.eOptionOff);
            dSnacks.SetOptionState(2, DialogOptionState.eOptionOn);
        }

        public override void Option2()
        {
            cSnacks.Say("Salt.");
            GLOBAL.dialog_request(2);
            cFadi.Say("They're too salty?");
            cSnacks.Say("Nope.");
            GLOBAL.dialog_request(2);
            cFadi.Say("Not salty enough?");
            cSnacks.Say("Yep.");
            GLOBAL.dialog_request(2);
            GLOBAL.dialog_request(6);
        }

        public override void Option3()
        {
            cSnacks.Say("Nope.");
            GLOBAL.dialog_request(2);
            cFadi.Say("But you've got lots of them!");
            cSnacks.Say("Mine.");
            GLOBAL.dialog_request(2);
        }

        public override void Option4()
        {
            cSnacks.Say("Bye.");
            GLOBAL.dialog_request(2);
            GLOBAL.dialog_request(3);
            StopDialog();
        }

        public override void Option5()
        {
            cSnacks.Say("Nope.");
            cFadi.Say("But you're standing right here!");
            cSnacks.Say("Yep.");
            dSnacks.SetOptionState(5, DialogOptionState.eOptionOff);
            dSnacks.SetOptionState(4, DialogOptionState.eOptionOn);
        }

    }
}

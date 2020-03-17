// Conv_dStatue - Type "override" followed by space to see list of C# methods to implement
using static Awakener.GlobalBase;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using MessagePack;
using Clarvalon.XAGE.Global;

namespace Awakener
{
    public partial class Conv_dStatue
    {
        // Methods
        public override void OptionS()
        {
            cFadi.Say("Ummm... hello? Miss Statue Lady?");
            cStatue.Say("Curses! Yes, what is it, child?");
        }

        public override void Option1()
        {
            cStatue.Say("I was enchanted after I was sculpted, so that I might have sight, speech and hearing.");
            cFadi.Say("Who gave you all of these abilities?");
            cStatue.Say("I don't know, but I curse them for not giving me some clothes!");
            GLOBAL.dialog_request(4);
            dStatue.SetOptionState(1, DialogOptionState.eOptionOff);
            dStatue.SetOptionState(2, DialogOptionState.eOptionOn);
        }

        public override void Option2()
        {
            cStatue.Say("Can you not see that I am naked? How would you like being stuck naked on this accursed platform?");
            cFadi.Say("...I guess I wouldn't, really...");
            cStatue.Say("Exactly! Every cursed passerby gawking at your bits... it's humiliating, I tell you.");
        }

        public override void Option3()
        {
            cStatue.Say("I'm cursed to hear every cursed thing. Of course I know his password!");
            cFadi.Say("Can you tell me?");
            cStatue.Say("Look, get me something to wear, and I'll tell you the accursed password.");
        }

        public override void Option4()
        {
            cStatue.Say("Fine! Leave me on this cursed platform!");
            StopDialog();
        }

    }
}

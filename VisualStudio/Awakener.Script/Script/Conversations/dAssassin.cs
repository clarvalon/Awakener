// Conv_dAssassin - Type "override" followed by space to see list of C# methods to implement
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
    public partial class Conv_dAssassin
    {
        // Methods
        public override void OptionS()
        {
            cFadi.Say("Hello, Miss Assassin lady!");
            cAssassin.Say("You got the message, kid?");
        }

        public override void Option1()
        {
            cAssassin.Say("I was told that the messenger would meet me here. I haven't got all day kid, I need to know my target.");
            cFadi.Say("Wow! You're on a proper mission!");
            cAssassin.Say("I would be if I knew who I'm supposed to waste, kid.");
        }

        public override void Option2()
        {
            cAssassin.Say("Waiting for the messenger, aren't I, kid?");
            cFadi.Say("I haven't seen any messengers around. I bet they've been caught and killed!");
            cAssassin.Say("They will be if they don't hurry up and deliver the note, kid.");
        }

        public override void Option3()
        {
            cAssassin.Say("Later, kid.");
            GLOBAL.dialog_request(5);
            StopDialog();
        }

    }
}

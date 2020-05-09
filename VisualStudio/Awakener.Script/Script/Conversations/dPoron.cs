// Conv_dPoron - Type "override" followed by space to see list of C# methods to implement
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
    public partial class Conv_dPoron
    {
        // Methods
        public override void OptionS()
        {
            cFadi.Say("Excuse me? Mr Poron? Are you home?");
            cPoron.Say("Ya needs to tell me the password.");
        }

        public override void Option1()
        {
            cPoron.Say("Yeah, looks, like I said, I needs a password, see?");
            cFadi.Say("Why do you need a password?");
            cPoron.Say("The fuzz, they're onta me, see? I gots to start covering my hiney, see?");
        }

        public override void Option2()
        {
            cPoron.Say("Well, looks. I'd love ta help ya out, see, but I gots to knows I can trust ya.");
            cFadi.Say("But it's Fadi! You've known me for years, Poron!");
            cPoron.Say("Look, gets yourself the password and come back, ok?");
        }

        public override void Option3()
        {
            cPoron.Say("Yeah, looks, come back when ya know the password.");
            StopDialog();
        }

    }
}

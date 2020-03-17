// Conv_dAunt1 - Type "override" followed by space to see list of C# methods to implement
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
    public partial class Conv_dAunt1
    {
        // Methods
        public override void OptionS()
        {
        }

        public override void Option1()
        {
            cAunt.Say("No, I would have remembered his face. I'm not sure where he came from, but I've never seen him before.");
        }

        public override void Option2()
        {
            cAunt.Say("Yes, I called out to him and even threw water on his face - that usually works for the drunkards.");
            cFadi.Say("He must be very tired.");
            cAunt.Say("Well, at least he is still breathing.");
        }

        public override void Option3()
        {
            cAunt.Say("Hah, to you every stranger looks like an adventurer, Fadi.");
            cFadi.Say("Well look at his clothes! He looks like a woodsman. I bet he's killed monsters!");
            cAunt.Say("Whatever he is, he can't stay here. Go see Poron for some spirit of hartshorn. That ought to wake him up.");
            cFadi.Say("Yes! It's like my very own quest!");
            cAunt.Say("*sigh*");
            StopDialog();
        }

    }
}

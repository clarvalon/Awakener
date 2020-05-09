// Conv_dAunt2 - Type "override" followed by space to see list of C# methods to implement
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
    public partial class Conv_dAunt2
    {
        // Methods
        public override void OptionS()
        {
            cAunt.Say("How are you going with getting the Spirit of Hartshorn, Fadi?");
        }

        public override void Option1()
        {
            cAunt.Say("Don't take too long, dear, I'd like to wake this fellow up before I open the tavern.");
            cFadi.Say("I'm doing my best, but it is a challenging quest, Auntie Sylvia.");
            cAunt.Say("I am sure it is, Fadi.");
        }

        public override void Option2()
        {
            cAunt.Say("I wasn't aware that Poron had a password, Fadi.");
            cFadi.Say("He says he needs one before he'll talk to me.");
            cAunt.Say("Well, I'm sure someone around here must have overheard it. Try asking around.");
            GLOBAL.dialog_request(8);
        }

        public override void Option3()
        {
            cAunt.Say("You do that, Fadi.");
            StopDialog();
        }

    }
}

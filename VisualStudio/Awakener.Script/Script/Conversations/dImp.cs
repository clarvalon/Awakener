// Conv_dImp - Type "override" followed by space to see list of C# methods to implement
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
    public partial class Conv_dImp
    {
        // Methods
        public override void OptionS()
        {
            cFadi.Say("Umm, hello there, Mr Imp...");
            cImp.Say("Yes, yes, yes, hello there! Did you have a wish?");
        }

        public override void Option1()
        {
            cImp.Say("I heard you make a wish, yes, yes, yes, and I am so eager to grant your wishes!");
            cFadi.Say("I would have thought that it would be harder to get wishes granted...");
            cImp.Say("Yes, yes, yes, it was, until all the wish scammers made people unwilling to wish anymore.");
            dImp.SetOptionState(1, DialogOptionState.eOptionOff);
            dImp.SetOptionState(2, DialogOptionState.eOptionOn);
        }

        public override void Option2()
        {
            cImp.Say("Yes, yes, yes, you know, \"U r 1,000,000th wisher, wish here for lucky prize1!!!1!\"");
            cFadi.Say("Oh, I've heard about those. Apparently they're everywhere.");
            cImp.Say("Yes, yes, yes, terrible business. Now I can't even give wishes away!");
        }

        public override void Option3()
        {
            cImp.Say("Ah, yes, yes, yes, of course. I would grant your wish, if only I had more mana!");
            cFadi.Say("You don't have any mana?");
            cImp.Say("Yes, yes, yes, I used it all up teleporting here. Tell you what, you find me a potion of mana and I'll grant your wish!");
        }

        public override void Option4()
        {
            cImp.Say("Yes, yes, yes, see you in a bit, I expect.");
            StopDialog();
        }

    }
}

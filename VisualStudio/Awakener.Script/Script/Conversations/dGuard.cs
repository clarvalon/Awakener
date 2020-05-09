// Conv_dGuard - Type "override" followed by space to see list of C# methods to implement
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
    public partial class Conv_dGuard
    {
        // Methods
        public override void OptionS()
        {
            cFadi.Say("Hello, Mr Guard Sir!");
            cGuard.Say("Good morning, hello, greetings, salutations, young man!");
        }

        public override void Option1()
        {
            cGuard.Say("This building, this store, these premises, this establishment, it is under investigation.");
            cFadi.Say("Who is investigating it?");
            cGuard.Say("The king, the crown, the government, the authorities. Indeed, it seems Poron may have been selling elixirs he is not licensed to sell.");
        }

        public override void Option2()
        {
            cGuard.Say("You can't, you shouldn't, you mustn't, you shan't, young sir.");
            cFadi.Say("Why can I not speak with him?");
            cGuard.Say("We need to investigate his business, his trade, his sales, his activities first.");
            GLOBAL.dialog_request(7);
        }

        public override void Option3()
        {
            cGuard.Say("Goodbye, good day, farewell, so long, young man!");
            cGuard.Say("I shall remain here, doing my dull, boring, tiresome, tedious duty!");
            GLOBAL.dialog_request(1);
            StopDialog();
        }

    }
}

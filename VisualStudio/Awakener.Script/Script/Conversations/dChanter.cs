// Conv_dChanter - Type "override" followed by space to see list of C# methods to implement
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
    public partial class Conv_dChanter
    {
        // Methods
        public override void OptionS()
        {
            cFadi.Say("Good morning, Mr Chanter.");
            cMerchant.Say("Pennies for sale, a dollar for three! Pennies for sale, get your pennies from me!");
        }

        public override void Option1()
        {
            cMerchant.Say("Pennies for sale, shiny new copper! Pennies for sale, get your pennies here shopper!");
            cFadi.Say("But who would spend a dollar to buy just three pennies?");
            cMerchant.Say("Pennies for sale, fresh from the mint! Pennies for sale, watch them glisten and glint!");
        }

        public override void Option2()
        {
            cMerchant.Say("Pennies for sale, only pennies today! Pennies for sale, get your pennies I say!");
            cFadi.Say("Not exactly the most diverse range of produce, really.");
            cMerchant.Say("Pennies for sale, I've got pennies in store! Pennies for sale, spend one coin for three more!");
        }

        public override void Option3()
        {
            cMerchant.Say("Pennies for sale, pennies my friend! Pennies for sale, please do come again!");
            StopDialog();
        }

        public override void Option4()
        {
            cMerchant.Say("Pennies for sale, not passwords today! Pennies for sale, I know not, I say!");
            cFadi.Say("Awww.");
            dChanter.SetOptionState(4, DialogOptionState.eOptionOff);
            dChanter.SetOptionState(3, DialogOptionState.eOptionOn);
        }

    }
}

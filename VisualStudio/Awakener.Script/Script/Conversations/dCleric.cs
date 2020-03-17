// Conv_dCleric - Type "override" followed by space to see list of C# methods to implement
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
    public partial class Conv_dCleric
    {
        // Methods
        public override void OptionS()
        {
            cFadi.Say("Hello there, Mr Cleric.");
            cCleric.Say("\"And he welcomed those who had gathered before him\", book of Dhall, verse seven.");
        }

        public override void Option1()
        {
            cCleric.Say("\"He stayed in the area until he had located the ritual blade - source of the unholy presence he felt\", book of Virgil, verse nineteen.");
            cFadi.Say("You're looking for something nasty around here?");
            cCleric.Say("\"And it was seen that his words were true\", book of Pharod, verse three.");
            dCleric.SetOptionState(2, DialogOptionState.eOptionOn);
        }

        public override void Option2()
        {
            cCleric.Say("\"And he was shown the way by the divine being, who spoke to him with visions of the truth\", book of Arkanis, verse thirty eight.");
            cFadi.Say("If a deity is guiding you, then why haven't you found it already?");
            cCleric.Say("\"Patiently he waited whilst his master finished his dining\", book of Minsc, verse seventeen.");
            cFadi.Say("...your God is busy eating? How can that be?");
            cCleric.Say("\"And it was shown to those who believed that their Lord would be on lunch break between ten and two\", book of Osprey, verse six.");
        }

        public override void Option3()
        {
            cCleric.Say("\"As the prophet had spoken, those who once stood in His presence strayed into the unknown\", book of Ebenezer, verse twenty four.");
            StopDialog();
        }

    }
}

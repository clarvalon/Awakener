// Room_Town - Type "override" followed by space to see list of C# methods to implement
using static Awakener.GlobalBase;
using static Awakener.Room_Town;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using MessagePack;
using Clarvalon.XAGE.Global;

namespace Awakener
{
    public partial class Room_Town // 1
    {
        // Fields
        public int sleepy;

        // Methods
        public override void room_Load()
        {
            SetTimer(1, 160);
            SetTimer(2, 80);
            SetTimer(3, 400);
            SetViewport(0, 0);
            region[1].Enabled = false;
            hDoor.Enabled = false;
            cPoron.Transparency = 100;
            cBarrel.Transparency = 100;
            cStatue.Transparency = 100;
            cSleeper.Animate(0, 80, eRepeat, eNoBlock);
            cAssassin.FaceLocation(cAssassin.x, cAssassin.y+1, eBlock);
            gGuibar.Transparency = 100;
            gGuibar.Visible = true;
            cFadi.Transparency = 0;
        }

        public override void repeatedly_execute_always()
        {
            if (IsTimerExpired(1) && sleepy == 0 && guardtalking == false)
            {
                if (cGuard.Loop == 0)
                {
                    cGuard.Loop = 1;
                    cGuard.Animate(1, 3, eRepeat, eNoBlock);
                    SetTimer(1, 80);
                }
                else if (cGuard.Loop == 1)
                {
                    cGuard.Loop = 2;
                    SetTimer(1, 60);
                }
                else if (cGuard.Loop == 2)
                {
                    cGuard.Loop = 3;
                    cGuard.Animate(3, 3, eRepeat, eNoBlock);
                    SetTimer(1, 40);
                }
                else if (cGuard.Loop == 3)
                {
                    cGuard.Loop = 0;
                    SetTimer(1, 160);
                }
            }
            if (IsTimerExpired(2) && girltalking == false)
            {
                cSnacks.Animate(1, 3, eOnce, eNoBlock);
                int rand = Random(200);
                while (rand < 80)
                {
                    rand=Random(200);
                }
                SetTimer(2, rand);
            }
            if (IsTimerExpired(3) && assassintalk == false)
            {
                cAssassin.Animate(1, 3, eOnce, eNoBlock);
                int rand2 = Random(800);
                while (rand2 < 400)
                {
                    rand2=Random(800);
                }
                SetTimer(3, rand2);
            }
        }

        public override void oHalberd_Interact()
        {
            if (guardloser == true)
            {
                if (cGuard.Loop != 2)
                {
                    cFadi.Walk(571, 219, eBlock);
                    guardtalking = true;
                    cGuard.Loop = 0;
                    cGuard.Frame = 0;
                    cGuard.Say("Young sir! That pike, that weapon, that halberd, that lance, it is mine. I beseech you not to touch it!");
                    Wait(10);
                    guardtalking = false;
                    cFadi.Walk(560, 219, eBlock);
                    SetTimer(1, 80);
                }
                else if (cGuard.Loop == 2)
                {
                    sleepy = 1;
                    cFadi.Walk(572, 219, eBlock);
                    oHalberd.Visible = false;
                    cFadi.Animate(4, 3, eOnce, eBlock);
                    cFadi.AddInventory(iHalberd);
                    Wait(10);
                    cFadi.Walk(460, 219, eBlock);
                    Wait(10);
                    cFadi.Say("I have a huge weapon! I'm a *real* adventurer now!");
                    sleepy = 0;
                    SetTimer(1, 1);
                }
            }
            else if (guardloser == false)
            {
                cFadi.Say("I shouldn't rely on theft until I've at least tried diplomacy...");
            }
        }

        public override void room_AfterFadeIn()
        {
            StartCutscene(eSkipESCOnly);
            Wait(80);
            player.Walk(290, 170, eBlock, eAnywhere);
            player.Walk(290, 220, eBlock, eAnywhere);
            player.Walk(120, 220, eBlock, eAnywhere);
            Wait(20);
            player.Say("Auntie Sylvia! I finished all my chores and mother said I could come and spend the day with you!");
            Wait(10);
            cAunt.Say("Hello Fadi, it is good to see you. How kind of your mother to allow you to come and visit me.");
            cFadi.Say("We should go on an adven-");
            Wait(20);
            cFadi.FaceLocation(cFadi.x, cFadi.y-1);
            Wait(10);
            cFadi.Say("Who is that guy!?");
            Wait(10);
            cAunt.Say("I'm not sure, Fadi. He was out here sleeping when I woke up this morning.");
            Wait(10);
            cFadi.FaceCharacter(cAunt, eBlock);
            dAunt1.Start();
            ReleaseViewport();
            EndCutscene();
            region[1].Enabled = true;
        }

        public void region1_WalksOnto()
        {
            cFadi.StopMoving();
            Wait(10);
            cFadi.Say("Poron's place is at the other end of this street. Auntie Sylvia has an account with him, so I shouldn't need to pay for the spirit of hartshorn.");
            region[1].Enabled = false;
        }

        public override void region2_WalksOnto()
        {
            cFadi.StopMoving();
            Wait(10);
            cFadi.Say("Hey, what is a guard doing outside Poron's place? This looks like a job for Fadi, seasoned adventurer!");
            region[2].Enabled = false;
        }

        public override void region3_WalksOnto()
        {
            if (oHalberd.Visible == false)
            {
                cFadi.StopMoving();
                guardtalking = true;
                cGuard.Say("Dang, darn, damn, drat! Where could my weapon have gone!? I must find it, I do not wish to be on sewer patrol again!");
                Wait(10);
                cGuard.ChangeRoom(0);
                region[3].Enabled = false;
                hDoor.Enabled = true;
            }
        }

        public override void oSalt_Interact()
        {
            cFadi.Walk(94, 212, eBlock);
            Wait(10);
            cFadi.FaceObject(oSalt, eBlock);
            Wait(10);
            cFadi.Say("Every true adventurer knows that salt keeps away bad spirits! I'll take this just in case.");
            cFadi.Animate(6, 3, eOnce, eBlock);
            cFadi.FaceLocation(cFadi.x, cFadi.y-10, eBlock);
            oSalt.Visible = false;
            cFadi.AddInventory(iSalt);
        }

        public override void oSalt_Look()
        {
            cFadi.Say("The adventurer must have been doing something with this salt before he fell asleep! Maybe it is a clue?");
        }

        public override void hPlant_Interact()
        {
            if (Game.DoOnceOnly("knifefind"))
            {
                Wait(10);
                cFadi.FaceLocation(cFadi.x, cFadi.y -1, eBlock);
                cFadi.Say("Hey, there's a knife in this pot! I wonder who put that here?");
                Wait(10);
                cFadi.Walk(142, 186, eBlock, eAnywhere);
                cFadi.Animate(5, 3, eOnce, eBlock);
                Wait(10);
                cFadi.Walk(142, 198, eBlock, eAnywhere);
                cFadi.AddInventory(iDagger);
                Wait(10);
                cFadi.Say("It's a good thing I've been practising that pose for situations like this one!");
            }
            else 
                cFadi.Say("There isn't anything else in the pot.");
        }

        public override void oVender_UseInv()
        {
            cFadi.Walk(325, 230, eBlock);
            cFadi.FaceObject(oVender, eBlock);
            if (cFadi.ActiveInventory == iDollar)
            {
                cFadi.Say("The slot isn't big enough. Looks like the machine only accepts pennies.");
            }
            else if (cFadi.ActiveInventory == iPennies)
            {
                cFadi.Animate(19, 3, eOnce, eBlock);
                cFadi.Frame = 0;
                cFadi.Loop = 1;
                cFadi.LoseInventory(iPennies);
                Wait(10);
                oSoda.Visible = true;
                oSoda.Move(295, 231, 1, eBlock, eAnywhere);
                Wait(10);
                cCleric.ChangeRoom(0);
                ObjectsInRoom[4].Graphic = 352;
                oVender.Move(oVender.X, 0, 10, eBlock, eAnywhere);
                RestoreWalkableArea(2);
            }
            else 
                cFadi.Say("I don't think that's going to get me soda from the vending machine...");
        }

        public override void hStatue_Interact()
        {
            if (cPigeon.x == 377)
            {
                cFadi.Say("There's nothing I can do with the statue.");
            }
            else if (cPigeon.x != 377 && password == true)
            {
                cFadi.Say("No, I've annoyed her enough for today.");
            }
            else 
            {
                cFadi.FaceCharacter(cStatue, eBlock);
                dStatue.Start();
            }
        }

        public override void hDoor_Interact()
        {
            if (password == false)
            {
                dAunt2.SetOptionState(2, eOptionOn);
                dStatue.SetOptionState(3, eOptionOn);
                dPoron.Start();
            }
            else if (Game.DoOnceOnly("spirit get"))
            {
                cFadi.Say("I have the password, Mr Poron! It is Taste the Day!");
                cPoron.Say("Shhh, not so louds, chum! Anyone coulds be listening, see?");
                Wait(20);
                oDoor.Visible = true;
                cPoron.Transparency = 0;
                Wait(20);
                cPoron.Say("Now, was it Spirit of Hartshorn you wanted, chum?");
                cFadi.Say("Yes please, Mr Poron, sir.");
                cPoron.Say("Alright, but if anybody asks, ya didn't gets it from me, see?");
                cPoron.Animate(1, 3, eOnce, eBlock);
                cFadi.Animate(24, 3, eOnce, eBlock);
                cPoron.Animate(2, 3, eOnce, eBlock);
                cFadi.Animate(25, 3, eOnce, eBlock);
                cFadi.Loop = 2;
                cFadi.Frame = 0;
                cPoron.Loop = 0;
                cPoron.Frame = 0;
                cPoron.Say("I'll puts it on yer Aunt's account. Now gets outta here before someone starts asking questions, see?");
                cFadi.Say("Thank you, Mr Poron!");
                cFadi.AddInventory(iSpirit);
                Wait(20);
                oDoor.Visible = false;
                PlaySound(10);
                cPoron.Transparency = 100;
            }
            else 
                cFadi.Say("I already have the Spirit of Hartshorn.");
        }

        public override void oFlag_Interact()
        {
            cFadi.Walk(260, 194, eBlock);
            cFadi.FaceObject(oFlag, eBlock);
            if (oFlag.Graphic == 49)
            {
                cFadi.Say("It seems to be hooked on the top. I'll have to find something to unhook it with.");
            }
            else 
            {
                cFadi.FaceLocation(cFadi.x, cFadi.y-1, eBlock);
                cFadi.Say("This could be useful somehow...");
                cFadi.Animate(12, 3, eOnce, eBlock);
                cFadi.AddInventory(iFlag);
                oFlag.Visible = false;
                cFadi.Animate(13, 3, eOnce, eBlock);
            }
        }

        public override void oFlag_UseInv()
        {
            if (cFadi.ActiveInventory == iHalberd && oFlag.Graphic == 49)
            {
                cFadi.Walk(260, 194, eBlock);
                cFadi.FaceLocation(cFadi.x+1, cFadi.y, eBlock);
                cFadi.Say("This should help me unhook the flag!");
                cFadi.Animate(10, 3, eOnce, eBlock);
                PlaySound(1);
                oFlag.Move(oFlag.X, 187, 10, eBlock, eAnywhere);
                oFlag.Graphic = 50;
                cFadi.Animate(11, 3, eOnce, eBlock);
                cFadi.Loop = 2;
                cFadi.Frame = 0;
                cFadi.Say("Flags everywhere will tremble at the mere mention of my name!");
            }
            else if (oFlag.Graphic == 49 && cFadi.ActiveInventory != iHalberd)
            {
                cFadi.Say("That will not help me unhook the flag.");
            }
            else if (oFlag.Graphic != 49)
            {
                cFadi.Say("I can just pick it up now...");
            }
        }

        public override void hStatue_UseInv()
        {
            if (cFadi.ActiveInventory == iFlag && clotheswanted == true)
            {
                if (Game.DoOnceOnly("flagwish"))
                {
                    cFadi.Say("I wish there were some way I could get this flag onto that statue - it'd probably fit well enough to make a dress...");
                    Wait(20);
                    oSpell.Transparency = 100;
                    oSpell.Visible = true;
                    int trans = oSpell.Transparency;
                    PlaySound(15);
                    while (trans > 0)
                    {
                        trans = trans - 5;
                        oSpell.Transparency = trans;
                        Wait(1);
                    }
                    cImp.Transparency = 100;
                    cImp.ChangeRoom(1, 307, 125);
                    int trans2 = cImp.Transparency;
                    while (trans2 > 0)
                    {
                        trans2 = trans2 - 5;
                        cImp.Transparency = trans2;
                        Wait(1);
                    }
                    while (trans < 100)
                    {
                        trans = trans + 5;
                        oSpell.Transparency = trans;
                        Wait(1);
                    }
                    oSpell.Visible = false;
                    Wait(20);
                    cImp.Say("Did someone just make a wish?");
                    Wait(20);
                    cFadi.FaceCharacter(cImp);
                    cFadi.Say("Wow, a real imp! This *is* turning out to be a proper adventure!");
                }
                else 
                    cFadi.Say("I'm going to need help if I want to use that as a dress for the statue.");
            }
            else if (cFadi.ActiveInventory == iFlag)
            {
                cFadi.Say("I don't actually have a reason to want to hang the flag off the statue, although it'd probably fit.");
            }
            else 
                cFadi.Say("I really don't see what that is going to do to the statue.");
        }

        public override void region4_WalksOnto()
        {
            if (cAssassin.Loop == 5)
            {
                cAssassin.ChangeRoom(0);
                region[4].Enabled = false;
            }
        }

        public override void oSoda_Interact()
        {
            cFadi.Walk(311, 232, eBlock);
            cFadi.Animate(20, 3, eOnce, eBlock);
            oSoda.Visible = false;
            cFadi.Animate(22, 3, eOnce, eBlock);
            oLightray.Transparency = 100;
            oLightray.Visible = true;
            int trans = oLightray.Transparency;
            PlaySound(8);
            while (trans > 0)
            {
                trans = trans-10;
                oLightray.Transparency = trans;
                Wait(1);
            }
            Wait(60);
            cFadi.Animate(21, 3, eOnce, eBlock);
            while (trans < 100)
            {
                trans= trans+10;
                oLightray.Transparency = trans;
                Wait(1);
            }
            oLightray.Visible = false;
            cFadi.Loop = 1;
            cFadi.Frame = 0;
            cFadi.Say("I love it when it does that.");
            cFadi.AddInventory(iCola);
        }

        public override void hDoor_Look()
        {
            cFadi.Say("That is the door to Poron's house.");
        }

        public override void hDoor_UseInv()
        {
            if (cFadi.ActiveInventory == iDagger || cFadi.ActiveInventory == iHalberd)
            {
                cFadi.Say("I don't think Poron would be too happy with me using that to pry his door open...");
            }
            else 
                cFadi.Say("Using that on the door will achieve nothing.");
        }

        public override void hStatue_Look()
        {
            cFadi.FaceLocation(cFadi.x, cFadi.y+1, eBlock);
            cFadi.Say("Come on, you can see the statue. Don't make me look at it, I'm only 9!");
        }

        public override void hPlant_Look()
        {
            cFadi.Say("A plant, which sometimes doubles as a urinal for the tavern patrons...");
        }

        public override void hPlant_UseInv()
        {
            if (cFadi.ActiveInventory == iCola)
            {
                cFadi.Say("It'd probably be interesting to water a plant with Holy Cola... but I better save this inventory item for the puzzle that actually requires it.");
                cFadi.Say("You players get so cranky about walking deads these days.");
            }
            else if (cFadi.ActiveInventory == iDagger)
            {
                cFadi.Say("I'm not putting it back. The plant wasn't using it anyway.");
            }
            else 
                cFadi.Say("The plant doesn't want that. He told me. In secret plant language.");
        }

        public override void hToadstool_Look()
        {
            cFadi.Say("Those toads! Always leaving their furn...");
            Wait(20);
            cFadi.Say("No, sorry, I can't say it. It is simply going too far.");
        }

        public override void hToadstool_Interact()
        {
            cFadi.Say("What am I, a kleptomaniac? No, I'll leave that there.");
        }

        public override void hToadstool_UseInv()
        {
            cFadi.Say("I've got better things to do than trying to use my inventory items on everything.");
            cFadi.Say("Apparently, however, you do not.");
        }

        public override void hBarrel_Interact()
        {
            cFadi.Say("Using the barrel isn't going to help.");
            cBarrel.Say("Oh, but I know the solution to the game!");
            cFadi.Say("Quiet, you!");
        }

        public override void hBarrel_Look()
        {
            cFadi.Say("It is just a barrel. There's nothing special about it.");
            cBarrel.Say("Actually, I'm a magical talking barrel.");
            cFadi.Say("No you're not. Now be quiet.");
        }

        public override void hBarrel_UseInv()
        {
            cFadi.Say("That won't do anyth-");
            if (cFadi.ActiveInventory == iChip)
            {
                cBarrel.Say("Oh, is that a chip!? I've always wanted one of those! I'll give you a sword in exchange for it!");
            }
            else if (cFadi.ActiveInventory == iDagger)
            {
                cBarrel.Say("Oh, a dagger! Wow, I'd love a dagger! I'll teach you a spell if you give me that dagger!");
            }
            else if (cFadi.ActiveInventory == iCola)
            {
                cBarrel.Say("Holy Cola! My favourite drink! If you let me have that, I'll tell you where can can find an ancient treasure!");
            }
            else if (cFadi.ActiveInventory == iFlag)
            {
                cBarrel.Say("Wow, what a great flag! I'll trade you a suit of magical armour for that flag!");
            }
            else if (cFadi.ActiveInventory == iHalberd)
            {
                cBarrel.Say("What a neat halberd! I'll give you a giant ruby if you let me have that!");
            }
            else if (cFadi.ActiveInventory == iSpirit)
            {
                cBarrel.Say("Awesome, Spirit of Hartshorn! Want to swap it for an amulet of strength?");
            }
            else if (cFadi.ActiveInventory == iSalt)
            {
                cBarrel.Say("I need some salt! Hey, I'll let you have my boots of speed if you give me that!");
            }
            else if (cFadi.ActiveInventory == iNote)
            {
                cBarrel.Say("Oh, that's the note I've been waiting for! Let me have it and I will grant you an audience with the king!");
            }
            else if (cFadi.ActiveInventory == iDollar)
            {
                cBarrel.Say("You have a dollar? I'll give you a magical rope for just one dollar!");
            }
            else if (cFadi.ActiveInventory == iPennies)
            {
                cBarrel.Say("Three pennies? I'll swap you seven magic beans if you give me those!");
            }
            cFadi.Say("No. Stop talking.");
        }

        public override void hSign_Interact()
        {
            cFadi.Say("It is far too high for me to reach.");
        }

        public override void hSign_Look()
        {
            cFadi.Say("The sign to Auntie Sylvia's tavern. So obvious that even the blind drunk can understand what it means.");
        }

        public override void hSign_UseInv()
        {
            cFadi.Say("Of course! That's the secret hidden puzzle! How did you know?");
            Wait(40);
            cFadi.Say("Actually, I lied. Sorry.");
        }

        public override void hLamp_Interact()
        {
            cFadi.Say("This isn't Shifter's Box! I bet you wish it was...");
        }

        public override void hLamp_Look()
        {
            cFadi.Say("It's a street lamp. Seems a bit crooked, but still does the job.");
        }

        public override void hLamp_UseInv()
        {
            cFadi.Say("That won't turn the lamp on. And I can see fine as it is, anyway.");
        }

        public override void hSigns_Interact()
        {
            cFadi.Say("They just tell me what street I am on, and I know this part of the city well enough anyway.");
        }

        public override void hSigns_Look()
        {
            cFadi.Say("These signs tell the street names. So you don't get lost.");
            cFadi.Say("Even though it's only a one room game...");
            Wait(20);
            cFadi.Say("Seems a bit pointless, actually, now that I think about it.");
        }

        public override void hSigns_UseInv()
        {
            cFadi.Say("I'm trying to think of a really good reason to not do that.");
            Wait(20);
            cFadi.Say("I can't actually think of a reason to not do it, but I'm still not doing it.");
        }

        public override void hBigsign_Interact()
        {
            cFadi.Say("It is for reading, not using.");
        }

        public override void hBigsign_Look()
        {
            cFadi.Say("It says 'Pennies'. I'm sure I didn't actually need to tell you that.");
        }

        public override void hBigsign_UseInv()
        {
            cFadi.Say("I bet Ben doesn't realize that the standard of puzzles in his games are so low that you're actually willing to try that.");
        }

        public override void hTree_Interact()
        {
            cFadi.Say("I'd like to climb the tree, but I'd better help Auntie Sylvia first.");
        }

        public override void hTree_Look()
        {
            cFadi.Say("A big, leafy tree. All the best games have them.");
        }

        public override void hTree_UseInv()
        {
            cFadi.Say("Maybe in the sequel.");
        }

        public override void hOrange_Interact()
        {
            cFadi.Say("I can't reach it from here. Or anywhere else in the game, in case you were wondering. And even if I could, I don't really need any green paper right now.");
        }

        public override void hOrange_Look()
        {
            cFadi.Say("It's an orange orange on the tree. And yes, I meant to say orange twice, Leon.");
        }

        public override void hOrange_UseInv()
        {
            cFadi.Say("I'm not giving that to the orange.");
        }

        public override void oDress_Interact()
        {
            cFadi.Say("I went through enough trouble to get her dressed. I don't want to fiddle with it any more.");
        }

        public override void oDress_Look()
        {
            cFadi.Say("It's a flag - which is now acting as a dress for the statue lady.");
        }

        public override void oDress_UseInv()
        {
            cFadi.Say("The dress is best left as it is.");
        }

        public override void oChip_Interact()
        {
            cFadi.Say("No, Mr Pigeon is eating that.");
        }

        public override void oChip_Look()
        {
            cFadi.Say("I have donated my chip to Mr Pigeon.");
        }

        public override void oChip_UseInv()
        {
            cFadi.Say("She put salt on the chip - what more could it need?");
        }

        public override void oSalt_UseInv()
        {
            cFadi.Say("I don't want to make that salty.");
        }

        public override void oSoda_Look()
        {
            cFadi.Say("It is a bottle of Holy Cola, from the vending machine.");
        }

        public override void oSoda_UseInv()
        {
            cFadi.Say("That doesn't mix well with soda.");
        }

        public override void oVender_Interact()
        {
            cFadi.Say("I have to put some money in it in order to get a soda.");
        }

        public override void oVender_Look()
        {
            cFadi.Say("It's a Holy Cola vending machine, sent from on high. Righteous anachronism, God!");
        }

        public override void oHalberd_Look()
        {
            cFadi.Say("The guard's mighty polearm! He's left it resting against the building... careless move in an adventure game...");
        }

        public override void oHalberd_UseInv()
        {
            cFadi.Say("This is no fitting activity for me to undertake with this pike!");
        }

        public override void oFlag_Look()
        {
            cFadi.Say("I don't know what the flag is meant to represent, but I like the colour.");
        }

    }

    #region Globally Exposed Items

    public partial class GlobalBase
    {

    }

    #endregion

}

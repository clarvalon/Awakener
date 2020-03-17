// Room_GLOBAL - Type "override" followed by space to see list of C# methods to implement
using static Awakener.GlobalBase;
using static Awakener.Room_GLOBAL;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using MessagePack;
using Clarvalon.XAGE.Global;

namespace Awakener
{
    public partial class Room_GLOBAL
    {
        // Fields
        public int interfaceEnabledInLastFrame;
        public bool fullScreen;

        // Methods
        public override void game_start()
        {
            Game.SpeechFont = eFontFont3;
            game.dialog_options_y = 5;
            game.dialog_options_x = 5;
            gHotspot.Clickable = false;
            gMenu.Visible = false;
        }

        public void UpdateHotSpotAndInventory()
        {
            if (IsInterfaceEnabled() == 1)
            {
                if (interfaceEnabledInLastFrame == 0)
                {
                    UpdateHotSpotLabel();
                    gHotspot.Visible = true;
                }
                TransitionInventoryVisible();
            }
            else 
            {
                if (interfaceEnabledInLastFrame == 1)
                {
                    gHotspot.Visible = false;
                }
                TransitionInventoryInvisible();
            }
            interfaceEnabledInLastFrame = IsInterfaceEnabled();
        }

        public override void repeatedly_execute()
        {
            if (Mouse.Mode != eModeInteract && Mouse.Mode != eModeUseinv)
            {
                Mouse.Mode = eModeInteract;
            }
            InventoryGraphicSelect();
            UpdateHotSpotLabel();
        }

        public override void repeatedly_execute_always()
        {
            UpdateHotSpotAndInventory();
        }

        public override void on_key_press(eKeyCode keycode)
        {
            if (IsGamePaused() == 0)
                keycode = 0;
            if (keycode == eKeyCtrlQ)
                QuitGame(1);
            if (keycode == eKeyF9)
                RestartGame();
            if (keycode == eKeyF12)
                SaveScreenShot("scrnshot.pcx");
            if (keycode == eKeyCtrlS)
                Debug(0,0);
            if (keycode == eKeyCtrlV)
                Debug(1,0);
            if (keycode == eKeyCtrlA)
                Debug(2,0);
            if (keycode == eKeyCtrlX)
                Debug(3,0);
        }

        public override void on_mouse_click(MouseButton button)
        {
            if (button == eMouseLeftInv)
            {
                if (player.ActiveInventory != InventoryItem.GetAtScreenXY(mouse.x, mouse.y))
                {
                    SelectInventory(InventoryItem.GetAtScreenXY(mouse.x, mouse.y));
                }
                else if (player.ActiveInventory == InventoryItem.GetAtScreenXY(mouse.x, mouse.y))
                {
                    DeSelectInventory();
                }
            }
            else if (button == eMouseLeft)
            {
                if (GetLocationType(mouse.x, mouse.y) != eLocationNothing)
                {
                    ProcessClick(mouse.x,mouse.y, Mouse.Mode);
                }
                else 
                    ProcessClick(mouse.x, mouse.y, eModeWalkto);
            }
            else if (button == eMouseRight)
            {
                DeSelectInventory();
            }
        }

        public void interface_click(int interfaceVar, int button)
        {
        }

        public override void cGuard_Interact()
        {
            if (ObjectsInRoom[0].Visible == true)
            {
                guardtalking = true;
                cFadi.Walk(528, 212, eBlock);
                Wait(10);
                cFadi.FaceCharacter(cGuard);
                cGuard.Loop = 0;
                cGuard.Frame = 0;
                dGuard.Start();
            }
            else 
                cFadi.Say("Not after I took his halberd, I don't want to alert him.");
        }

        public void dialog_request(int req)
        {
            if (req == 1)
            {
                guardtalking = false;
                SetTimer(1, 80);
            }
            else if (req == 2)
            {
                cSnacks.Animate(1, 3, eOnce, eBlock);
            }
            else if (req == 3)
            {
                girltalking = false;
                SetTimer(2, 80);
            }
            else if (req == 4)
            {
                clotheswanted = true;
            }
            else if (req == 5)
            {
                assassintalk = false;
                SetTimer(3, 480);
            }
            else if (req == 6)
            {
                saltychip = true;
            }
            else if (req == 7)
            {
                guardloser = true;
            }
            else if (req == 8)
            {
                if (Game.DoOnceOnly("password ask"))
                {
                    dSnacks.SetOptionState(4, eOptionOff);
                    dSnacks.SetOptionState(5, eOptionOn);
                    dChanter.SetOptionState(3, eOptionOff);
                    dChanter.SetOptionState(4, eOptionOn);
                }
            }
        }

        public override void cAssassin_Look()
        {
            if (cAssassin.Loop != 5)
            {
                assassintalk = true;
                cFadi.Say("Wow, a real assassin! I bet she has five knives hidden in her sleeves!");
                cAssassin.Say("Actually, it's more like seven, kid.");
                cFadi.Say("Wow! Seven knives!");
                assassintalk = false;
                SetTimer(3, 480);
            }
            else 
                cFadi.Say("She seems to be busy reading that note I gave her.");
        }

        public override void cAssassin_Interact()
        {
            if (assassinmessage != 2)
            {
                assassintalk = true;
                cFadi.Walk(232, 197, eBlock);
                cFadi.FaceCharacter(cAssassin, eBlock);
                assassinmessage = 1;
                dAssassin.Start();
            }
            else if (assassinmessage == 2)
            {
                cFadi.Say("Nope, she's got her message. I'll leave her alone.");
            }
        }

        public override void cMerchant_Look()
        {
            cFadi.Say("He's a Chanter. They're like a cult of merchants - and they only seem to sell things nobody will buy.");
        }

        public override void cMerchant_Interact()
        {
            cFadi.Walk(491, 205, eBlock);
            cFadi.FaceCharacter(cMerchant, eBlock);
            dChanter.Start();
        }

        public override void cSnacks_Interact()
        {
            if (chipsalted == false)
            {
                cFadi.Walk(590, 227, eBlock);
                Wait(10);
                cFadi.FaceCharacter(cSnacks, eBlock);
                girltalking = true;
                dSnacks.Start();
            }
            else 
                cFadi.Say("I've already taken one of her snacks. I'll leave her in peace.");
        }

        public override void cSnacks_UseInv()
        {
            if (cFadi.ActiveInventory == iSalt && saltychip == true)
            {
                girltalking = true;
                cFadi.Walk(588, 226, eBlock);
                Wait(10);
                cFadi.FaceCharacter(cSnacks, eBlock);
                Wait(10);
                cFadi.Say("I brought you some salt for your chips.");
                cFadi.Animate(7, 3, eOnce, eBlock);
                cSnacks.Say("Thanks.");
                cSnacks.Animate(3, 3, eOnce, eBlock);
                cFadi.Animate(8, 3, eOnce, eBlock);
                cFadi.Loop = 2;
                cFadi.Frame = 0;
                cSnacks.Animate(2, 3, eOnce, eBlock);
                cSnacks.Animate(1, 3, eOnce, eBlock);
                Wait(40);
                cSnacks.Say("Hungry?");
                cSnacks.Animate(1, 3, eOnce, eBlock);
                cFadi.Say("Are you offering me some of your chips?");
                cSnacks.Say("One.");
                cSnacks.Animate(1, 3, eOnce, eBlock);
                cFadi.Walk(597, 226, eBlock);
                cFadi.Animate(9, 3, eOnce, eBlock);
                cFadi.Loop = 2;
                cFadi.Frame = 0;
                cFadi.Say("Thanks, I guess.");
                cSnacks.Say("Enjoy.");
                cSnacks.Animate(1, 3, eOnce, eNoBlock);
                cFadi.AddInventory(iChip);
                cFadi.LoseInventory(iSalt);
                girltalking = false;
                SetTimer(2, 80);
                chipsalted = true;
            }
            else if (cFadi.ActiveInventory == iSalt && saltychip == false)
            {
                cFadi.Say("It's probably best to make sure she wants salt before just giving it to her...");
            }
            else 
                cFadi.Say("I am not giving that to her.");
        }

        public override void cPigeon_Interact()
        {
            if (pigeonfed == false)
            {
                cFadi.Say("I can't reach the pigeon while he is up there.");
            }
            else if (pigeonfed == true && Game.DoOnceOnly("noteget"))
            {
                cFadi.Walk(440, 195, eBlock, eAnywhere);
                cFadi.FaceCharacter(cPigeon);
                cFadi.Say("Hey, there's a small note tied to this pigeon's leg! How exciting!");
                cFadi.Animate(15, 3, eOnce, eBlock);
                cFadi.AddInventory(iNote);
                cFadi.Walk(438, 210, eBlock, eAnywhere);
            }
            else 
                cFadi.Say("I'll leave him to eat his chip in peace.");
        }

        public override void cPigeon_UseInv()
        {
            if (cFadi.ActiveInventory == iChip)
            {
                cFadi.Walk(440, 195, eBlock, eAnywhere);
                cFadi.FaceCharacter(cPigeon);
                cFadi.Say("Here, Mr Pigeon, come and eat some food!");
                cFadi.Animate(15, 3, eOnce, eBlock);
                ObjectsInRoom[5].Visible = true;
                Wait(20);
                cPigeon.ChangeView(5);
                cPigeon.Animate(0, 2, eRepeat, eNoBlock);
                cPigeon.Move(435, 156, eBlock, eAnywhere);
                cPigeon.ChangeView(4);
                cPigeon.Animate(0, 3, eRepeat, eNoBlock);
                Wait(10);
                cFadi.Walk(438, 210, eBlock, eAnywhere);
                cFadi.Say("He seems to like it.");
                cFadi.LoseInventory(iChip);
                pigeonfed = true;
                Wait(20);
                cStatue.Say("Finally, I am free of that cursed bird!");
                Wait(20);
                cFadi.Say("...did that statue just speak?");
            }
            else 
                cFadi.Say("I don't really think he wants that...");
        }

        public override void cAssassin_UseInv()
        {
            if (cFadi.ActiveInventory == iNote && assassinmessage == 1)
            {
                assassinmessage = 2;
                assassintalk = true;
                cFadi.Walk(220, 194, eBlock, eAnywhere);
                cFadi.FaceCharacter(cAssassin, eBlock);
                Wait(10);
                cFadi.Say("Is this the message you're waiting for?");
                cFadi.Animate(16, 3, eOnce, eBlock);
                cAssassin.Say("Finally. Thanks kid, here, take this.");
                cAssassin.Animate(2, 3, eOnce, eBlock);
                cFadi.Animate(17, 3, eOnce, eBlock);
                cAssassin.Animate(3, 3, eOnce, eBlock);
                cAssassin.Animate(4, 3, eOnce, eBlock);
                cFadi.Baseline = 206;
                cFadi.Animate(18, 3, eOnce, eBlock);
                Wait(10);
                cFadi.Baseline = 0;
                cFadi.Walk(220, 206, eBlock, eAnywhere);
                cFadi.Say("A dollar!? I'm rich!");
                cAssassin.Animate(5, 3, eOnce, eBlock);
                cFadi.LoseInventory(iNote);
                cFadi.AddInventory(iDollar);
            }
            else if (cFadi.ActiveInventory == iNote && assassinmessage == 0)
            {
                cFadi.Say("I'm not giving this note away until I know for sure that who it is written for.");
            }
            else if (cFadi.ActiveInventory == iDagger)
            {
                cFadi.Say("I am quite sure she has enough daggers of her own, really.");
            }
            else if (cFadi.ActiveInventory == iHalberd)
            {
                cFadi.Say("Assassins don't really use polearms, as a rule. I'll keep it for myself.");
            }
            else 
                cFadi.Say("I'm not giving that to the assassin.");
        }

        public override void cMerchant_UseInv()
        {
            if (cFadi.ActiveInventory == iDollar)
            {
                cFadi.Walk(491, 205, eBlock);
                cFadi.FaceCharacter(cMerchant, eBlock);
                Wait(10);
                cFadi.Say("Here is a dollar. May I please purchase some pennies?");
                cFadi.Animate(23, 3, eOnce, eBlock);
                cMerchant.Say("Pennies for sale, and here's three for you! Pennies for sale, enjoy them, please do!");
                cMerchant.Animate(0, 3, eOnce, eBlock);
                cFadi.Animate(23, 3, eOnce, eBlock, eBackwards);
                cFadi.Loop = 3;
                cFadi.Frame = 0;
                cFadi.LoseInventory(iDollar);
                cFadi.AddInventory(iPennies);
            }
            else if (cFadi.ActiveInventory == iPennies)
            {
                cFadi.Say("He only sells pennies... so that seems a little pointless, really.");
            }
            else 
                cFadi.Say("That isn't accepted as currency.");
        }

        public override void cCleric_Interact()
        {
            if (ObjectsInRoom[4].Visible == false)
            {
                cFadi.Walk(325, 204, eBlock);
                cFadi.FaceCharacter(cCleric, eBlock);
                dCleric.Start();
            }
            else 
                cFadi.Say("I don't think he's going to be able to help me with anything right now...");
        }

        public override void cCleric_UseInv()
        {
            if (cFadi.ActiveInventory == iDagger)
            {
                cFadi.Walk(298, 195, eBlock);
                cFadi.FaceCharacter(cCleric, eBlock);
                Wait(10);
                cFadi.Say("Is this dagger an unholy relic?");
                cFadi.Animate(14, 3, eOnce, eBlock);
                cCleric.Say("By the lords above! It is! I shall call upon holy might to smite it, and cleanse this place!");
                cCleric.Animate(4, 3, eOnce, eBlock);
                cFadi.Animate(8, 3, eOnce, eBlock);
                cCleric.Animate(6, 3, eOnce, eBlock);
                cFadi.LoseInventory(iDagger);
                Wait(10);
                cCleric.ChangeView(16);
                cCleric.Walk(296, 226, eBlock, eAnywhere);
                Wait(10);
                cCleric.ChangeView(10);
                cCleric.SpeechView = 17;
                cFadi.FaceCharacter(cCleric, eBlock);
                cCleric.Animate(5, 3, eOnce, eBlock);
                cCleric.ChangeView(17);
                Wait(10);
                cCleric.Say("O my Lord! I humbly request that you crush the impure item that I hold here in my hands!");
                PlaySound(4);
                Wait(80);
                ObjectsInRoom[4].Y = 0;
                ObjectsInRoom[4].Visible = true;
                ObjectsInRoom[4].Graphic = 352;
                ObjectsInRoom[4].Move(268, 235, 25, eBlock, eAnywhere);
                ObjectsInRoom[4].Graphic = 120;
                cCleric.x = 302;
                cCleric.Animate(1, 3, eOnce, eBlock);
                Wait(40);
                cCleric.Animate(1, 3, eOnce, eBlock);
                cFadi.Say("He probably should have not held onto the dagger when he was saying that...");
                Wait(40);
                cDeity.Transparency = 100;
                cDeity.ChangeRoom(1, 325, 10);
                cDeity.Say("NOR SHOULD HE HAVE INTERRUPTED A DEITY DURING HIS LUNCH BREAK.");
                cDeity.ChangeRoom(0);
                Wait(40);
                cFadi.Say("Err, yes, that too, Mr God, sir.");
                RemoveWalkableArea(2);
            }
            else 
                cFadi.Say("I don't think he'd be very interested in having that.");
        }

        public override void cImp_Interact()
        {
            cFadi.Walk(303, 209, eBlock);
            cFadi.FaceCharacter(cImp, eBlock);
            if (impmana == false)
            {
                dImp.Start();
            }
            else 
            {
                cFadi.Say("Hello there, Mr Imp sir.");
                cImp.Say("Yes, yes, yes, I'm ready to grant your wish. Just hand me the flag!");
            }
        }

        public override void cImp_UseInv()
        {
            if (cFadi.ActiveInventory == iFlag)
            {
                if (impmana == false)
                {
                    cImp.Say("Yes, yes, yes, I'd love to grant your wish, but I'm all out of mana.");
                }
                else 
                {
                    cFadi.Walk(303, 209, eBlock);
                    cFadi.FaceCharacter(cImp, eBlock);
                    cFadi.Say("So you can use this flag as clothing for the statue?");
                    cImp.Say("Yes, yes, yes, one moment please...");
                    Wait(40);
                    cFadi.LoseInventory(iFlag);
                    ObjectsInRoom[9].Transparency = 100;
                    ObjectsInRoom[9].Visible = true;
                    PlaySound(13);
                    int trans2 = ObjectsInRoom[9].Transparency;
                    while (trans2 > 0)
                    {
                        trans2 = trans2 - 5;
                        ObjectsInRoom[9].Transparency = trans2;
                        Wait(1);
                    }
                    ObjectsInRoom[1].Transparency = 100;
                    ObjectsInRoom[1].Visible = true;
                    int trans3 = ObjectsInRoom[1].Transparency;
                    while (trans3 > 0)
                    {
                        trans3 = trans3 - 5;
                        ObjectsInRoom[1].Transparency = trans3;
                        Wait(1);
                    }
                    while (trans2 < 100)
                    {
                        trans2 = trans2 + 5;
                        ObjectsInRoom[9].Transparency = trans2;
                        Wait(1);
                    }
                    ObjectsInRoom[9].Visible = false;
                    Wait(10);
                    Wait(20);
                    cFadi.Say("You did it! Thank you, Mr Imp sir!");
                    cImp.Say("Yes, yes, yes, it was my pleasure. Now excuse me, I must be off.");
                    Wait(10);
                    ObjectsInRoom[8].Visible = true;
                    PlaySound(14);
                    int trans = ObjectsInRoom[8].Transparency;
                    while (trans > 0)
                    {
                        trans = trans - 5;
                        ObjectsInRoom[8].Transparency = trans;
                        Wait(1);
                    }
                    int trans4 = cImp.Transparency;
                    while (trans4 < 100)
                    {
                        trans4 = trans4 + 5;
                        cImp.Transparency = trans4;
                        Wait(1);
                    }
                    cImp.ChangeRoom(0);
                    while (trans < 100)
                    {
                        trans = trans + 5;
                        ObjectsInRoom[8].Transparency = trans;
                        Wait(1);
                    }
                    ObjectsInRoom[8].Visible = false;
                    Wait(10);
                    cStatue.Say("Well, thank you for your help. Now I don't have to put up with the curse of strangers gawking at my bits.");
                    password = true;
                    cStatue.Say("Poron's password is 'Taste the Day', by the way.");
                    cFadi.Say("Thanks, Miss Statue! I'll go and see him now.");
                }
            }
            else if (cFadi.ActiveInventory == iCola)
            {
                cFadi.Walk(303, 209, eBlock);
                cFadi.FaceCharacter(cImp, eBlock);
                cFadi.Say("Will this soda help restore your mana?");
                cImp.Say("Yes, yes, yes, Holy Cola, how delicious and refreshing!");
                ObjectsInRoom[10].Visible = true;
                ObjectsInRoom[10].Move(296, 111, 5, eBlock, eAnywhere);
                ObjectsInRoom[10].Visible = false;
                cImp.Animate(4, 3, eOnce, eBlock);
                Wait(10);
                cFadi.LoseInventory(iCola);
                cImp.Say("Yes, yes, yes, I'm all ready to grant your wish now! Pass me the flag when you please!");
                impmana = true;
            }
            else 
                cFadi.Say("I don't want to give that to the imp.");
        }

        public void iSalt_Look()
        {
            cFadi.Say("It is salt. Useful for keeping nasty spirits away, mixing alchemical elixirs and sprinkling on food.");
        }

        public void iFlag_Look()
        {
            cFadi.Say("A large, purple flag. Every hero needs his own flag!");
        }

        public void iHalberd_Look()
        {
            cFadi.Say("An enormous pike with a hook at the end. If anyone dares to challenge me to battle, I am ready!");
        }

        public void iChip_Look()
        {
            cFadi.Say("Fried potato such as this is the perfect food for any adventurer!");
        }

        public void iCola_Look()
        {
            cFadi.Say("It's Holy Cola! Carbonated holy water - the soda of choice for the Demon slayer in the know. The label warns that it may contain traces of mana.");
        }

        public void iPennies_Look()
        {
            cFadi.Say("Three pennies - who knows what sort of creature these may have been looted from?");
        }

        public void iDollar_Look()
        {
            cFadi.Say("It's a dollar, given to me by an assassin. I wonder whose blood was spilled to earn this?");
        }

        public void iSpirit_Look()
        {
            cFadi.Say("Spirit of hartshorn. This stuff is strong enough to wake the comatose!");
        }

        public void iDagger_Look()
        {
            cFadi.Say("A sinister looking dagger. I bet it was used in sacrificial rituals at one point!");
        }

        public void iNote_Look()
        {
            cFadi.Say("A small, sealed note from the leg of that pigeon. The seal has a picture of a dagger on it...");
        }

        public override void cAunt_Interact()
        {
            player.Walk(120, 220, eBlock);
            cFadi.FaceCharacter(cAunt, eBlock);
            if (cFadi.HasInventory(iSpirit))
            {
                cFadi.Say("I got the Spirit of Hartshorn, Auntie Sylvia!");
                cAunt.Say("Thank you Fadi. Open the bottle and wave it under this fellow's nose, will you? It should wake him straight up.");
            }
            else 
                dAunt2.Start();
        }

        public override void cSnacks_Look()
        {
            cFadi.Say("She sure seems to be very focused on eating her snacks.");
        }

        public override void cGuard_Look()
        {
            cFadi.Say("He's leaning against Poron's door - on guard duty. He also looks very bored.");
        }

        public override void cGuard_UseInv()
        {
            cFadi.Say("I'm not giving him my things!");
        }

        public override void cPigeon_Look()
        {
            cFadi.Say("It's a pigeon. They're all over the city.");
        }

        public override void cAunt_Look()
        {
            cFadi.Say("It's my Auntie Sylvia. She owns this tavern.");
        }

        public override void cAunt_UseInv()
        {
            cFadi.Say("I'm not showing Auntie Sylvia my things. She always takes the exciting things off me, and says they're too dangerous.");
        }

        public override void cCleric_Look()
        {
            cFadi.Say("It's a cleric. Usually you only see them when you go into a temple...");
        }

        public override void cSleeper_Interact()
        {
            cFadi.FaceCharacter(cSleeper);
            cFadi.Say("Mr sleeping man, sir? Can you hear me?");
            Wait(40);
            cFadi.Say("I guess not.");
        }

        public override void cSleeper_Look()
        {
            cFadi.Say("He's sleeping very soundly. I wonder how long he's been there...");
        }

        public override void cSleeper_UseInv()
        {
            if (cFadi.ActiveInventory == iSpirit)
            {
                cFadi.Walk(127, 206, eBlock);
                cFadi.FaceCharacter(cSleeper);
                Wait(10);
                cFadi.Say("This ought to wake him up...");
                cFadi.Animate(26, 3, eOnce, eBlock);
                Wait(20);
                cFadi.Loop = 1;
                cFadi.Frame = 0;
                cSleeper.ChangeView(24);
                cSleeper.SpeechView = 24;
                Wait(20);
                cSleeper.Say("...gah! I can't believe it happened again!");
                cSleeper.Say("At least I managed to survive. I nearly thought I wasn't going to make it!");
                Wait(40);
                cSleeper.Say("Wait, where is the salt?");
                Wait(20);
                cFadi.Say("Oh, er, um, about that, Mr Sleeping Man, sir...");
                cSleeper.Say("What? Did you see some-");
                cSleeper.Say("NO! Not already! I can't believe he found me!");
                cSleeper.Say("I have to get out of here!");
                ObjectsInRoom[8].X = 80;
                ObjectsInRoom[8].Y = 220;
                ObjectsInRoom[8].Transparency = 100;
                ObjectsInRoom[8].Visible = true;
                PlaySound(16);
                PlayMusic(2);
                int trans = ObjectsInRoom[8].Transparency;
                while (trans > 0)
                {
                    trans = trans - 5;
                    ObjectsInRoom[8].Transparency = trans;
                    Wait(1);
                }
                int trans4 = cSleeper.Transparency;
                while (trans4 < 100)
                {
                    trans4 = trans4 + 5;
                    cSleeper.Transparency = trans4;
                    Wait(1);
                }
                cSleeper.ChangeRoom(0);
                while (trans < 100)
                {
                    trans = trans + 5;
                    ObjectsInRoom[8].Transparency = trans;
                    Wait(1);
                }
                Wait(80);
                cFadi.Say("What was all that about?");
                cAunt.Say("I don't-");
                cWraith.ChangeRoom(1, 330, 218);
                cWraith.Walk(171, 218, eBlock, eAnywhere);
                Wait(20);
                cFadi.FaceCharacter(cWraith);
                Wait(40);
                cWraith.Say("So, he was awakened in time. How very unfortunate.");
                Wait(20);
                cWraith.Say("I wonder if he has the good sense to keep that salt with him wherever he ended up?");
                Wait(40);
                cWraith.Say("I am going to make sure he needs it...");
                Wait(10);
                cWraith.Walk(330, 218, eBlock, eAnywhere);
                Wait(20);
                cFadi.FaceCharacter(cAunt);
                Wait(20);
                cFadi.Say("Who was he, Auntie?");
                cAunt.Say("I don't know - and I don't think I want to Fadi.");
                cAunt.Say("Come now, you've had plenty of adventure for the day. Come inside and help me open up the tavern.");
                cFadi.Say("Can't I just follow him a little bit, to see what he does?");
                cAunt.Say("No, Fadi, definitely not. He wasn't a good person, and following him is going to get you in trouble.");
                cAunt.Say("One day you'll be ready to take on such an adventure, but not yet, Fadi dear.");
                cFadi.Say("Yes, Auntie Syvlia.");
                cAunt.Say("That's a good boy. Come on, I'll get you some lunch before we open.");
                Wait(20);
                FadeOut(2);
                cFadi.LoseInventory(iSpirit);
                cFadi.ChangeRoom(3);
            }
            else 
                cFadi.Say("That won't wake him up!");
        }

        public override void cImp_Look()
        {
            cFadi.Say("It's an imp! I've only ever read about them in books!");
        }

        public void UpdateFullScreenDisplay()
        {
            String fs = "Yes";
            if (!fullScreen)
                fs = "No";
            lblFullScreen.Text = fs;
        }

        public void UpdateDisplayStyleDisplay()
        {
            string str = string.Empty;            switch (displayStyle)            {                case DisplayStyle.PixelPerfect:                  str = "Perfect";                  break;                case DisplayStyle.MaintainAspectRatio:                  str = "Borders";                  break;                case DisplayStyle.StretchToFit:                  str = "Stretch";                  break;            }            lblDisplayMode.Text = str;
        }

        public override void ButtonPause_OnClick(GUIControl control, MouseButton button)
        {
            displayStyle = GetDisplayStyle();            fullScreen = GetFullScreen();
            UpdateDisplayStyleDisplay();
            UpdateFullScreenDisplay();
            gMenu.Visible = true;
        }

        public void gGuibar_OnClick(GUI theGui, MouseButton button)
        {
        }

        public override void btnLoad_OnClick(GUIControl control, MouseButton button)
        {
            if (Game.GetSaveSlotDescription(1) != null)
            {
                RestoreGameSlot(1);
                gMenu.Visible = false;
            }
        }

        public override void btnSave_OnClick(GUIControl control, MouseButton button)
        {
            gMenu.Visible = false;
            SaveGameSlot(1, "1");
            cNar.Say("Game Saved");
        }

        public override void btnNewGame_OnClick(GUIControl control, MouseButton button)
        {
            DeleteSaveSlot(1);
            RestartGame();
        }

        public override void btnDisplayMode_OnClick(GUIControl control, MouseButton button)
        {
            switch (displayStyle)            {                case DisplayStyle.PixelPerfect:                    displayStyle = DisplayStyle.MaintainAspectRatio;                    break;                case DisplayStyle.MaintainAspectRatio:                    displayStyle = DisplayStyle.StretchToFit;                    break;                case DisplayStyle.StretchToFit:                    displayStyle = DisplayStyle.PixelPerfect;                    break;            }            UpdateDisplayStyleDisplay();
        }

        public override void btnToggleFullScreen_OnClick(GUIControl control, MouseButton button)
        {
            
#if  ANDROID

            
#else 

            fullScreen = !fullScreen;
            UpdateFullScreenDisplay();
            
#endif

        }

        public override void btnAccept_OnClick(GUIControl control, MouseButton button)
        {
            SetDisplayStyle(displayStyle);            SetFullScreen(fullScreen);            ApplyGraphicalChanges();
            gMenu.Visible = false;
        }

        public override void btnCancel_OnClick(GUIControl control, MouseButton button)
        {
            gMenu.Visible = false;
        }

        public override void btnQuit_OnClick(GUIControl control, MouseButton button)
        {
            QuitGame(0);
        }

DisplayStyle displayStyle;        // Expose Global Variables
        public bool guardtalking = false;
        public bool girltalking = false;
        public bool chipsalted = false;
        public bool pigeonfed = false;
        public int assassinmessage = 0;
        public bool clotheswanted = false;
        public bool impmana = false;
        public bool password = false;
        public bool assassintalk = false;
        public bool saltychip = false;
        public bool guardloser = false;

    }

    #region Globally Exposed Items

    public partial class GlobalBase
    {
        // Expose Global Variables
        public static bool guardtalking { get { return GLOBAL.guardtalking; } set { GLOBAL.guardtalking = value; } }
        public static bool girltalking { get { return GLOBAL.girltalking; } set { GLOBAL.girltalking = value; } }
        public static bool chipsalted { get { return GLOBAL.chipsalted; } set { GLOBAL.chipsalted = value; } }
        public static bool pigeonfed { get { return GLOBAL.pigeonfed; } set { GLOBAL.pigeonfed = value; } }
        public static int assassinmessage { get { return GLOBAL.assassinmessage; } set { GLOBAL.assassinmessage = value; } }
        public static bool clotheswanted { get { return GLOBAL.clotheswanted; } set { GLOBAL.clotheswanted = value; } }
        public static bool impmana { get { return GLOBAL.impmana; } set { GLOBAL.impmana = value; } }
        public static bool password { get { return GLOBAL.password; } set { GLOBAL.password = value; } }
        public static bool assassintalk { get { return GLOBAL.assassintalk; } set { GLOBAL.assassintalk = value; } }
        public static bool saltychip { get { return GLOBAL.saltychip; } set { GLOBAL.saltychip = value; } }
        public static bool guardloser { get { return GLOBAL.guardloser; } set { GLOBAL.guardloser = value; } }


    }

    #endregion

}

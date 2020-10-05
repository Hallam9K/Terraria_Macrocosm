using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Macrocosm.Items.Currency;
using Macrocosm.Items.Weapons;
using Macrocosm.Items.Materials;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Macrocosm.NPCs.Friendly.TownNPCs
{
    [AutoloadHead]
    public class Astronaut : ModNPC
    {
        public override string Texture => "Macrocosm/NPCs/Friendly/TownNPCs/Astronaut";

        public override bool Autoload(ref string name)
        {
            name = "MoonChampion";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 26;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 5;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 60;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            NPCID.Sets.HatOffsetY[npc.type] = 0;
        }
        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 1000;
            npc.defense = 100;
            npc.lifeMax = 5000;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.knockBackResist = 1f;
            animationType = NPCID.Guide;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            Player player = Main.player[Main.myPlayer];
            if (player.HasItem(ModContent.ItemType<UnuCredit>()))
            {
                return true;
            }
            return false;
        }

        public override string TownNPCName()
        {
            switch(WorldGen.genRand.Next(4))
            {
                case 0:
                    return "Mann";
                case 1:
                    return "Doyle";
                case 2:
                    return "Romilly";
                default:
                    return "Cooper";
            }
        }

        public override string GetChat()
        {
            Player player = Main.player[Main.myPlayer];
            switch (Main.rand.Next(4))
            {
                case 0:
                    if (player.CountItem(ModContent.ItemType<UnuCredit>()) > 1)
                    {
                        return $"I see you have {player.CountItem(ModContent.ItemType<UnuCredit>())} Moon Monies. why don't you try spending them here?";
                    }
                    else if (player.CountItem(ModContent.ItemType<UnuCredit>()) == 1)
                    {
                        return $"I see you have {player.CountItem(ModContent.ItemType<UnuCredit>())} Moon Money. why don't you try spending them here?";
                    }
                    else
                    {
                        return "I see you lack Moon Money, try killing lunar enemies.";
                    }
                case 1:
                    return "Why do you deny the fact that I am the coolest man alive?";
                case 2:
                    return "When humans first went to Mars, I thought \"Dang, Im never gunna do this!\" Yeah, unfortunately, I didn't. I went to Ganymede, baby!";
                default:
                    return "I say space is cool, but at that price, it is also scary.";
            }
        }
        
        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Shop";
            button2 = "Advice";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if(firstButton)
            {
                shop = true;
            }
            else
            {
                switch (Main.rand.Next(3))
                {
                    case 1:
                        Main.npcChatText = "Enemies on the moon planets drop a currency known as Moon Money. You can get them by just... Well... Killing enemies!";
                        break;
                    case 2:
                        Main.npcChatText = "The moon is littered with deadly enemies that can easily overwhelm you if you are not careful. I'd recommend you get prepared"
                            + " as much as possible before taking anything on it.";
                        break;
                    default:
                        Main.npcChatText = "It is crucial to use an astronaut suit on the moon, otherwise you would run out of breath and die faster than you could imagine.";
                        break;
                }
            }
        }
        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<ChandriumBar>());
            shop.item[nextSlot].SetNameOverride("Sorry dude this is just placeholder");
            shop.item[nextSlot].shopCustomPrice = new int?(20);
            shop.item[nextSlot].shopSpecialCurrency = CurrencyManager.UnuCredit;
            nextSlot++;
        }

        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ModContent.ItemType<CosmicDust>());
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 125;
            knockback = 5f;
        }
        public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            item = ModContent.GetTexture("Terraria/Item_" + ItemID.None);
            itemSize = 20;
            scale = 1f;

        }
        public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
        {
            // Idk i dont really have the time rn to implement this, might do it later
        }
        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 1;
            randExtraCooldown = 1;
        }
    }
}
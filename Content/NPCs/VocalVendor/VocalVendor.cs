using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TreeSword.Common.Systems;
using TreeSword.Content.Items.Materials;
using TreeSword.Content.Items.Weapons;

namespace TreeSword.Content.NPCs.VocalVendor
{
	[AutoloadHead]
	public class VocalVendor : ModNPC
	{
		// We keep variant count centralized so dialogue + texture selection can't drift apart.
		private const int VariantCount = 4;

		// Placeholder visuals: use different vanilla town NPC textures per variant.
		// You can swap these out later for your own spritesheets at:
		// Content/NPCs/VocalVendor/VocalVendor_(Miku|Rin|Kaito|Meiko).png
		private static readonly int[] VariantVanillaNpcIds =
		{
			NPCID.Stylist,     // "Miku" placeholder
			NPCID.PartyGirl,   // "Rin" placeholder
			NPCID.Clothier,    // "Kaito" placeholder
			NPCID.Nurse        // "Meiko" placeholder
		};

		public override string Texture
		{
			get
			{
				// Choose a placeholder vanilla NPC sprite based on the rolled variant.
				int variant = PeakingItSystem.VocalVendorVariant % VariantCount;
				int vanillaNpcId = VariantVanillaNpcIds[variant];
				return $"Terraria/Images/NPC_{vanillaNpcId}";
			}
		}

		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Guide];
			NPCID.Sets.ActsLikeTownNPC[Type] = true;
		}

		public override void SetDefaults()
		{
			NPC.width = 18;
			NPC.height = 40;
			NPC.aiStyle = NPCAIStyleID.Passive; // Town NPC
			NPC.friendly = true;
			NPC.townNPC = true;
			NPC.damage = 10;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.5f;
		}

		public override bool CanTownNPCSpawn(int numTownNPCs)
		{
			// Requirement: moves in after first Doppelganger kill.
			// We are not implementing the enemy yet, so this flag is toggled via a chat command for testing.
			return PeakingItSystem.DownedDoppelganger;
		}

		public override List<string> SetNPCNameList()
		{
			// Optional: keep one name, you can expand later per variant.
			return new List<string> { "Vocal Vendor" };
		}

		public override string GetChat()
		{
			int variant = PeakingItSystem.VocalVendorVariant % VariantCount;
			string[] lines = variant switch
			{
				0 => MikuLines,
				1 => RinLines,
				2 => KaitoLines,
				_ => MeikoLines
			};

			return lines[Main.rand.Next(lines.Length)];
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28"); // "Shop"
		}

		public override void OnChatButtonClicked(bool firstButton, ref string shopName)
		{
			if (firstButton)
				shopName = "Shop";
		}

		public override void AddShops()
		{
			var barkblade = new Item(ModContent.ItemType<Barkblade>())
			{
				shopCustomPrice = Item.buyPrice(gold: 15)
			};
			var tutorialSword = new Item(ModContent.ItemType<TutorialSword>())
			{
				shopCustomPrice = Item.buyPrice(silver: 1)
			};
			var shadowEssence = new Item(ModContent.ItemType<ShadowEssence>())
			{
				shopCustomPrice = Item.buyPrice(gold: 1)
			};

			var shop = new NPCShop(Type, "Shop")
				.Add(barkblade)
				.Add(tutorialSword)
				.Add(shadowEssence);

			shop.Register();
		}

		private static readonly string[] MikuLines =
		{
			"01 01 01 01... adventurer detected! Welcome to my shop!",
			"Teal twin-tails, teal deals. Take a look!",
			"Need something sharp? Need something shiny? I’ve got both.",
			"Your rhythm is off—try a new weapon and sync back up.",
			"Keep your inventory organized; it makes the chorus hit harder.",
			"I can’t sing you to victory, but I can sell you the tools."
		};

		private static readonly string[] RinLines =
		{
			"Heya! Don't let my size fool you, these items pack a punch!",
			"Fast beats, fast swings—want something quicker?",
			"If it looks cute and hits hard, it belongs in your hotbar.",
			"Got coins? Great. Got courage? Even better.",
			"I’ve got a feeling you’re the type to spam-click. Respect.",
			"Try something new—worst case, you look cool doing it."
		};

		private static readonly string[] KaitoLines =
		{
			"Feeling cold? My Peak Forge will warm you right up.",
			"Blue tones, cool trades. Take your time.",
			"A steady hand wins fights. A better weapon wins them faster.",
			"If you’re preparing for a tough opponent, start at my shop.",
			"Sometimes the best strategy is simply better gear.",
			"Stay calm. Spend smart. Survive."
		};

		private static readonly string[] MeikoLines =
		{
			"I've got what you need. Don't waste my time.",
			"If you’re buying, talk. If not, move.",
			"Quality costs. Luckily, you look like you can pay.",
			"Don’t blame the weapon if you miss. But do buy a better one.",
			"Get in, gear up, get out. That’s the deal.",
			"Come back with more coins. Or don’t."
		};
	}
}


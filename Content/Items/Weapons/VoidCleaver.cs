using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TreeSword.Content.Items.Weapons
{
	// Not obtainable by the player: this exists primarily so the Doppelganger NPC
	// can reference a concrete item type later (sprite, stats, etc.).
	public class VoidCleaver : ModItem
	{
		// Placeholder texture until you draw the ~40x40 "void-black glow" blade.
		public override string Texture => $"Terraria/Images/Item_{ItemID.NightsEdge}";

		public override void SetDefaults()
		{
			Item.damage = 35;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.rare = ItemRarityID.Purple;

			// No value/recipe: keep it effectively unobtainable.
			Item.value = 0;
		}
	}
}


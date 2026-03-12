using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TreeSword.Content.Items.Materials
{
	// Crafting material dropped by the future Doppelganger enemy and stocked by the Vocal Vendor.
	public class ShadowEssence : ModItem
	{
		// Placeholder texture.
		public override string Texture => $"Terraria/Images/Item_{ItemID.ShadowScale}";

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 9999;
			Item.value = Item.sellPrice(silver: 50);
			Item.rare = ItemRarityID.Blue;
		}
	}
}


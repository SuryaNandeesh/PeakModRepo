using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TreeSword.Content.Projectiles;

namespace TreeSword.Content.Items.Weapons
{
	// Melee sword sold by the Vocal Vendor that fires a leaf projectile on every swing.
	public class Barkblade : ModItem
	{
		// Placeholder texture until you draw Barkblade (custom PNG can be added later).
		public override string Texture => $"Terraria/Images/Item_{ItemID.WoodenSword}";

		public override void SetDefaults()
		{
			Item.damage = 22;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;

			Item.useTime = 20; // average speed
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4; // slight knockback
			Item.value = Item.buyPrice(gold: 15);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

			// Fires a leaf projectile on each swing (shooting-sword pattern).
			Item.shoot = ModContent.ProjectileType<LeafProjectile>();
			Item.shootSpeed = 6f; // slow-moving
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			// Requirement: leaf deals 60% of base damage (22 * 0.6 ≈ 13).
			damage = (int)(Item.damage * 0.6f);
		}
	}
}


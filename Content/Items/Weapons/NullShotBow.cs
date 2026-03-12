using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TreeSword.Content.Projectiles;

namespace TreeSword.Content.Items.Weapons
{
	// Ranged weapon that fires the Null Shot projectile.
	// This gives the "Null Shot" concept a concrete bow the player (or Doppelganger) can use.
	public class NullShotBow : ModItem
	{
		// Placeholder texture until you draw a custom bow sprite.
		public override string Texture => $"Terraria/Images/Item_{ItemID.DemonBow}";

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 20;
			Item.height = 40;
			Item.useTime = 26;
			Item.useAnimation = 26;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 2f;
			Item.value = 0; // not obtainable by default; you can add a recipe or shop entry later.
			Item.rare = ItemRarityID.Blue;

			Item.noMelee = true;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;

			// Fires NullShot directly and does not consume ammo.
			Item.shoot = ModContent.ProjectileType<NullShot>();
			Item.shootSpeed = 10f;
		}
	}
}


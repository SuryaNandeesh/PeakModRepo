using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TreeSword.Content.Projectiles;

namespace TreeSword.Content.Items.Weapons
{
	// Not necessarily sold/craftable yet — this exists so the project has the
	// "Doppelganger items-only" assets ready even without the NPC implemented.
	public class ShadowSling : ModItem
	{
		// Placeholder texture (you'll replace with ~14x14 orb item sprite).
		public override string Texture => $"Terraria/Images/Item_{ItemID.WoodYoyo}";

		public override void SetDefaults()
		{
			Item.damage = 24;
			Item.DamageType = DamageClass.MeleeNoSpeed;
			Item.width = 24;
			Item.height = 24;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 2.5f;
			Item.rare = ItemRarityID.Blue;
			Item.value = 0; // keep unobtainable unless you later add recipe/shop

			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.UseSound = SoundID.Item1;

			Item.shoot = ModContent.ProjectileType<ShadowSlingYoyoProjectile>();
			Item.shootSpeed = 16f;
		}
	}
}


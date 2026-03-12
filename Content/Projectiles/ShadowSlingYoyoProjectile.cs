using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TreeSword.Content.Projectiles
{
	// A yoyo-style projectile used by Shadow Sling.
	public class ShadowSlingYoyoProjectile : ModProjectile
	{
		// Placeholder texture (you'll replace with ~14x14 dark orb).
		public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.WoodYoyo}";

		public override void SetStaticDefaults()
		{
			// These are how Terraria configures yoyo behavior.
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Type] = 8f;
			ProjectileID.Sets.YoyosMaximumRange[Type] = 220f;
			ProjectileID.Sets.YoyosTopSpeed[Type] = 14f;
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = ProjAIStyleID.Yoyo;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.MeleeNoSpeed;
			Projectile.scale = 1f;
		}

		public override void AI()
		{
			// Light shadow trail feel (cheap placeholder using dust).
			if (Main.rand.NextBool(3))
			{
				int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, 0f, 0f, 150, default, 0.9f);
				Main.dust[d].noGravity = true;
				Main.dust[d].velocity *= 0.1f;
			}
		}
	}
}


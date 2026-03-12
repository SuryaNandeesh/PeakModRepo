using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TreeSword.Content.Projectiles
{
	// Shadowy projectile used by the Null Shot bow and, in the future, by the Doppelganger NPC.
	public class NullShot : ModProjectile
	{
		// Placeholder texture until you draw the ~8x8 dark projectile.
		public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.ShadowFlame}";

		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = ProjAIStyleID.Arrow;
			// Friendly by default so the player-facing bow can use it.
			// A future Doppelganger NPC can either reuse this with custom flags
			// or use a separate hostile variant if needed.
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 180;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
		}

		public override void AI()
		{
			if (Main.rand.NextBool(2))
			{
				int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, 0f, 0f, 200, default, 0.8f);
				Main.dust[d].noGravity = true;
				Main.dust[d].velocity *= 0.2f;
			}
		}
	}
}


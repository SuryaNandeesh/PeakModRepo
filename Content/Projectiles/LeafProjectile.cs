using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TreeSword.Content.Projectiles
{
	// Slow-moving rotating leaf used by Barkblade as its ranged component.
	public class LeafProjectile : ModProjectile
	{
		// Placeholder texture until you draw the 10x10 leaf.
		public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Leaf}";

		public override void SetStaticDefaults()
		{
			// Use the same frame count as the vanilla Leaf projectile so we don't draw
			// the entire tall spritesheet at once. Each frame is a single leaf.
			Main.projFrames[Type] = Main.projFrames[ProjectileID.Leaf];
		}

		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 180;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;

			// Treat as melee-adjacent projectile for scaling.
			Projectile.DamageType = DamageClass.Melee;
		}

		public override void AI()
		{
			// Basic animation: cycle through the leaf frames so only one frame (one leaf)
			// is drawn at a time instead of the whole tall texture.
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 6)
			{
				Projectile.frameCounter = 0;
				Projectile.frame++;
				if (Projectile.frame >= Main.projFrames[Type])
				{
					Projectile.frame = 0;
				}
			}

			Projectile.rotation += 0.35f * Projectile.direction;

			// Lightly damp over time to keep it "slow-moving".
			Projectile.velocity *= 0.99f;

			if (Main.rand.NextBool(4))
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Grass, 0f, 0f, 150, default, 0.9f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 0.2f;
			}
		}
	}
}


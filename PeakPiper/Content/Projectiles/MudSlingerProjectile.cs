using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PeakPiper.Content.Projectiles
{
    public class MudSlingerProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {

            ProjectileID.Sets.YoyosLifeTimeMultiplier[Type] = 7f;

            ProjectileID.Sets.YoyosMaximumRange[Type] = 75f;

            ProjectileID.Sets.YoyosTopSpeed[Type] = 6f;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.aiStyle = ProjAIStyleID.Yoyo;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.penetrate = -1;
        }
        public override void PostAI()
        {
            // Necessary for yoyos, doesn't function without.
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheMimic.Content.NPCs
{
    internal class EnemyMimic : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 40;
            NPC.gfxOffY = -(164 - 40) / 2;
            NPC.damage = 50;
            NPC.defense = 5;
            NPC.lifeMax = 50;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.aiStyle = -1;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.friendly = false;
            NPC.value = Item.buyPrice(silver: 5);
            NPC.npcSlots = 1f;
        }

        public override void AI()
        {
            NPC.TargetClosest(true);

            if (NPC.target < 0 || NPC.target >= Main.maxPlayers) return;

            Player player = Main.player[NPC.target];

            float lockRange = 300f;

            // Lock-on system: once in range, stays locked
            if (NPC.ai[2] == 0f && NPC.Distance(player.Center) <= lockRange)
            {
                NPC.ai[2] = 1f;
                NPC.netUpdate = true;
            }

            // Not locked on yet -> idle
            if (NPC.ai[2] == 0f)
            {
                NPC.velocity.X *= 0.9f;
                return;
            }

            // Reset if player dead
            if (!player.active || player.dead)
            {
                NPC.ai[2] = 0f;
                return;
            }

            // Attack cooldown
            NPC.ai[1]--;
            if (NPC.ai[1] < 0f) NPC.ai[1] = 0f;
            bool isAttacking = NPC.ai[1] > 0f;

            if (isAttacking)
            {
                NPC.ai[0]--;
                NPC.velocity.X *= 0.9f;
                NPC.velocity.Y *= 0.9f;

                // Face player during attack
                NPC.direction = Math.Sign(player.Center.X - NPC.Center.X);
                if (NPC.direction == 0) NPC.direction = NPC.oldDirection;
                NPC.spriteDirection = NPC.direction;
            }

            // Horizontal mimic movement (only when not attacking)
            if (!isAttacking)
            {
                // Vector from mimic to player
                float distanceX = player.Center.X - NPC.Center.X;

                // Only act if player is moving
                if (Math.Abs(player.velocity.X) > 0.1f)
                {
                    // Determine if player is moving toward or away from mimic
                    bool movingToward = (distanceX > 0 && player.velocity.X > 0) || (distanceX < 0 && player.velocity.X < 0);
                    bool movingAway = (distanceX > 0 && player.velocity.X < 0) || (distanceX < 0 && player.velocity.X > 0);

                    if (movingToward || movingAway)
                    {
                        NPC.velocity.X = player.velocity.X; // Match player's horizontal speed
                    }
                }
                else
                {
                    // Player not moving
                    NPC.velocity.X = 0f;
                }
            }

            // Face the direction of movement
            if (!isAttacking)
            {
                if (NPC.velocity.X != 0f)
                {
                    NPC.direction = Math.Sign(NPC.velocity.X);
                    NPC.spriteDirection = NPC.direction;
                }
            }

            // Vertical mimic (jump when player jumps)
            bool onGround = NPC.velocity.Y == 0f;

            // Jump only when mimic is on the ground and player is pressing jump
            if (player.controlJump && onGround)
            {
                // Use player's jump speed and apply a small random factor for natural movement
                NPC.velocity.Y = -player.jumpSpeedBoost + (Main.rand.NextFloat() * 0.1f);
            }

            // Autonomous attack behavior
            bool canAttack = NPC.ai[1] <= 0f && !isAttacking && NPC.Distance(player.Center) < 220f;

            if (canAttack && (player.controlUseItem || Main.rand.NextFloat() < 0.05f))
            {
                TryMeleeSwing(player);
            }

            // Extra attack chance when low on life
            if (NPC.life < NPC.lifeMax * 0.25f && canAttack && Main.rand.NextFloat() < 0.3f)
            {
                TryMeleeSwing(player, force: true);
            }
        }

        private void TryMeleeSwing(Player player, bool force = false)
        {
            SoundEngine.PlaySound(SoundID.Item1 with { Volume = 0.9f, PitchVariance = 2f }, NPC.Center);

            // Jump towards player
            Vector2 dir = (player.Center - NPC.Center).SafeNormalize(Vector2.Zero);
            float launchSpeed = force ? 9.5f : 7.5f;
            NPC.velocity.X = dir.X * launchSpeed;
            NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -12f, 12f);

            // Attack state
            NPC.ai[0] = 25f;
            NPC.ai[1] = force ? 30f : 50f;
            NPC.netUpdate = true;
            NPC.netSpam = 0;
        }

        public override void FindFrame(int frameHeight)
        {
            frameHeight = NPC.height / Main.npcFrameCount[Type];

            if (NPC.velocity.Y != 0f)
            {
                NPC.frame.Y = frameHeight * 3;
            }
            else if (Math.Abs(NPC.velocity.X) > 0.5f)
            {
                NPC.frameCounter += 0.2f;
                if (NPC.frameCounter >= 4) NPC.frameCounter = 0f;
                NPC.frame.Y = (int)NPC.frameCounter * frameHeight;
            }
            else
            {
                NPC.frame.Y = 0;
            }

            NPC.spriteDirection = NPC.direction;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneOverworldHeight ? 1f : 0f;
        }
    }
}
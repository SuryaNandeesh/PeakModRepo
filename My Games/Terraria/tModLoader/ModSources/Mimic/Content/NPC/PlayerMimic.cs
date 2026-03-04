using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Mimic.Content
{
    internal class PlayerMimic : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.width = 1;
            NPC.height = 1;
            NPC.damage = 50;
            NPC.defense = 100;
            NPC.lifeMax = 1000;
            NPC.HitSound = Terraria.ID.SoundID.NPCHit1;
            NPC.DeathSound = Terraria.ID.SoundID.NPCDeath1;
            NPC.aiStyle = -1;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.value = Item.buyPrice(silver: 5);
            NPC.npcSlots = 1f;
        }

        public override void AI()
        {
            NPC.TargetClosest(true);

            if (NPC.target < 0 || NPC.target >= Main.maxPlayers) return;

            Player player = Main.player[NPC.target];

            // === MIMIC HORIZONTAL MOVEMENT ===
            float desiredSpeed = 0f;
            if (player.controlLeft) desiredSpeed -= player.maxRunSpeed;
            if (player.controlRight) desiredSpeed += player.maxRunSpeed;

            float accel = player.runAcceleration * 0.8f;

            if (desiredSpeed != 0f)
            {
                // accelerate toward player's speed
                if (NPC.velocity.X < desiredSpeed) NPC.velocity.X += accel;
                else if (NPC.velocity.X > desiredSpeed) NPC.velocity.X -= accel;
            }
            else
            {
                NPC.velocity.X *= 0.95f;
            }

            // Face the same direction the player is facing
            NPC.direction = player.direction;
            NPC.spriteDirection = NPC.direction;

            bool onGround = NPC.velocity.Y == 0f && NPC.oldVelocity.Y >= 0f;
            if (player.controlJump && onGround && !player.controlDown)
            {
                NPC.velocity.Y = -player.jumpSpeedBoost;
            }

            // Mimic attack animation or simple attack
            if (player.controlUseItem)
            {
                // Example: face player and swing (or spawn a projectile)
                // You could also check player.HeldItem and copy it, but that's advanced
            }
        }

        public override void FindFrame(int frameHeight)
        {
            // Simple walk/jump animation example
            if (NPC.velocity.Y != 0)
            {
                NPC.frame.Y = frameHeight * 3;
            }
            else if (Math.Abs(NPC.velocity.X) > 0.5f)
            {
                NPC.frameCounter += 0.2f;
                if (NPC.frameCounter >= 4) NPC.frameCounter = 0;
                NPC.frame.Y = (int)NPC.frameCounter * frameHeight;
            }
            else 
            {
                NPC.frame.Y = 0;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneOverworldHeight ? 0.02f : 0f;
        }
    }
}

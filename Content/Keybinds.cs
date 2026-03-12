using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace TheMimic.Commands
{
    // This class defines a chat command
    public class SpawnMimicCommand : ModCommand
    {
        // Command info
        public override CommandType Type => CommandType.Chat;

        public override string Command => "spawnmimic"; // The chat command: /spawnmimic

        public override string Description => "Spawns the Enemy Mimic in front of you and makes it daytime.";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Player player = caller.Player;

            // Make it daytime
            Main.dayTime = true;
            Main.time = 13500.0;

            // Spawn in front of player
            int spawnX = (int)(player.Center.X + (120 * player.direction));
            int spawnY = (int)(player.Bottom.Y - 20);

            NPC.NewNPC(
                player.GetSource_FromThis(),
                spawnX,
                spawnY,
                ModContent.NPCType<Content.NPCs.EnemyMimic>()
            );

            // Confirmation message
            Main.NewText("Enemy Mimic spawned!", 255, 255, 0);
        }
    }
}
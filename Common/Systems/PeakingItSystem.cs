using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace TreeSword.Common.Systems
{
	public class PeakingItSystem : ModSystem
	{
		// Per-world progression flag (saved to the world file). The Vocal Vendor uses this
		// to decide whether they can move in. The (future) Doppelganger NPC will set it.
		public static bool DownedDoppelganger;

		// Per-session (per world load) appearance choice for the Vocal Vendor.
		// Requirement: randomized at world load, consistent for the whole session.
		public static int VocalVendorVariant;

		public override void OnWorldLoad()
		{
			DownedDoppelganger = false;
			VocalVendorVariant = Main.rand.Next(4);
		}

		public override void OnWorldUnload()
		{
			DownedDoppelganger = false;
			VocalVendorVariant = 0;
		}

		public override void SaveWorldData(TagCompound tag)
		{
			tag["DownedDoppelganger"] = DownedDoppelganger;
		}

		public override void LoadWorldData(TagCompound tag)
		{
			DownedDoppelganger = tag.GetBool("DownedDoppelganger");
			// Variant is per-session, so we intentionally do NOT load it here.
		}

		public override void NetSend(BinaryWriter writer)
		{
			writer.Write(DownedDoppelganger);
		}

		public override void NetReceive(BinaryReader reader)
		{
			DownedDoppelganger = reader.ReadBoolean();
		}
	}
}


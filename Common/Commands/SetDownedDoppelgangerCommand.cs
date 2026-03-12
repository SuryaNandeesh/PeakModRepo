using Terraria.ModLoader;
using TreeSword.Common.Systems;

namespace TreeSword.Common.Commands
{
	public class SetDownedDoppelgangerCommand : ModCommand
	{
		// Debug/testing helper: since we aren't implementing the Doppelganger NPC yet,
		// this lets you toggle the move-in condition for the Vocal Vendor.
		public override string Command => "downeddoppelganger";
		public override CommandType Type => CommandType.Chat;
		public override string Usage => "/downeddoppelganger <true|false>";
		public override string Description => "Sets the world flag used to allow the Vocal Vendor to move in (testing helper).";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			// Parse the boolean argument and update the world flag, replying with feedback or usage help.
			if (args.Length != 1 || !bool.TryParse(args[0], out bool value))
			{
				caller.Reply($"Usage: {Usage}");
				return;
			}

			PeakingItSystem.DownedDoppelganger = value;
			caller.Reply($"DownedDoppelganger set to {value}.");
		}
	}
}


using Microsoft.Xna.Framework;
using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace ExampleMod.Content.Currencies
{
	// An example of a custom currency, similar to the Defenders Medal.
	// This class implements the ILoadable interface, meaning that its parameterless constructor will be used
	// to auto-register an instance of it into our mod, automatically invoking Load() and Unload().
	public class ExampleCustomCurrency : CustomCurrencySingleCoin, ILoadable
	{
		// A static property where we store the ID of our currency.
		// This is what Item.shopSpecialCurrency will be set to to make a shop entry use our currency.
		public static int CurrencyId { get; private set; }

		// This constructor is used by ILoadable, it is not to be used by anything else.
		private ExampleCustomCurrency() : base(-1, -1) { }

		public ExampleCustomCurrency(int coinItemId, long currencyCap, string currencyTextKey, Color currencyTextColor) : base(coinItemId, currencyCap) {
			CurrencyTextKey = currencyTextKey;
			CurrencyTextColor = currencyTextColor;
		}

		// This is the explicit implementation syntax for interface methods, it lets us make a private method that is still callable by interfaces.
		void ILoadable.Load(Mod mod) {
			// This call actually registers our custom currency.
			CurrencyId = CustomCurrencyManager.RegisterCurrency(new ExampleCustomCurrency(
				// We assign ExampleItem as the item used for this currency, there is no "ExampleCustomCurrency" item.
				coinItemId: ModContent.ItemType<Items.ExampleItem>(),
				// The currency is the max amount of held currency that will be counted and displayed.
				// Item prices in shops should not be higher than this value if you want them to be ever affordable.
				currencyCap: 999,
				// Localization string and color that are used for this currency's name.
				currencyTextKey: "Mods.ExampleMod.Currencies.ExampleCustomCurrency",
				currencyTextColor: Color.BlueViolet
			));
		}
		void ILoadable.Unload() { }
	}
}
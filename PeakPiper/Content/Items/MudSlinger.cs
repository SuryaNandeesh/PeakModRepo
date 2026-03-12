using PeakPiper.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PeakPiper.Content.Items
{
    public class MudSlinger : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Yoyo[Type] = true; 
            ItemID.Sets.GamepadExtraRange[Type] = 15; 
            ItemID.Sets.GamepadSmartQuickReach[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 24; 
            Item.height = 24; 

            Item.useStyle = ItemUseStyleID.Shoot; 
            Item.useTime = 25; 
            Item.useAnimation = 25; 
            Item.noMelee = true; 
            Item.noUseGraphic = true; 
            Item.UseSound = SoundID.Item1; 

            Item.damage = 14; 
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.knockBack = 0.8f; 
            Item.crit = 50; 
            Item.channel = true; 
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 50); 

            Item.shoot = ModContent.ProjectileType<MudSlingerProjectile>(); 
            Item.shootSpeed = 16f; 
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MudBlock, 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
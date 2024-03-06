namespace AQOL.Content.Items.Accessories;

internal class HuntersGuide : ModItem
{
    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.Size = new Vector2(26, 36);
        Item.rare = ItemRarityID.Green;
        Item.value = Item.buyPrice(0, 8, 0, 0);
    }

    public override void UpdateEquip(Player player) => player.dontHurtCritters = false;
    public override void UpdateInventory(Player player) => player.dontHurtCritters = false;

    internal class CritterPlayer : ModPlayer
    {
        public override void ResetEffects() => Player.dontHurtCritters = true;
    }
}

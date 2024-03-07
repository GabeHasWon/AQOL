using Terraria.GameContent.ItemDropRules;

namespace AQOL.Common.World.Items;

internal class BossBagGlobal : GlobalItem
{
    public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
    {
        if (item.type == ItemID.SkeletronPrimeBossBag)
            itemLoot.Add(ItemDropRule.Common(ItemID.ZapinatorOrange));
        else if (item.type == ItemID.DestroyerBossBag)
            itemLoot.Add(ItemDropRule.Common(ItemID.BeamSword));
        else if (item.type == ItemID.TwinsBossBag)
            itemLoot.Add(ItemDropRule.Common(ItemID.Uzi));
    }
}

using AQOL.Content.Items.Accessories;

namespace AQOL.Common.World.NPCs;

internal class VendorGlobal : GlobalNPC
{
    public override void ModifyShop(NPCShop shop)
    {
        if (shop.NpcType == NPCID.Merchant)
            shop.Add(new NPCShop.Entry(ModContent.ItemType<HuntersGuide>()));
        else if (shop.NpcType == NPCID.BestiaryGirl)
        {
            var entry = shop.GetEntry(ItemID.DontHurtCrittersBook);
            entry.Disable();
        }
    }
}

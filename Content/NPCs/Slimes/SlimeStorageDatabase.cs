using System.Linq;
using Terraria.Utilities;

namespace AQOL.Content.NPCs.Slimes;

internal class SlimeStorageDatabase : ModSystem
{
    public WeightedRandom<int> ItemPool = new();

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<AQOLServerConfig>().SlimeReplacement;

    public override void PostSetupContent()
    {
        ItemPool.Clear();
        ItemPool.Add(ItemID.Apple, 1);
        ItemPool.Add(ItemID.Apricot, 1);
        ItemPool.Add(ItemID.Banana, 1);
        ItemPool.Add(ItemID.Cherry, 1);
        ItemPool.Add(ItemID.Grapefruit, 1);
        ItemPool.Add(ItemID.Lemon, 1);
        ItemPool.Add(ItemID.Mango, 1);
        ItemPool.Add(ItemID.Peach, 1);
        ItemPool.Add(ItemID.Pineapple, 1);
        ItemPool.Add(ItemID.Plum, 1);
    }

    /// <summary>
    /// Ported from vanilla.
    /// </summary>
    private static int VanillaItemInsideBody()
    {
        int num = Main.rand.Next(4);

        switch (num)
        {
            case 0:
                switch (Main.rand.Next(7))
                {
                    case 0:
                        return 290;
                    case 1:
                        return 292;
                    case 2:
                        return 296;
                    case 3:
                        return 2322;
                    default:
                        if (Main.netMode != NetmodeID.SinglePlayer && Main.rand.NextBool(2))
                            return 2997;
                        return 2350;
                }
            case 1:
                return Main.rand.Next(4) switch
                {
                    0 => 8,
                    1 => 166,
                    2 => 965,
                    _ => 58,
                };
            case 2:
                return Main.rand.Next(ItemID.Sets.OreDropsFromSlime.Keys.ToList());
            default:
                return Main.rand.Next(3) switch
                {
                    0 => 71,
                    1 => 72,
                    _ => 73,
                };
        }
    }

    public static int DetermineItem(bool guarantee)
    {
        if (Main.rand.NextBool(4))
            return VanillaItemInsideBody();
        else if (!Main.rand.NextBool(2))
            return ModContent.GetInstance<SlimeStorageDatabase>().ItemPool;

        return guarantee ? DetermineItem(guarantee) : -1;
    }
}

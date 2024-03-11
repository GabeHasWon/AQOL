using System.Collections.Generic;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace AQOL.Common.World.Steps;

// Technically I should use ModSystem PostWorldGen. Too bad!

internal class GoldChestRareItemStep : GenStep
{
    public override string GenName => "AQOL: Rare Items in Chests";

    public override int GenIndex(List<GenPass> passes) => passes.Count;

    public override void Generation(GenerationProgress progress, GameConfiguration config)
    {
        int[] itemIds = [ItemID.Nazar, ItemID.Bezoar, ItemID.BonePickaxe, ItemID.BoneSword, ItemID.BlackLens, ItemID.MiningPants, ItemID.MiningShirt];
        int itemToPlace = WorldGen.genRand.Next(itemIds.Length);
        int successes = 0;

        for (int i = 0; i < Main.maxChests; i++)
        {
            Chest chest = Main.chest[i];

            if (chest == null)
                continue;

            Tile chestTile = Main.tile[chest.x, chest.y];

            if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 36)
            {
                if (WorldGen.genRand.NextBool(8))
                    continue;

                for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                {
                    if (chest.item[inventoryIndex].type == ItemID.None)
                    {
                        chest.item[inventoryIndex].SetDefaults(itemIds[itemToPlace]);
                        itemToPlace = (itemToPlace + 1) % itemIds.Length;
                        successes++;
                        break;
                    }
                }
            }
        }
    }
}

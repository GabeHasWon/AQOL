using AQOL.Content.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.WorldBuilding;

namespace AQOL.Common.World.Steps;

internal class PilesStep : GenStep
{
    public override string GenName => "AQOL: Piles";

    public override int GenIndex(List<GenPass> tasks) => tasks.FindIndex(x => x.Name == "Piles");

    public override void Generation(GenerationProgress progress, GameConfiguration config)
    {
        for (int i = 0; i < 15 * (Main.maxTilesX / 4200f); ++i)
        {
            int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            int y = WorldGen.genRand.Next((int)Main.worldSurface - 200, (int)Main.worldSurface + 20);
            int style = WorldGen.genRand.Next(4);

            bool hasGrass = true;

            for (int j = 0; j < 3; ++j)
                if (!Main.tile[x + j, y + 1].HasTile || Main.tile[x + j, y + 1].TileType != TileID.Grass)
                    hasGrass = false;

            if (!hasGrass)
            {
                i--;
                continue;
            }

            if (!WorldGen.PlaceObject(x, y, ModContent.TileType<VeggieDecor>(), true, style))
                i--;
        }
    }
}

﻿using AQOL.Common.Mono.Detours;
using AQOL.Common.Mono.Edits;
using System.Collections.Generic;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace AQOL.Common.World.Steps;

internal class MossStep : GenStep
{
    public override string GenName => "AQOL: More Moss";

    public override int GenIndex(List<GenPass> tasks) => tasks.FindIndex(x => x.Name == "Moss");

    public override void Generation(GenerationProgress progress, GameConfiguration config)
    {
        StopUnderworldMossEdit.AQOLMossGen = true;

        for (int i = 0; i < 200 * (Main.maxTilesX / 4200f); ++i)
        {
            int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            int y = WorldGen.genRand.Next(Main.maxTilesY / 2, Main.maxTilesY - 250);
            WorldGen.Spread.Moss(x, y);
        }

        StopUnderworldMossEdit.AQOLMossGen = false;
    }
}

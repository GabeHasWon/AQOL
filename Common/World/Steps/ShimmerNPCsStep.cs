using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AQOL.Common.World.Steps;

internal class ShimmerNPCStep : GenStep
{
    public override string GenName => "AQOL: Shimmer WorldGen NPCs";

    public override int GenIndex(List<GenPass> tasks) => tasks.Count - 1;

    public override void Generation(GenerationProgress progress, GameConfiguration config)
    {
        for (int i = 0; i < Main.maxNPCs; ++i)
        {
            NPC npc = Main.npc[i];

            if (npc.active && npc.townNPC && NPCID.Sets.ShimmerTownTransform[npc.type] && npc.townNpcVariationIndex != 1)
                npc.townNpcVariationIndex = 1;
        }
    }
}
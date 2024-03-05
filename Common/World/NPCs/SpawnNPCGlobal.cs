using System.Collections.Generic;

namespace AQOL.Common.World.NPCs;

internal class SpawnNPCGlobal : GlobalNPC
{
    public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
    {
        if (!Main.dayTime)
            pool[NPCID.Ghost] = 0.05f;
    }
}

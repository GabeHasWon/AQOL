
using AQOL.Content.NPCs.Critters;

namespace AQOL.Common.World.NPCs;

internal class CritterGroupNPC : GlobalNPC
{
    public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => NPCID.Sets.CountsAsCritter[entity.type] && entity.type < NPCID.Count;

    public override void OnSpawn(NPC npc, IEntitySource source)
    {
        if (source is EntitySource_SpawnNPC && !CritterNPC.StopGroups)
        {
            CritterNPC.StopGroups = true;
            CritterNPC.SpawnCritterGroup(npc.whoAmI, (int)(npc.Center.X / 16f), (int)(npc.Center.Y / 16f), npc);
            CritterNPC.StopGroups = false;
        }
    }
}

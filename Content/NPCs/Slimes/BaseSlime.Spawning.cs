using Terraria.Utilities;

namespace AQOL.Content.NPCs.Slimes;

internal abstract partial class BaseSlime : ModNPC
{
    public static WeightedRandom<int> SlimeCount;

    public override void Load()
    {
        base.Load();

        SlimeCount = new();
        SlimeCount.Add(2, 1f);
        SlimeCount.Add(1, 0.5f);
        SlimeCount.Add(0, 0.3f);
    }

    public override int SpawnNPC(int tileX, int tileY)
    {
        int npc = base.SpawnNPC(tileX, tileY);
        int count = SlimeCount;

        if (count != 0)
        {
            for (int i = 0; i < count; ++i)
            {
                int newNPC = NPC.NewNPC(Terraria.Entity.GetSource_NaturalSpawn(), tileX * 16, tileY * 16, Type, npc);
                NPC slime = Main.npc[newNPC];
                slime.velocity = Main.rand.NextVector2Circular(4, 4);
                slime.netUpdate = true;
            }
        }
        else
        {
            int newNPC = NPC.NewNPC(Terraria.Entity.GetSource_NaturalSpawn(), tileX * 16, tileY * 16, Type, npc);
            (Main.npc[newNPC].ModNPC as BaseSlime)._storedItem = SlimeStorageDatabase.DetermineItem(true);
            Main.npc[newNPC].netUpdate = true;
        }

        return npc;
    }
}

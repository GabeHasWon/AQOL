using System.Linq;

namespace AQOL.Content.NPCs.Slimes;

internal class RemoveVanillaSlimes : ILoadable
{
    public static int[] RemovedSlimes = [NPCID.GreenSlime, NPCID.BlueSlime, NPCID.RedSlime, NPCID.PurpleSlime, NPCID.YellowSlime, NPCID.IceSlime, NPCID.CorruptSlime, NPCID.Crimslime, 
        NPCID.MotherSlime, NPCID.SandSlime, NPCID.SpikedIceSlime, NPCID.JungleSlime, NPCID.SpikedJungleSlime, NPCID.BabySlime, NPCID.LavaSlime, NPCID.DungeonSlime, NPCID.Pinky, 
        NPCID.ToxicSludge];

    public void Load(Mod mod) => On_NPC.NewNPC += StopSlimes;

    private int StopSlimes(On_NPC.orig_NewNPC orig, IEntitySource source, int X, int Y, int Type, int Start, float ai0, float ai1, float ai2, float ai3, int Target)
    {
        if (!ModContent.GetInstance<AQOLServerConfig>().SlimeReplacement)
            return orig(source, X, Y, Type, Start, ai0, ai1, ai2, ai3, Target);

        if (Type < NPCID.Count && RemovedSlimes.Contains(Type))
            return 200;

        return orig(source, X, Y, RemovedSlimes.Contains(Type) ? ModContent.NPCType<EmptyNPC>() : Type, Start, ai0, ai1, ai2, ai3, Target);
    }

    public void Unload() => RemovedSlimes = null;
}

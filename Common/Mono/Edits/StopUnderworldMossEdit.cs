using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace AQOL.Common.Mono.Edits;

internal class StopUnderworldMossEdit : Modification
{
    public static bool AQOLMossGen = false;

    public override void Load(Mod mod)
    {
        IL_WorldGen.Spread.Moss += SpreadMossStopUnderworld;
    }

    private static void SpreadMossStopUnderworld(ILContext il)
    {
        ILCursor c = new(il);

        for (int i = 0; i < 2; ++i)
            c.GotoNext(x => x.MatchCall<WorldGen>(nameof(WorldGen.InWorld)));

        c.GotoNext(x => x.MatchBrtrue(out _));

        c.Emit(OpCodes.Ldloc_S, (byte)5);
        c.EmitDelegate(CanPlaceMoss);
    }

    public static bool CanPlaceMoss(bool original, Point pos)
    {
        if (!AQOLMossGen)
            return original;

        if (pos.Y < Main.maxTilesY - 205 + WorldGen.genRand.Next(6))
            return original;

        return false;
    }
}

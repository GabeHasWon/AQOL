//using Mono.Cecil.Cil;
//using MonoMod.Cil;
//using System;

//namespace AQOL.Common.World.Tiles;

//[Obsolete("Not proper approach.")]
//internal class ShakeTreeGlobalTile_AddFruitToTable : GlobalTile
//{
//    public override bool IsLoadingEnabled(Mod mod) => false;

//    public override void Load()
//    {
//        IL_WorldGen.ShakeTree += ModifyForestTreeFruit;
//    }

//    private void ModifyForestTreeFruit(ILContext il)
//    {
//        ILCursor c = new(il);

//        c.GotoNext(x => x.MatchSwitch(out _));
//        c.GotoNext(x => x.MatchSwitch(out _));

//        int index = 0;

//        c.GotoNext(x => x.MatchLdloc(out index));

//        c.Emit(OpCodes.Ldloca_S, (byte)index);
//        c.EmitDelegate(ModifyFruitType);
//    }

//    private static void ModifyFruitType(ref int type)
//    {
//        // Apple, Peach, Grapefruit, Lemon, Apricot
//        if (Main.rand.NextBool(7))
//        {

//        }
//    }
//}

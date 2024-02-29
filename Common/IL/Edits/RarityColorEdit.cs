using Humanizer;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Threading.Channels;
using Terraria;
using Terraria.ModLoader;

namespace AQOL.Common.IL.Edits;

internal class RarityColorEdit : ILEdit
{
    public override void Load(Mod mod)
    {
        IL_Main.MouseText_DrawItemTooltip += ModifyRarity;
    }

    private void ModifyRarity(ILContext il)
    {
        ILCursor c = new(il);

        if (!c.TryGotoNext(x => x.MatchLdstr("ItemName")))
            return;

        ILLabel label = null;

        if (!c.TryGotoNext(MoveType.After, x => x.MatchBrfalse(out ILLabel label)))
            return;

        if (label is null)
            label = c.Prev.Operand as ILLabel;

        c.Emit(OpCodes.Ldloca_S, (byte)53);
        c.Emit(OpCodes.Ldarg_2);
        c.EmitDelegate(ModifyColor);
        c.Emit(OpCodes.Br, label);
    }

    private static void ModifyColor(ref Color color, int rarity)
    {
        color = rarity switch
        {
            0 => Color.White,
            1 => Color.MediumPurple,
            2 => Color.Violet,
            3 => Color.LightBlue,
            4 => Color.Aqua,
            5 => Color.Aquamarine,
            6 => Color.Green,
            7 => Color.Lime,
            8 => Color.Yellow,
            9 => Color.DarkOrange,
            10 => Color.Orange,
            11 => Color.White, // tbd
            12 => Color.DarkRed,
            _ => Color.Gray
        };
    }
}

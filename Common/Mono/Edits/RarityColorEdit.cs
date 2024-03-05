using AQOL.Common.Mono;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AQOL.Common.Mono.Detours;

internal class RarityColorEdit : Modification
{
    public override void Load(Mod mod)
    {
        IL_Main.MouseText_DrawItemTooltip += ModifyRarity;
        IL_Main.MouseTextInner += ModifyRarityInner;
    }

    private void ModifyRarityInner(ILContext il)
    {
        ILCursor c = new(il);

        c.TryGotoNext(MoveType.After, x => x.MatchLdsfld<Main>(nameof(Main.spriteBatch)));

        c.Emit(OpCodes.Ldloca_S, (byte)11);
        c.Emit(OpCodes.Ldloc_1);
        c.EmitDelegate(ModifyColor); // Override the original color
    }

    private void ModifyRarity(ILContext il)
    {
        ILCursor c = new(il);

        if (!c.TryGotoNext(x => x.MatchLdstr("ItemName")))
            return;

        ILLabel label = null;

        if (!c.TryGotoNext(MoveType.After, x => x.MatchBrfalse(out ILLabel label)))
            return;

        label ??= c.Prev.Operand as ILLabel;

        c.Emit(OpCodes.Ldloca_S, (byte)53);
        c.Emit(OpCodes.Ldarg_2);
        c.EmitDelegate(ModifyColor);
        c.Emit(OpCodes.Br, label); // Skip the original color code
    }

    private static void ModifyColor(ref Color color, int rarity)
    {
        // Switch doesn't contain the mode rarities as they're unique
        Color col = rarity switch
        {
            ItemRarityID.White => Color.White,
            ItemRarityID.Blue => Color.Violet,
            ItemRarityID.Green => Color.LightBlue,
            ItemRarityID.Orange => Color.Aqua,
            ItemRarityID.LightRed => Color.MediumPurple,
            ItemRarityID.Pink => Color.Aquamarine,
            ItemRarityID.LightPurple => Color.MediumSpringGreen,
            ItemRarityID.Lime => Color.Lime,
            ItemRarityID.Yellow => Color.Yellow,
            ItemRarityID.Cyan => Color.DarkOrange,
            ItemRarityID.Red => Color.Red,
            ItemRarityID.Purple => Color.Fuchsia,
            ItemRarityID.Quest => Color.Brown,
            _ when rarity > ItemRarityID.Purple => RarityLoader.GetRarity(rarity).RarityColor,
            _ => color,
        };

        if (Main.HoverItem.expert || rarity == ItemRarityID.Expert)
            col = Color.Lerp(Color.Orange, Color.Red, MathF.Pow(MathF.Sin(Main.GameUpdateCount * 0.06f), 2));

        if (Main.HoverItem.master || rarity == ItemRarityID.Master)
            col = Color.DarkRed;

        color = col * (Main.mouseTextColor / 255f);
    }
}

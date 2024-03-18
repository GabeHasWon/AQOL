using AQOL.Common.Systems.UI;
using System;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;
using Terraria.WorldBuilding;

namespace AQOL.Common.Mono.Detours;

internal class WorldOreSelectionDetour : Modification
{
    public static readonly Dictionary<string, List<int>> OreIds = new()
    {
        { "Copper", new List<int>() { TileID.Copper, TileID.Tin } },
        { "Iron", new List<int>() { TileID.Iron, TileID.Lead } },
        { "Silver", new List<int>() { TileID.Silver, TileID.Tungsten } },
        { "Gold", new List<int>() { TileID.Gold, TileID.Platinum } },
        { "Cobalt", new List<int>() { TileID.Cobalt, TileID.Palladium } },
        { "Mythril", new List<int>() { TileID.Mythril, TileID.Orichalcum } },
        { "Adamantite", new List<int>() { TileID.Adamantite, TileID.Titanium } },
    };

    public static readonly Dictionary<string, int?> SelectedOreIds = new()
    {
        { "Copper", null },
        { "Iron", null },
        { "Silver", null },
        { "Gold", null },
        { "Cobalt", null },
        { "Mythril", null },
        { "Adamantite", null },
    };

    public static readonly Dictionary<int, int> OreToBarId = new()
    {
        { TileID.Copper, ItemID.CopperBar },
        { TileID.Tin, ItemID.TinBar },
        { TileID.Iron, ItemID.IronBar },
        { TileID.Lead, ItemID.LeadBar },
        { TileID.Silver, ItemID.SilverBar },
        { TileID.Tungsten, ItemID.TungstenBar },
        { TileID.Gold, ItemID.GoldBar },
        { TileID.Platinum, ItemID.PlatinumBar },
    };

    public override void Load(Mod mod)
    {
        On_UIWorldCreation.BuildPage += AddOre;
    }

    private void AddOre(On_UIWorldCreation.orig_BuildPage orig, UIWorldCreation self)
    {
        orig(self);

        UIPanel panel = new()
        {
            HAlign = 0.5f,
            Width = StyleDimension.FromPixels(240),
            Height = StyleDimension.FromPixels(160),
            Left = StyleDimension.FromPixels(376),
            Top = StyleDimension.FromPixels(200),
        };

        self.Append(panel);

        panel.Append(new UIText(Language.GetTextValue("Mods.AQOL.WorldOres"), 1.2f));
        string[] ores = ["Copper", "Iron", "Silver", "Gold"];//, "Cobalt", "Mythril", "Adamantite"];

        for (int i = 0; i < ores.Length; ++i)
        {
            string ore = ores[i];
            panel.Append(new UIText(Language.GetTextValue("Mods.AQOL.Ores." + ore) + ":", 0.95f) { Top = StyleDimension.FromPixels(30 + (i * 24)) });
            panel.Append(new UIOreChoice(ores[i], 0.95f) { Top = StyleDimension.FromPixels(30 + (i * 24)), HAlign = 1f });
        }
    }
}

public class SetOreSystem : ModSystem
{
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
    {
        int index = tasks.FindIndex(x => x.Name == "Reset");

        if (index != -1)
            tasks.Insert(index + 2, new PassLegacy("AQOL: Set Ores", (GenerationProgress progress, GameConfiguration passConfig) =>
            {
                SetOreTileAndBar("Copper", ref GenVars.copper, ref GenVars.copperBar);
                SetOreTileAndBar("Iron", ref GenVars.iron, ref GenVars.ironBar);
                SetOreTileAndBar("Silver", ref GenVars.silver, ref GenVars.silverBar);
                SetOreTileAndBar("Gold", ref GenVars.gold, ref GenVars.goldBar);
            }));
    }

    private static void SetOreTileAndBar(string key, ref int tile, ref int bar)
    {
        if (WorldOreSelectionDetour.SelectedOreIds[key].HasValue)
        {
            tile = WorldOreSelectionDetour.SelectedOreIds[key].Value;
            bar = WorldOreSelectionDetour.OreToBarId[tile];
        }
    }
}
using MonoMod.Cil;
using Terraria.Localization;

namespace AQOL.Common.Mono.Detours;

internal class WorldToIslandEdit : Modification
{
    public static string World
    {
        get => Language.ActiveCulture.Name switch
        {
            "es-ES" => "Mundo",
            _ => "World"
        };
    }

    public static string WorldLowercase
    {
        get => Language.ActiveCulture.Name switch
        {
            "es-ES" => "mundo",
            _ => "world"
        };
    }

    public static string Island
    {
        get => Language.ActiveCulture.Name switch
        {
            "es-ES" => "Isla",
            _ => "Island"
        };
    }

    public static string IslandLowercase
    {
        get => Language.ActiveCulture.Name switch
        {
            "es-ES" => "isla",
            _ => "island"
        };
    }

    public override void Load(Mod mod)
    {
        IL_LanguageManager.LoadLanguageFromFileTextJson += EditWorldToIslandJson;
        IL_LanguageManager.LoadLanguageFromFileTextCsv += EditWorldToIslandCsv;
    }

    private void EditWorldToIslandCsv(ILContext il)
    {
        ILCursor c = new(il);

        for (int i = 0; i < 2; ++i)
            c.GotoNext(MoveType.After, x => x.MatchLdloc(9));

        c.EmitDelegate(EditEntry);
    }

    private static void EditWorldToIslandJson(ILContext il)
    {
        ILCursor c = new(il);

        c.GotoNext(MoveType.After, x => x.MatchStloc(4));
        c.GotoNext(MoveType.Before, x => x.MatchCallvirt<LocalizedText>("SetValue"));

        c.EmitDelegate(EditEntry);
    }

    /// <summary>
    /// Super hacky way of modifying each entry's value and changing World to Island.<br/>
    /// This forcefully accounts for localization using a hardcoded system as the proper normal system may not be loaded.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string EditEntry(string value)
    {
        if (value.Contains(World))
            return value.Replace(World, Island);

        if (value.Contains(WorldLowercase))
            return value.Replace(WorldLowercase, IslandLowercase);

        return value;
    }
}

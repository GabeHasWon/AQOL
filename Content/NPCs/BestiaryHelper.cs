using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria.GameContent.Bestiary;
using Terraria.Localization;

namespace AQOL.Content.NPCs;

/// <summary>
/// Copied from the impl. of the same name in Spirit.
/// </summary>
internal class BestiaryHelper : ILoadable
{
    private static Dictionary<string, IBestiaryInfoElement> _ConditionsByName = null;

    public void Load(Mod mod)
    {
        // Set up dictionaries
        _ConditionsByName = [];

        // Load all conditions
        LoadNestedClassConditions(typeof(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes));
        LoadNestedClassConditions(typeof(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events));
        LoadNestedClassConditions(typeof(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Invasions));
        LoadNestedClassConditions(typeof(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times));
        LoadNestedClassConditions(typeof(BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals));
    }

    /// <summary>
    /// Builds a BestiaryInfoElement for the given NPC with the given conditions.<br/>
    /// Also automatically generates a bestiary entry for the NPC, defaults to <see cref="string.Empty"/>.
    /// </summary>
    /// <param name="npc"></param>
    /// <param name="conditions"></param>
    /// <returns></returns>
    public static IBestiaryInfoElement[] BuildEntry(ModNPC npc, string conditions)
    {
        string entryKey = $"Mods.{npc.Mod.Name}.NPCs.{npc.Name}.Entry"; // Make the key,
        Language.GetOrRegister(entryKey, () => ""); // Register it,

        var flavour = new FlavorTextBestiaryInfoElement(entryKey); // And use it automatically for the flavour text.

        if (conditions == string.Empty)
            return [flavour];

        string[] allConditions = conditions.Split(' ');
        IBestiaryInfoElement[] elements = new IBestiaryInfoElement[allConditions.Length + 1];

        elements[0] = flavour;

        for (int i = 1; i < elements.Length; ++i)
            elements[i] = _ConditionsByName[allConditions[i - 1]];

        return elements;
    }

    private static void LoadNestedClassConditions(Type containerType)
    {
        // Get all conditions in the given class 
        var allElements = containerType
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(x => x.FieldType.IsAssignableTo(typeof(IBestiaryInfoElement)));

        foreach (var item in allElements)
        {
            var evnt = item.GetValue(null) as IBestiaryInfoElement;

            if (!_ConditionsByName.ContainsKey(item.Name))
                _ConditionsByName.Add(item.Name, evnt);
            else
                _ConditionsByName.Add(item.DeclaringType.Name + "." + item.Name, evnt);
        }
    }

    public void Unload() => _ConditionsByName = null;
}

public static class BestiaryExtensions
{
    /// <summary>
    /// Adds the conditions to the bestiary entry alongside the localized text automatically.<br/>
    /// Valid entries are as follows:<br/><b>Biomes:</b><br/><c>TheCorruption TheCrimson Surface Graveyard UndergroundJungle TheUnderworld TheDungeon Underground TheHallow UndergroundMushroom
    /// Jungle Caverns UndergroundSnow Ocean SurfaceMushroom UndergroundDesert Snow Desert Meteor Oasis SpiderNest TheTemple CorruptUndergroundDesert CrimsonUndergroundDesert
    /// HallowUndergroundDesert HallowDesert CorruptDesert CrimsonDesert Granite UndergroundCorruption UndergroundCrimson UndergroundHallow Marble CorruptIce HallowIce CrimsonIce
    /// Sky NebulaPillar VortexPillar StardustPillar SolarPillar</c><br/>
    /// <b>Times:</b><br/><c>DayTime NightTime</c><br/>
    /// <b>Events:</b><br/><c>Rain SlimeRain WindyDay BloodMoon Halloween Christmas Eclipse Party Blizzard Sandstorm</c>
    /// </summary>
    public static void AddInfo(this BestiaryEntry bestiaryEntry, ModNPC npc, string conditions) => bestiaryEntry.Info.AddRange(BestiaryHelper.BuildEntry(npc, conditions));
}
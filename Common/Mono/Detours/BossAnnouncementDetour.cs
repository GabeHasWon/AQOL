using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Chat;
using Terraria.Localization;

namespace AQOL.Common.Mono.Detours;

internal class BossAnnouncementDetour : Modification
{
    public static bool SpawningBoss = false;
    public static int SpawnBossId = -1;

    private static readonly Dictionary<string, int> NPCIdsByLocalizedName = [];

    private static string CultureName = string.Empty;

    public override void Load(Mod mod)
    {
        On_NPC.SpawnBoss += CheckBossSpawning;
        On_ChatHelper.BroadcastChatMessage += AlsoHijackBossSpawn;
        On_Main.NewText_string_byte_byte_byte += HijackBossSpawn;
        On_NPC.SpawnWOF += CheckWoFSpawning;

        On_LanguageManager.GetTextValue_string_object += HijackSpawnMessage;

        CultureName = Language.ActiveCulture.Name;
        PopulateNPCNameLookup();
    }

    /// <summary>
    /// This is a last-case scenario as it isn't certain to be run, but it might help.
    /// </summary>
    /// <param name="orig"></param>
    /// <param name="self"></param>
    /// <param name="key"></param>
    /// <param name="arg0"></param>
    /// <returns></returns>
    private string HijackSpawnMessage(On_LanguageManager.orig_GetTextValue_string_object orig, LanguageManager self, string key, object arg0)
    {
        if (SpawningBoss) // Ignore, have the other detours handle it
            return orig(self, key, arg0);

        if (CultureName != Language.ActiveCulture.Name)
            PopulateNPCNameLookup();

        if (key == "Announcement.HasAwoken" && arg0 is string name)
        {
            int id = NPCIdsByLocalizedName[name];
            string newKey = GetKey(id);
            return orig(self, newKey, arg0);
        }
        else
            return orig(self, key, arg0);
    }

    private static void PopulateNPCNameLookup()
    {
        CultureName = Language.ActiveCulture.Name;
        NPCIdsByLocalizedName.Clear();

        foreach (var npc in ContentSamples.NpcsByNetId.Values.Where(x => x.boss || x.type == NPCID.EaterofWorldsHead))
        {
            if (!NPCIdsByLocalizedName.ContainsKey(npc.TypeName))
                NPCIdsByLocalizedName.Add(npc.TypeName, npc.type);
        }
    }

    private static void CheckWoFSpawning(On_NPC.orig_SpawnWOF orig, Vector2 pos)
    {
        SpawningBoss = true;
        SpawnBossId = NPCID.WallofFlesh;
        orig(pos);
        SpawningBoss = false;
    }

    private static void CheckBossSpawning(On_NPC.orig_SpawnBoss orig, int spawnPositionX, int spawnPositionY, int Type, int targetPlayerIndex)
    {
        SpawningBoss = true;
        SpawnBossId = Type;
        orig(spawnPositionX, spawnPositionY, Type, targetPlayerIndex);
        SpawningBoss = false;
    }

    private static void AlsoHijackBossSpawn(On_ChatHelper.orig_BroadcastChatMessage orig, NetworkText text, Color color, int excludedPlayer)
    {
        if (!SpawningBoss)
            orig(text, color, excludedPlayer);
        else
        {
            string key = GetKey(SpawnBossId);
            color = Color.Red;

            if (key == string.Empty)
                orig(text, color, excludedPlayer);
            else
                orig(NetworkText.FromKey(key), color, excludedPlayer);
        }
    }

    private static void HijackBossSpawn(On_Main.orig_NewText_string_byte_byte_byte orig, string newText, byte R, byte G, byte B)
    {
        if (!SpawningBoss)
            orig(newText, R, G, B);
        else
        {
            string key = GetKey(SpawnBossId);
            (R, G, B) = (255, 0, 0);

            if (key == string.Empty)
                orig(newText, R, G, B);
            else
                orig(Language.GetTextValue(key), R, G, B);
        }
    }

    private static string GetKey(int spawnBossId)
    {
        const string Prefix = "Mods.AQOL.BossAnnouncements.";

        return Prefix + spawnBossId switch
        {
            NPCID.EyeofCthulhu => "EoC",
            NPCID.KingSlime => "KingSlime",
            NPCID.EaterofWorldsHead => "EoW",
            NPCID.BrainofCthulhu => "BoC",
            NPCID.QueenBee => "QueenBee",
            NPCID.Skeleton => "Skeletron",
            NPCID.WallofFlesh => "WoF",
            NPCID.Deerclops => "Deerclops",
            NPCID.Retinazer or NPCID.Spazmatism => "Twins",
            NPCID.TheDestroyer => "Destroyer",
            NPCID.QueenSlimeBoss => "QueenSlime",
            NPCID.SkeletronPrime => "SkelePrime",
            NPCID.Plantera => "Plantera",
            NPCID.DukeFishron => "DukeFishron",
            NPCID.Golem => "Golem",
            NPCID.HallowBoss => "EoL",
            NPCID.MoonLordCore => "MoonLord",
            _ => string.Empty
        };
    }
}

using Terraria.Chat;
using Terraria.Localization;

namespace AQOL.Common.Mono.Detours;

internal class WorldAnnouncementDetour : Modification
{
    public static bool UpdatingWorld = false;

    public override void Load(Mod mod)
    {
        On_Main.NewText_string_byte_byte_byte += HijackWorldMessage;
        On_ChatHelper.BroadcastChatMessage += AlsoHijackWorldMessage;
        On_Main.UpdateTime += CheckWorldAnnouncement;
        On_WorldGen.UpdateWorld += AlsoCheckWorldAnnouncement;
    }

    private void AlsoCheckWorldAnnouncement(On_WorldGen.orig_UpdateWorld orig)
    {
        UpdatingWorld = true;
        orig();
        UpdatingWorld = false;
    }

    private void CheckWorldAnnouncement(On_Main.orig_UpdateTime orig)
    {
        UpdatingWorld = true;
        orig();
        UpdatingWorld = false;
    }

    private static void AlsoHijackWorldMessage(On_ChatHelper.orig_BroadcastChatMessage orig, NetworkText text, Color color, int excludedPlayer)
    {
        if (UpdatingWorld)
            color = Color.LightYellow;

        orig(text, color, excludedPlayer);
    }

    private static void HijackWorldMessage(On_Main.orig_NewText_string_byte_byte_byte orig, string newText, byte R, byte G, byte B)
    {
        if (UpdatingWorld)
            (R, G, B) = (240, 240, 10);

        orig(newText, R, G, B);
    }
}

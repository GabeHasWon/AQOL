using Terraria.Chat;
using Terraria.GameContent.UI.Chat;
using Terraria.Initializers;
using Terraria.Localization;

namespace AQOL.Common.Mono.Detours;

internal class NewTextHijackEdit : Modification
{
    public bool PlayerChatting = false;

    public override void Load(Mod mod)
    {
        On_RemadeChatMonitor.AddNewMessage += HijackNewMessage;
        On_ChatCommandProcessor.CreateOutgoingMessage += PlayerIsChatting;
        On_Main.DoUpdate_HandleChat += UnsetChatting;
        On_AchievementInitializer.OnAchievementCompleted += HijackAchievement;
    }

    private void HijackAchievement(On_AchievementInitializer.orig_OnAchievementCompleted orig, Terraria.Achievements.Achievement achievement)
    {
        PlayerChatting = true;
        orig(achievement);
        PlayerChatting = false;
    }

    private void UnsetChatting(On_Main.orig_DoUpdate_HandleChat orig)
    {
        orig();
        PlayerChatting = false;
    }

    private ChatMessage PlayerIsChatting(On_ChatCommandProcessor.orig_CreateOutgoingMessage orig, ChatCommandProcessor self, string text)
    {
        PlayerChatting = true;
        return orig(self, text);
    }

    private void HijackNewMessage(On_RemadeChatMonitor.orig_AddNewMessage orig, RemadeChatMonitor self, string text, Color color, int widthLimitInPixels)
    {
        if (PlayerChatting)
            orig(self, text, color, widthLimitInPixels);
        else
        {
            float xVel = 0;

            if (BossAnnouncementDetour.SpawningBoss)
            {
                string key = BossAnnouncementDetour.GetKey(BossAnnouncementDetour.SpawnBossId);

                if (key != string.Empty)
                {
                    text = Language.GetTextValue(key);
                    xVel = Main.LocalPlayer.direction * 18;
                }
            }

            AdvancedPopupRequest request = default;
            request.Text = text;
            request.Color = color;
            request.DurationInFrames = 240 + (2 * text.Length);
            request.Velocity = new Vector2(xVel, -1);
            PopupText.NewText(request, Main.LocalPlayer.Top);
        }
    }
}

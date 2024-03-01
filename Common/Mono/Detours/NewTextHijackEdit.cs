using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.UI.Chat;
using Terraria.ModLoader;

namespace AQOL.Common.Mono.Detours;

internal class NewTextHijackEdit : Modification
{
    public bool PlayerChatting = false;

    public override void Load(Mod mod)
    {
        On_RemadeChatMonitor.AddNewMessage += HijackNewMessage;
        On_ChatCommandProcessor.CreateOutgoingMessage += PlayerIsChatting;
        On_Main.DoUpdate_HandleChat += UnsetChatting;
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
            AdvancedPopupRequest request = default;
            request.Text = text;
            request.Color = color;
            request.DurationInFrames = 240 + (2 * text.Length);
            request.Velocity = new Vector2(0, -1);
            PopupText.NewText(request, Main.LocalPlayer.Top);
        }
    }
}

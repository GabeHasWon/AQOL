using Microsoft.Xna.Framework.Graphics;

namespace AQOL.Content.NPCs;

internal class EmptyNPC : ModNPC
{
    public override string Texture => "Terraria/Images/NPC_0";

    public override bool PreAI()
    {
        NPC.active = false;
        return false;
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) => false;
}

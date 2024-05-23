using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace AQOL.Content.NPCs.Slimes;

internal abstract partial class BaseSlime : ModNPC
{
    public override string Texture => "AQOL/Content/NPCs/Slimes/Slime";

    protected abstract Color SlimeColor { get; }
    protected virtual int CopyType => NPCID.BlueSlime;
    protected virtual float Scale => 1f;

    private int _storedItem = 0;

    public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<AQOLServerConfig>().SlimeReplacement;
    public override void SetStaticDefaults() => Main.npcFrameCount[Type] = 2;

    public sealed override void SetDefaults()
    {
        NPC.CloneDefaults(CopyType);
        NPC.color = SlimeColor with { A = 50 };
        NPC.Opacity = 0.5f;
        NPC.scale = Main.rand.NextFloat(0.8f, 1.2f) * Scale;
        NPC.Size = new Vector2(32, 24);

        AIType = CopyType;
        AnimationType = NPCID.BlueSlime;
        Defaults();

        _storedItem = SlimeStorageDatabase.DetermineItem(false); 
    }

    public virtual void Defaults() { }

    public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.3f);
        NPC.damage = (int)(NPC.damage * 0.2f);
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) => SpawnConditions(spawnInfo);
    public abstract float SpawnConditions(NPCSpawnInfo spawnInfo);

    public override void HitEffect(NPC.HitInfo hit)
    {
        for (int i = 0; i < 6; ++i)
            Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.t_Slime, hit.HitDirection, 0, 0, SlimeColor with { A = 50 }, Main.rand.NextFloat(1, 1.5f));

        if (NPC.life <= 0 && Main.netMode != NetmodeID.MultiplayerClient && _storedItem != -1)
            Item.NewItem(NPC.GetSource_Death(), NPC.Hitbox, _storedItem, 1);
    }

    public override void SendExtraAI(BinaryWriter writer) => writer.Write(_storedItem);
    public override void ReceiveExtraAI(BinaryReader reader) => _storedItem = reader.ReadInt32();
    public override void ModifyNPCLoot(NPCLoot npcLoot) => npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 1, 4));

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        if (_storedItem == -1)
            return true;

        Main.instance.LoadItem(_storedItem);
        Vector2 pos = NPC.Center - screenPos + new Vector2(0, NPC.frame.Y > 0 ? 0 : 2);
        Main.DrawItemIcon(spriteBatch, ContentSamples.ItemsByType[_storedItem], pos, Lighting.GetColor(NPC.Center.ToTileCoordinates()), NPC.height * 0.6f);

        return true;
    }
}

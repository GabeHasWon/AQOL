using Terraria.GameContent.Bestiary;

namespace AQOL.Content.NPCs.Enemies.Caverns;

public class Eel : ModNPC
{
    public override void SetStaticDefaults() => Main.npcFrameCount[Type] = 6;

    public override void SetDefaults()
    {
        NPC.width = 40;
        NPC.height = 14;
        NPC.damage = 20;
        NPC.defense = 0;
        NPC.lifeMax = 22;
        NPC.knockBackResist = 1f;
        NPC.value = Item.buyPrice(copper: 40);
        NPC.aiStyle = NPCAIStyleID.Piranha;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.noGravity = true;

        AIType = NPCID.Piranha;
        AnimationType = NPCID.Piranha;
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "Caverns");
    public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneNormalCaverns && spawnInfo.PlayerInTown && spawnInfo.Water ? 0.6f : 0f;
    public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo) => target.AddBuff(BuffID.Poisoned, 240);

    public override void HitEffect(NPC.HitInfo hit)
    {
        for (int i = 0; i < 2; ++i)
            Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, 0);

        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            for (int i = 0; i < 2; ++i)
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, 0);

            for (int i = 0; i < 2; ++i)
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Eel" + i).Type);
        }
    }
}
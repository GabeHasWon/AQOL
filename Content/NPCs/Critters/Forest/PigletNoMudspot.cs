using System;
using Terraria.GameContent.Bestiary;

namespace AQOL.Content.NPCs.Critters.Forest;

public class PigletNoMudspot : CritterNPC
{
    private ref float Timer => ref NPC.ai[0];
    private ref float StartWalking => ref NPC.ai[1];

    public override void SetStaticDefaults()
    {
        Main.npcCatchable[Type] = true;
        Main.npcFrameCount[Type] = 6;

        NPCID.Sets.CountsAsCritter[Type] = true;
    }

    public override void SetDefaults()
    {
        NPC.width = 40;
        NPC.height = 26;
        NPC.damage = 0;
        NPC.defense = 0;
        NPC.lifeMax = 5;
        NPC.knockBackResist = 0.7f;
        NPC.value = 0f;
        NPC.aiStyle = -1;
        NPC.dontCountMe = true;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath24;

        if (Main.netMode != NetmodeID.Server)
            NPC.DeathSound = SoundID.NPCDeath24 with { PitchRange = (0.8f, 1f) };

        NPC.catchItem = (short)ItemType;

        AIType = NPCID.Goldfish;
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "Surface");
    public override float SpawnChance(NPCSpawnInfo spawnInfo)
        => spawnInfo.Player.ZonePurity && Main.dayTime && !spawnInfo.Sky && spawnInfo.SpawnTileY < Main.worldSurface ? (spawnInfo.PlayerInTown ? 0.25f : 0.05f) : 0f;

    public override void AI()
    {
        if (StartWalking == 0)
            UpdateWalkTime();

        Timer++;

        NPC nearestHostile = FindClosestHostileNPC();

        if (nearestHostile is null)
        {
            if (Timer > StartWalking)
            {
                NPC.velocity.X = NPC.direction * 1.2f;

                if (Timer > StartWalking + 180)
                    UpdateWalkTime();
            }
            else
                NPC.velocity.X = 0;
        }
        else
        {
            int dir = Math.Sign(NPC.Center.X - nearestHostile.Center.X);
            NPC.spriteDirection = NPC.direction = dir;
            NPC.velocity.X = dir * 1.8f;
        }

        Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
    }

    private NPC FindClosestHostileNPC()
    {
        int closest = -1;
        
        for (int i = 0; i < Main.maxNPCs; ++i)
        {
            NPC npc = Main.npc[i];
            float dist = npc.DistanceSQ(NPC.Center);

            if (npc.CanBeChasedBy() && (closest == -1 || dist > npc.DistanceSQ(NPC.Center)) && dist < 450 * 450)
                closest = i;
        }

        return closest == -1 ? null : Main.npc[closest];
    }

    private void UpdateWalkTime()
    {
        StartWalking = Main.rand.Next(180, 360);
        Timer = 0;
        NPC.spriteDirection = NPC.direction = Main.rand.NextBool() ? -1 : 1;
        NPC.netUpdate = true;
    }

    public override void FindFrame(int frameHeight)
    {
        float x = NPC.IsABestiaryIconDummy ? 0.8f : Math.Abs(NPC.velocity.X);

        if (x > 0)
        {
            NPC.frameCounter += 0.2f * x;
            NPC.frame.Y = (int)(NPC.frameCounter % 6) * frameHeight;
        }
        else
            NPC.frame.Y = 0;
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, hit.HitDirection, 0);

        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            for (int i = 0; i < 3; ++i)
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Piglet" + i).Type);
        }
    }
}
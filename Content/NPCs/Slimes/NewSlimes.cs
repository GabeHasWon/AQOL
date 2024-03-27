using System;
using System.Reflection.Metadata;
using Terraria.GameContent.Bestiary;

namespace AQOL.Content.NPCs.Slimes;

internal class GreenSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Green;
    protected override int CopyType => NPCID.BlueSlime;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo)
        => spawnInfo.Player.ZonePurity && Main.dayTime && !spawnInfo.PlayerInTown && !spawnInfo.Sky && spawnInfo.SpawnTileY < Main.worldSurface ? 0.7f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "Surface DayTime");
}

internal class BlueSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Blue;

    private float _fadeIn = 0f;

    public override bool PreAI()
    {
        if (_fadeIn < 1)
        {
            _fadeIn += 0.025f;
            NPC.Opacity = _fadeIn * 0.5f;
        }

        return true;
    }

    public override float SpawnConditions(NPCSpawnInfo spawnInfo)
        => spawnInfo.Player.ZonePurity && Main.dayTime && !spawnInfo.PlayerInTown && !spawnInfo.Sky && spawnInfo.SpawnTileY < Main.worldSurface ? 0.8f : 0f;

    public override int SpawnNPC(int tileX, int tileY)
    {
        TryMoveSpawnAbove(ref tileX, ref tileY); // Moves blue slimes up rarely
        return base.SpawnNPC(tileX, tileY);
    }

    private void TryMoveSpawnAbove(ref int tileX, ref int tileY)
    {
        int height = (int)(Main.screenHeight / 16f);
        int offset = Main.rand.Next(height - 10, height);

        if (!Collision.SolidCollision(new Vector2(tileX, tileY - offset).ToWorldCoordinates(), NPC.width, NPC.height) && Main.rand.NextBool(4))
        {
            float xDist = float.MaxValue;

            if (Main.netMode == NetmodeID.SinglePlayer)
                tileX = (int)(Main.LocalPlayer.Center.X / 16f);
            else
            {
                for (int i = 0; i < Main.maxPlayers; ++i)
                {
                    Player plr = Main.player[i];

                    if (plr.active && !plr.dead)
                    {
                        float dist = MathF.Abs(tileX * 16 - plr.Center.X);

                        if (dist < xDist)
                        {
                            tileX = (int)(plr.Center.X / 16f);
                            xDist = dist;
                        }
                    }
                }
            }

            tileY -= offset;
        }
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "Surface DayTime");
}

internal class RedSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Red;
    protected override int CopyType => NPCID.RedSlime;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneCrimson && Main.dayTime && !spawnInfo.PlayerInTown ? 0.8f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "TheCrimson DayTime");
}

internal class PurpleSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Lerp(Color.Purple, Color.White, 0.2f);
    protected override int CopyType => NPCID.PurpleSlime;
    protected override float Scale => 1.2f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneCorrupt && Main.dayTime && !spawnInfo.PlayerInTown ? 0.8f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "TheCorruption DayTime");
}

internal class YellowSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Yellow;
    protected override int CopyType => NPCID.YellowSlime;
    protected override float Scale => 1.2f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneDesert && Main.dayTime && !spawnInfo.PlayerInTown ? 0.8f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "Desert DayTime");
}

internal class IndigoSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Indigo;
    protected override int CopyType => NPCID.YellowSlime;
    protected override float Scale => 1f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneNormalCaverns && !spawnInfo.PlayerInTown ? 0.8f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "Caverns");
}

internal class BlackSplitterSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Black;
    protected override int CopyType => NPCID.MotherSlime;
    protected override float Scale => 2f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneNormalCaverns && !spawnInfo.PlayerInTown ? 0.8f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "Caverns");

    public override void OnKill()
    {
        for (int i = 0; i < 3; ++i)
            NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<BlackMiniSlime>());
    }
}

internal class BlackMiniSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Black;
    protected override int CopyType => NPCID.BabySlime;
    protected override float Scale => 0.67f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "Caverns");
}

internal class OrangeSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Orange;
    protected override int CopyType => NPCID.LavaSlime;
    protected override float Scale => 1f;

    public override void Defaults() => AIType = NPCID.BlueSlime;
    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneUnderworldHeight && !spawnInfo.PlayerInTown ? 0.8f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "TheUnderworld");
}

internal class VioletSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Violet;
    protected override int CopyType => NPCID.DungeonSlime;
    protected override float Scale => 1.5f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneDungeon && !spawnInfo.PlayerInTown ? 0.8f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "TheDungeon");
}

internal class PinkSlime : BaseSlime
{
    protected override Color SlimeColor => Color.HotPink;
    protected override int CopyType => NPCID.Pinky;
    protected override float Scale => 0.4f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneBeach && !spawnInfo.PlayerInTown ? 0.5f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "Ocean");
}

internal class BlueRainSlime : BaseSlime
{
    protected override Color SlimeColor => Color.BlueViolet;
    protected override int CopyType => NPCID.UmbrellaSlime;
    protected override float Scale => 1.2f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) 
        => spawnInfo.Player.ZonePurity && Main.IsItRaining && !spawnInfo.PlayerInTown && spawnInfo.SpawnTileX < Main.worldSurface ? 0.5f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "Surface Rain");
}

internal class Toxslime : BaseSlime
{
    protected override Color SlimeColor => Color.LightGreen;
    protected override int CopyType => NPCID.ToxicSludge;
    protected override float Scale => 1.8f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneNormalCaverns && Main.hardMode && !spawnInfo.PlayerInTown ? 0.8f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "Caverns");
}

internal class GreySlime : BaseSlime
{
    protected override Color SlimeColor => Color.Gray;
    protected override int CopyType => NPCID.YellowSlime;
    protected override float Scale => 1.1f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneNormalCaverns && !spawnInfo.PlayerInTown ? 1f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "Caverns");
}

internal class WhiteSlime : BaseSlime
{
    protected override Color SlimeColor => Color.White;
    protected override int CopyType => NPCID.YellowSlime;
    protected override float Scale => 1.1f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Sky && !spawnInfo.PlayerInTown ? 1f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "Sky");
}

internal class AquaSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Aqua;
    protected override int CopyType => NPCID.YellowSlime;
    protected override float Scale => 1f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneBeach && !spawnInfo.PlayerInTown ? 1f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "Ocean");
}

internal class LightBlueSlime : BaseSlime
{
    protected override Color SlimeColor => Color.LightBlue;
    protected override int CopyType => NPCID.IceSlime;
    protected override float Scale => 1f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneSnow && !spawnInfo.PlayerInTown ? 1f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "Snow");
}

internal class SpikedLightBlueSlime : BaseSlime
{
    public override string Texture => "AQOL/Content/NPCs/Slimes/SpikedSlime";
    protected override Color SlimeColor => Color.LightBlue;
    protected override int CopyType => NPCID.SpikedIceSlime;
    protected override float Scale => 1f;

    public override void Defaults()
    {
        AnimationType = NPCID.SpikedIceSlime;
    }

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneSnow && !spawnInfo.PlayerInTown && spawnInfo.Player.ZoneRockLayerHeight ? 0.4f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "UndergroundSnow");
}

internal class LimeSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Lime;
    protected override int CopyType => NPCID.JungleSlime;
    protected override float Scale => 1f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneJungle && !spawnInfo.PlayerInTown ? 1f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "Jungle");
}

internal class SpikedLimeSlime : BaseSlime
{
    public override string Texture => "AQOL/Content/NPCs/Slimes/SpikedSlime";
    protected override Color SlimeColor => Color.Lime;
    protected override int CopyType => NPCID.SpikedJungleSlime;
    protected override float Scale => 1f;

    public override void Defaults() => AnimationType = NPCID.SpikedJungleSlime;
    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneJungle && !spawnInfo.PlayerInTown && spawnInfo.Player.ZoneRockLayerHeight ? 0.4f : 0f;
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) => bestiaryEntry.AddInfo(this, "UndergroundJungle");
}

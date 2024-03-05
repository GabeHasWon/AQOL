namespace AQOL.Content.NPCs.Slimes;

internal class GreenSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Green;
    protected override int CopyType => NPCID.BlueSlime;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo)
        => spawnInfo.Player.ZonePurity && Main.dayTime && !spawnInfo.PlayerInTown && !spawnInfo.Sky && spawnInfo.SpawnTileY < Main.worldSurface ? 0.7f : 0f;
}

internal class BlueSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Blue;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo)
        => spawnInfo.Player.ZonePurity && Main.dayTime && !spawnInfo.PlayerInTown && !spawnInfo.Sky && spawnInfo.SpawnTileY < Main.worldSurface ? 0.8f : 0f;
}

internal class RedSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Red;
    protected override int CopyType => NPCID.RedSlime;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneCrimson && Main.dayTime && !spawnInfo.PlayerInTown ? 0.8f : 0f;
}

internal class PurpleSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Lerp(Color.Purple, Color.White, 0.2f);
    protected override int CopyType => NPCID.PurpleSlime;
    protected override float Scale => 1.2f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneCorrupt && Main.dayTime && !spawnInfo.PlayerInTown ? 0.8f : 0f;
}

internal class YellowSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Yellow;
    protected override int CopyType => NPCID.YellowSlime;
    protected override float Scale => 1.2f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneDesert && Main.dayTime && !spawnInfo.PlayerInTown ? 0.8f : 0f;
}

internal class IndigoSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Indigo;
    protected override int CopyType => NPCID.YellowSlime;
    protected override float Scale => 1f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneNormalCaverns && !spawnInfo.PlayerInTown ? 0.8f : 0f;
}

internal class BlackSplitterSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Black;
    protected override int CopyType => NPCID.MotherSlime;
    protected override float Scale => 2f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneNormalCaverns && !spawnInfo.PlayerInTown ? 0.8f : 0f;

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
}

internal class OrangeSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Orange;
    protected override int CopyType => NPCID.LavaSlime;
    protected override float Scale => 1f;

    public override void Defaults() => AIType = NPCID.BlueSlime;
    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneUnderworldHeight && !spawnInfo.PlayerInTown ? 0.8f : 0f;
}

internal class VioletSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Violet;
    protected override int CopyType => NPCID.DungeonSlime;
    protected override float Scale => 1.5f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneDungeon && !spawnInfo.PlayerInTown ? 0.8f : 0f;
}

internal class PinkSlime : BaseSlime
{
    protected override Color SlimeColor => Color.HotPink;
    protected override int CopyType => NPCID.Pinky;
    protected override float Scale => 0.4f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneBeach && !spawnInfo.PlayerInTown ? 0.5f : 0f;
}

internal class BlueRainSlime : BaseSlime
{
    protected override Color SlimeColor => Color.BlueViolet;
    protected override int CopyType => NPCID.UmbrellaSlime;
    protected override float Scale => 1.2f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) 
        => spawnInfo.Player.ZonePurity && Main.IsItRaining && !spawnInfo.PlayerInTown && spawnInfo.SpawnTileX < Main.worldSurface ? 0.5f : 0f;
}

internal class Toxslime : BaseSlime
{
    protected override Color SlimeColor => Color.LightGreen;
    protected override int CopyType => NPCID.ToxicSludge;
    protected override float Scale => 1.8f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneNormalCaverns && Main.hardMode && !spawnInfo.PlayerInTown ? 0.8f : 0f;
}

internal class GreySlime : BaseSlime
{
    protected override Color SlimeColor => Color.Gray;
    protected override int CopyType => NPCID.YellowSlime;
    protected override float Scale => 1.1f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneNormalCaverns && !spawnInfo.PlayerInTown ? 1f : 0f;
}

internal class WhiteSlime : BaseSlime
{
    protected override Color SlimeColor => Color.White;
    protected override int CopyType => NPCID.YellowSlime;
    protected override float Scale => 1.1f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Sky && !spawnInfo.PlayerInTown ? 1f : 0f;
}

internal class AquaSlime : BaseSlime
{
    protected override Color SlimeColor => Color.Aqua;
    protected override int CopyType => NPCID.YellowSlime;
    protected override float Scale => 1f;

    public override float SpawnConditions(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneBeach && !spawnInfo.PlayerInTown ? 1f : 0f;
}

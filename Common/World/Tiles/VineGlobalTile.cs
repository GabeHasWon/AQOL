namespace AQOL.Common.World.Tiles;

internal class VineGlobalTile : GlobalTile
{
    public static int[] VineIDs = [TileID.Vines, TileID.AshVines, TileID.CorruptVines, TileID.CrimsonVines, TileID.HallowedVines, TileID.JungleVines, TileID.MushroomVines, TileID.VineFlowers];

    public override void Load()
    {
        On_Player.Update += HijackTileCut;
    }

    private void HijackTileCut(On_Player.orig_Update orig, Player self, int i)
    {
        if (self.cordage)
            foreach (var id in VineIDs)
                Main.tileCut[id] = true;

        orig(self, i);

        foreach (var id in VineIDs)
            Main.tileCut[id] = false;
    }

    public override void SetStaticDefaults()
    {
        foreach (var id in VineIDs)
            Main.tileCut[id] = false;
    }
}

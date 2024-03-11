using Terraria.GameContent;
using Terraria.ObjectData;

namespace AQOL.Content.Tiles;

public class VeggieDecor : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileCut[Type] = false;
        Main.tileNoFail[Type] = true;
        Main.tileMergeDirt[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
        TileObjectData.newTile.RandomStyleRange = 4;
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.Origin = new Point16(1, 1);
        TileObjectData.addTile(Type);

        DustType = DustID.Stone;
        HitSound = SoundID.Dig;

        RegisterItemDrop(Mod.Find<ModItem>("Carrot").Type, 0);
        RegisterItemDrop(Mod.Find<ModItem>("Potato").Type, 1);
        RegisterItemDrop(Mod.Find<ModItem>("Broccoli").Type, 2);
        RegisterItemDrop(Mod.Find<ModItem>("Cabbage").Type, 3);
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = 4;
}

public class VeggieDecorRubble : VeggieDecor
{
    public override string Texture => base.Texture.Replace("Rubble", "");

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        FlexibleTileWand.RubblePlacementLarge.AddVariations(ItemID.StoneBlock, Type, 0, 1, 2, 3);
        RegisterItemDrop(ItemID.StoneBlock);
        TileObjectData.GetTileData(Type, 0).RandomStyleRange = 0;
    }
}

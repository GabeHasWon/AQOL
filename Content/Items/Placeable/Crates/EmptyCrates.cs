using Terraria.ObjectData;

namespace AQOL.Content.Items.Placeable.Crates;

public class EmptyCrates : ModTile
{
    public override string Texture => $"Terraria/Images/Tiles_{TileID.FishingCrate}";

    public override void Load()
    {
        Mod.AddContent(new CrateItem(ItemID.WoodenCrate, ItemID.Wood, false, 0, "Wood"));
        Mod.AddContent(new CrateItem(ItemID.IronCrate, ItemID.IronBar, false, 1, "Iron"));
        Mod.AddContent(new CrateItem(ItemID.GoldenCrate, ItemID.GoldBar, false, 2, "Golden"));
        Mod.AddContent(new CrateItem(ItemID.CorruptFishingCrate, ItemID.DemoniteBar, false, 3, "Corrupt"));
        Mod.AddContent(new CrateItem(ItemID.CrimsonFishingCrate, ItemID.CrimtaneBar, false, 4, "Crimson"));
        Mod.AddContent(new CrateItem(ItemID.DungeonFishingCrate, ItemID.Bone, false, 5, "Dungeon"));
        Mod.AddContent(new CrateItem(ItemID.FloatingIslandFishingCrate, ItemID.SunplateBlock, false, 6, "Sky"));
        Mod.AddContent(new CrateItem(ItemID.HallowedFishingCrate, ItemID.CrystalShard, false, 7, "Hallowed"));
        Mod.AddContent(new CrateItem(ItemID.JungleFishingCrate, ItemID.RichMahogany, false, 8, "Jungle"));
        Mod.AddContent(new CrateItem(ItemID.WoodenCrateHard, ItemID.Pearlwood, true, 9, "Pearlwood"));
        Mod.AddContent(new CrateItem(ItemID.IronCrateHard, ItemID.MythrilBar, true, 10, "Mythril"));
        Mod.AddContent(new CrateItem(ItemID.GoldenCrateHard, ItemID.TitaniumBar, true, 11, "Titanium"));
        Mod.AddContent(new CrateItem(ItemID.CorruptFishingCrateHard, ItemID.DemoniteBar, true, 12, "Defiled"));
        Mod.AddContent(new CrateItem(ItemID.CrimsonFishingCrateHard, ItemID.CrimtaneBar, true, 13, "Hematic"));
        Mod.AddContent(new CrateItem(ItemID.DungeonFishingCrateHard, ItemID.Bone, true, 14, "Stockade"));
        Mod.AddContent(new CrateItem(ItemID.FloatingIslandFishingCrateHard, ItemID.SunplateBlock, true, 15, "Azure"));
        Mod.AddContent(new CrateItem(ItemID.HallowedFishingCrateHard, ItemID.CrystalShard, true, 16, "Divine"));
        Mod.AddContent(new CrateItem(ItemID.JungleFishingCrateHard, ItemID.RichMahogany, true, 17, "Bramble"));
        Mod.AddContent(new CrateItem(ItemID.FrozenCrate, ItemID.BorealWood, false, 18, "Frozen"));
        Mod.AddContent(new CrateItem(ItemID.FrozenCrateHard, ItemID.BorealWood, true, 19, "Boreal"));
        Mod.AddContent(new CrateItem(ItemID.OasisCrate, ItemID.DesertFossil, false, 20, "Oasis"));
        Mod.AddContent(new CrateItem(ItemID.OasisCrateHard, ItemID.DesertFossil, true, 21, "Mirage"));
        Mod.AddContent(new CrateItem(ItemID.LavaCrate, ItemID.Obsidian, false, 22, "Obsidian"));
        Mod.AddContent(new CrateItem(ItemID.LavaCrateHard, ItemID.Obsidian, true, 23, "Hellstone"));
        Mod.AddContent(new CrateItem(ItemID.OceanCrate, ItemID.Coral, false, 24, "Ocean"));
        Mod.AddContent(new CrateItem(ItemID.OceanCrateHard, ItemID.Coral, true, 25, "Seaside"));
    }

    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileTable[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileSolidTop[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.CoordinateHeights = [16, 18];
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.addTile(Type);

        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
        AddMapEntry(new Color(0.4f, 0.4f, 0.1f));

        DustType = DustID.WoodFurniture;
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
}

[Autoload(false)]
public class CrateItem : ModItem
{
    public override string Texture => $"Terraria/Images/Item_{VanillaCrateItemId}";
    public override string Name => $"Empty{CrateName}Crate";
    protected override bool CloneNewInstances => true;

    private int VanillaCrateItemId;
    private int MaterialType;
    private bool IsHardmode;
    private int Style;
    private string CrateName;


    public CrateItem(int itemId, int materialType, bool isHardmode, int style, string name)
    {
        VanillaCrateItemId = itemId;
        MaterialType = materialType;
        IsHardmode = isHardmode;
        Style = style;
        CrateName = name;
    }

    public override ModItem Clone(Item newEntity)
    {
        var entity = base.Clone(newEntity) as CrateItem;
        entity.VanillaCrateItemId = VanillaCrateItemId;
        entity.MaterialType = MaterialType;
        entity.IsHardmode = IsHardmode;
        entity.Style = Style;
        entity.CrateName = CrateName;
        return entity;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(VanillaCrateItemId);
        Item.createTile = ModContent.TileType<EmptyCrates>();
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe()
            .AddIngredient(MaterialType, 20);

        if (IsHardmode)
            recipe.AddIngredient(ItemID.SoulofLight, 3);

        recipe.Register();
    }
}

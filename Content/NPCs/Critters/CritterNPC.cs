namespace AQOL.Content.NPCs.Critters;

public abstract class CritterNPC : ModNPC
{
    protected int ItemType => Mod.Find<ModItem>(Name + "_Item").Type;

    public override void Load() => Mod.AddContent(new CritterItem(Name + "_Item", FullName, Texture));

    [Autoload(false)]
    internal class CritterItem(string name, string npcKey, string texture) : ModItem
    {
        public override string Name => _name;
        protected override bool CloneNewInstances => true;
        public override string Texture => _texturePath + "_Item";

        private string _name = name;
        private string _texturePath = texture;
        private string _npcKey = npcKey;

        public override ModItem Clone(Item newEntity)
        {
            var entity = base.Clone(newEntity) as CritterItem;
            entity._name = _name;
            entity._npcKey = _npcKey;
            entity._texturePath = _texturePath;
            return entity;
        }

        public override void SetStaticDefaults() => Item.ResearchUnlockCount = 3;

        public override void SetDefaults()
        {
            var modNPC = ModContent.Find<ModNPC>(_npcKey);

            Item.Size = modNPC.NPC.Size;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.damage = 0;
            Item.rare = ItemRarityID.White;
            Item.maxStack = Item.CommonMaxStack;
            Item.noUseGraphic = true;
            Item.noMelee = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.makeNPC = (short)modNPC.Type;
            Item.autoReuse = true;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player) => !Collision.SolidCollision(Main.MouseWorld - new Vector2(10), 20, 20) && 
            player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, TileReachCheckSettings.Simple);
    }
}

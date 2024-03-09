using Microsoft.Xna.Framework.Graphics;

namespace AQOL.Content.Items.Food;

[Autoload(false)]
internal class FoodItem(Vector2 size, string name) : ModItem
{
    public override string Name => _name;
    protected override bool CloneNewInstances => true;

    private Vector2 Size => _size;

    private Vector2 _size = size;
    private string _name = name;

    public override ModItem Clone(Item newEntity)
    {
        var clone = base.Clone(newEntity) as FoodItem;
        clone._size = _size;
        clone._name = _name;
        return clone;
    }

    public override void SetStaticDefaults()
    {
        ItemID.Sets.IsFood[Type] = true;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.Apple);
        Item.Size = Size;
    }

    public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
        Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;
        Vector2 offset = new Vector2(-4);
        spriteBatch.Draw(tex, position.ToPoint().ToVector2() + offset, new Rectangle(0, 0, Item.width, Item.height), drawColor, 0f, Item.Size / 3f, scale * 3, SpriteEffects.None, 0f);
        return false;
    }

    public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
    {
        Texture2D tex = ModContent.Request<Texture2D>(Texture).Value;
        spriteBatch.Draw(tex, Item.Center - Main.screenPosition, new Rectangle(0, 0, Item.width, Item.height), lightColor, rotation, Item.Size / 2f, scale, SpriteEffects.None, 0f);
        return false;
    }
}

internal class FoodLoader : ILoadable
{
    public void Load(Mod mod)
    {
        mod.AddContent(new FoodItem(new(32, 20), "Lime"));
        mod.AddContent(new FoodItem(new(28), "Orange"));
        mod.AddContent(new FoodItem(new(32, 34), "Pear"));
        mod.AddContent(new FoodItem(new(24, 18), "Avocado"));
        mod.AddContent(new FoodItem(new(28), "Raspberries"));
        mod.AddContent(new FoodItem(new(32), "Blackberry"));
        mod.AddContent(new FoodItem(new(42, 40), "Carrot"));
        mod.AddContent(new FoodItem(new(26, 24), "Potato"));
        mod.AddContent(new FoodItem(new(34), "Cabbage"));
        mod.AddContent(new FoodItem(new(34, 36), "Broccoli"));
    }

    public void Unload()
    {
    }
}

using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria.Enums;

namespace AQOL.Common.World.Tiles;

internal class ShakeTreeGlobalTile : GlobalTile
{
    public override void Load()
    {
        IL_WorldGen.ShakeTree += ModifyForestTreeFruit;
    }

    private void ModifyForestTreeFruit(ILContext il)
    {
        ILCursor c = new(il);

        c.GotoNext(MoveType.After, x => x.MatchCall(typeof(PlantLoader), nameof(PlantLoader.ShakeTree)));

        c.Emit(OpCodes.Ldloc_0); // x
        c.Emit(OpCodes.Ldloc_1); // y
        c.Emit(OpCodes.Ldloc_3); // treeType
        c.EmitDelegate(HookShakeTree);
    }

    private static void HookShakeTree(int x, int y, TreeTypes treeType)
    {
        int type = Main.tile[x, y].TileType;

        if (type > TileID.Count)
            return;

        SpamBirds(x, y, treeType);

        if (treeType == TreeTypes.Forest)
        {
            if (!WorldGen.genRand.NextBool())
                return;

            static int Food(string name) => ModLoader.GetMod("AQOL").Find<ModItem>(name).Type;

            Item.NewItem(Type: WorldGen.genRand.Next(25) switch
            {
                0 => ItemID.Apple,
                1 => ItemID.Apricot,
                2 => ItemID.Lemon,
                3 => ItemID.Grapefruit,
                4 => Food("Lime"),
                5 => Food("Avocado"),
                6 => Food("Pear"),
                7 => Food("Orange"),
                8 => Food("Raspberries"),
                9 => Food("Blackberry"),
                10 => ItemID.BloodOrange,
                11 => ItemID.Rambutan,
                12 => ItemID.Mango,
                13 => ItemID.Banana,
                14 => ItemID.Cherry,
                15 => ItemID.Elderberry,
                16 => ItemID.Starfruit,
                17 => ItemID.Pineapple,
                18 => ItemID.SpicyPepper,
                19 => ItemID.Coconut,
                20 => ItemID.Plum,
                21 => ItemID.Pomegranate,
                22 => ItemID.BlackCurrant,
                23 => ItemID.Dragonfruit,
                _ => ItemID.Peach,
            }, source: new EntitySource_ShakeTree(x, y), X: x * 16, Y: y * 16, Width: 16, Height: 16);
        }
    }

    private static void SpamBirds(int x, int y, TreeTypes treeType)
    {
        bool isPalm = treeType == TreeTypes.Palm || treeType == TreeTypes.PalmCorrupt || treeType == TreeTypes.PalmCrimson || treeType == TreeTypes.PalmHallowed;

        if (WorldGen.genRand.NextBool(3) && isPalm && !WorldGen.IsPalmOasisTree(x))
            NPC.NewNPC(new EntitySource_ShakeTree(x, y), x * 16, y * 16, NPCID.Seagull2);

        if (WorldGen.genRand.NextBool(10) && (treeType == TreeTypes.Forest || treeType == TreeTypes.Hallowed)) // Spams a bunch of birds not just one
        {
            for (int i = 0; i < 5; i++)
            {
                Point point = new(x + Main.rand.Next(-2, 2), y - 1 + Main.rand.Next(-2, 2));
                int type = (Player.GetClosestRollLuck(x, y, NPC.goldCritterChance) != 0f) ? Main.rand.NextFromList([NPCID.Bird, NPCID.BirdBlue, NPCID.BirdRed]) : NPCID.GoldBird;
                SpawnBird(point.X, point.Y, type);
            }
        }

        if (WorldGen.genRand.NextBool(6) && treeType == TreeTypes.Jungle && Main.dayTime)
            SpawnBird(x, y, [NPCID.BlueMacaw, NPCID.ScarletMacaw, NPCID.Toucan, NPCID.GrayCockatiel, NPCID.YellowCockatiel]);
    }

    private static NPC SpawnBird(int x, int y, short[] ids) => SpawnBird(x, y, Main.rand.NextFromList(ids));

    private static NPC SpawnBird(int x, int y, int id)
    {
        NPC bird = Main.npc[NPC.NewNPC(new EntitySource_ShakeTree(x, y), x * 16, y * 16, id)];
        bird.velocity.Y = 1f;
        bird.netUpdate = true;
        return bird;
    }
}

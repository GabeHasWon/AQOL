using Terraria.GameContent.ItemDropRules;

namespace AQOL.Common.World.NPCs;

internal class LootNPCGlobal : GlobalNPC
{
    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        if (npc.type == NPCID.ArmoredSkeleton) // Remove Beam Sword from Armored Skeleton
            npcLoot.RemoveWhere(x => x is CommonDrop common && common.itemId == ItemID.BeamSword);
        else if (npc.type == NPCID.AngryTrapper) // Remove Uzi from Angry Trapper
            npcLoot.RemoveWhere(x => x is CommonDrop common && common.itemId == ItemID.Uzi);
        else if (npc.type == NPCID.SkeletronPrime) // Add Orange Zapinator to Skele Prime
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.ZapinatorOrange));
        else if (npc.type == NPCID.TheDestroyer) // Add Beam Sword to Destroyer
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.BeamSword));
        else if (npc.type is NPCID.Retinazer or NPCID.Spazmatism) // Add Uzi to Twins
        {
            bool needsBreak = false;

            foreach (var rule in npcLoot.Get(false))
            {
                if (rule is LeadingConditionRule cond && cond.condition is Conditions.MissingTwin)
                {
                    foreach (var subCond in cond.ChainedRules)
                    {
                        if (subCond.RuleToChain is LeadingConditionRule notExpertCond && notExpertCond.condition is Conditions.NotExpert)
                        {
                            notExpertCond.OnSuccess(ItemDropRule.Common(ItemID.Uzi));
                            needsBreak = true;
                            break;
                        }
                    }

                    if (needsBreak)
                        break;
                }
            }
        }
        else if (npc.type is NPCID.Bird or NPCID.BirdBlue or NPCID.BirdRed or NPCID.GoldBird)
            npcLoot.Add(ItemDropRule.Common(ItemID.Feather, 1, 1, 2));
    }
}

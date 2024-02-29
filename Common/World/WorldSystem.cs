using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AQOL.Common.World;

internal class WorldSystem : ModSystem
{
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
    {
        var content = GenStep.Steps;

        foreach (var step in content)
        {
            tasks.Insert(step.GenIndex(tasks), new PassLegacy(step.GenName, step.Generation));
        }
    }
}

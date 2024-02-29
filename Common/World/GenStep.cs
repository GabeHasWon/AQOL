using System.Collections.Generic;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AQOL.Common.World;

public abstract class GenStep : ILoadable
{
    public static List<GenStep> Steps = [];

    public abstract string GenName { get; }

    public virtual bool ShouldGen() => true;
    public abstract int GenIndex(List<GenPass> passes);
    public abstract void Generation(GenerationProgress progress, GameConfiguration config);

    public virtual void Load(Mod mod) => Steps.Add(this);
    public virtual void Unload() { }
}

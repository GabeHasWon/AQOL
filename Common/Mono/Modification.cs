using Terraria.ModLoader;

namespace AQOL.Common.Mono;

internal abstract class Modification : ILoadable
{
    public abstract void Load(Mod mod);
    public virtual void Unload() { }
}

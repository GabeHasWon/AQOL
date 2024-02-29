using Terraria.ModLoader;

namespace AQOL.Common.IL;

internal abstract class ILEdit : ILoadable
{
    public abstract void Load(Mod mod);
    public virtual void Unload() { }
}

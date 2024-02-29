using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace AQOL;

class AQUOLClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    /// <summary>
    /// Tracks whether worldgen-spawned NPCs should be shimmered (if they can be).<br/>
    /// This is set in
    /// </summary>
    [DefaultValue(false)]
    public bool ShimmeredWorldNPCs;
}
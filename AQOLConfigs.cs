using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace AQOL;

class AQOLClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    /// <summary>
    /// Tracks whether worldgen-spawned NPCs should be shimmered (if they can be).<br/>
    /// </summary>
    [DefaultValue(false)]
    public bool ShimmeredWorldNPCs;

    /// <summary>
    /// If true, chat will instead be spawned in-world as popup text.
    /// </summary>
    [DefaultValue(true)]
    public bool ChatRework;
}

class AQOLServerConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    /// <summary>
    /// Tracks whether the slime overhaul should be in place.
    /// </summary>
    [DefaultValue(true)]
    public bool SlimeReplacement;
}
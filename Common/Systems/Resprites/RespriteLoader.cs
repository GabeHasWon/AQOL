using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Linq;
using Terraria.GameContent;

namespace AQOL.Common.Systems.Resprites;

internal class RespriteLoader : ILoadable
{
    public void Load(Mod mod)
    {
        var files = mod.GetFileNames().Where(x => x.StartsWith("Common/Systems/Resprites/Textures/"));

        foreach (var file in files)
        {
            Asset<Texture2D> tex = ModContent.Request<Texture2D>("AQOL/" + file[..file.IndexOf(".rawimg")]);

            if (file.Contains("Item"))
                Resprite.ApplyFromArray("Item", int.Parse(file[(file.IndexOf('_') + 1)..file.IndexOf('.')]), tex);
        }
        
        Resprite.ApplyFromArray("ArmorHead", ArmorIDs.Head.CactusHelmet, "Armor_Head_70");
        Resprite.ApplyFromArray("ArmorLeg", ArmorIDs.Legs.CactusLeggings, "Armor_Legs_42");
        Resprite.ApplyFromArray("ArmorBodyComposite", ArmorIDs.Body.CactusBreastplate, "Armor_46");
    }

    public void Unload() => Resprite.Unapply();
}

using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.Linq;

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
    }

    public void Unload()
    {
        Resprite.Unapply();
    }
}

using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.GameContent;

namespace AQOL.Common.Systems.Resprites;

internal class Resprite
{
    const string RespriteDirectory = "AQOL/Common/Systems/Resprites/Textures/";

    private static readonly List<Resprite> resprites = [];

    private readonly string _reference;
    private readonly int? _index;
    private readonly Asset<Texture2D> _asset;

    private Asset<Texture2D> _oldAsset;

    private Resprite(string reference, int? index, Asset<Texture2D> asset)
    {
        _reference = reference;
        _index = index;
        _asset = asset;
    }

    public static Resprite ApplyFromArray(string reference, int index, Asset<Texture2D> texture)
    {
        Resprite resprite = new(reference, index, texture);
        resprites.Add(resprite);
        resprite.Apply();
        return resprite;
    }

    public static Resprite ApplyFromArray(string refer, int index, string path) => ApplyFromArray(refer, index, ModContent.Request<Texture2D>(RespriteDirectory + path));

    /// <summary>
    /// Assumes name to be <c>refer + "_" + index</c>.
    /// </summary>
    public static Resprite ApplyFromArray(string refer, int index) => ApplyFromArray(refer, index, ModContent.Request<Texture2D>(RespriteDirectory + refer + "_" + index));

    public void Apply()
    {
        var field = typeof(TextureAssets).GetField(_reference);

        if (!_index.HasValue)
        {
            _oldAsset = field.GetValue(null) as Asset<Texture2D>;
            field.SetValue(null, _asset);
        }
        else
        {
            var array = field.GetValue(null) as Asset<Texture2D>[];
            _oldAsset = array[_index.Value];
            array[_index.Value] = _asset;
        }
    }

    private void UnapplyInstance()
    {
        var field = typeof(TextureAssets).GetField(_reference);

        if (!_index.HasValue)
            field.SetValue(null, _oldAsset);
        else
        {
            var array = field.GetValue(null) as Asset<Texture2D>[];
            array[_index.Value] = _oldAsset;
        }
    }

    public static void Unapply()
    {
        foreach (var item in resprites)
            item.UnapplyInstance();

        resprites.Clear();
    }
}

using AQOL.Common.Mono.Detours;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace AQOL.Common.Systems.UI;

internal class UIOreChoice : UIButton<string>
{
    private int CurrentId => WorldOreSelectionDetour.OreIds[_oreKey][_oreSlot];

    private readonly string _oreKey;

    private int _oreSlot = 0;
    private bool _isDefault = true;

    public UIOreChoice(string oreKey, float textScaleMax = 1, bool large = false) : base(Language.GetTextValue("Mods.AQOL.Random"), textScaleMax, large)
    {
        _isDefault = true;
        _oreKey = oreKey;

        UpdateValues();
    }

    public override void LeftClick(UIMouseEvent evt)
    {
        if (_isDefault)
            _isDefault = false;
        else
        {
            _oreSlot++;

            if (_oreSlot >= WorldOreSelectionDetour.OreIds[_oreKey].Count)
                _oreSlot = 0;
        }

        UpdateValues();
    }

    public override void RightClick(UIMouseEvent evt)
    {
        _isDefault = !_isDefault;
        UpdateValues();
    }

    private void UpdateValues()
    {
        bool hasName = TileID.Search.TryGetName(CurrentId, out string name);

        if (!hasName)
            name = CurrentId.ToString();

        WorldOreSelectionDetour.SelectedOreIds[_oreKey] = _isDefault ? null : CurrentId;

        var text = _isDefault ? Language.GetTextValue("Mods.AQOL.Random") : $"{name}";
        SetText(text);

        var size = FontAssets.ItemStack.Value.MeasureString(text);
        Width.Set(size.X + 16, 0);
        Height.Set(size.Y, 0);
        Recalculate();
    }
}

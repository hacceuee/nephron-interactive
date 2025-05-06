using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
[ExecuteInEditMode]
public class ColorSet_ToggleActive : MonoBehaviour
{
    private Toggle toggle;
    private ColorPalette colorPalette;

    public enum paletteColors
    {
        colorBackground,
        colorEmphasis,
        colorAccent,
        colorActiveDot,
        colorUnactiveDot,
        colorOutline,
        colorShadow,
    };
    public paletteColors targetColor;

    void Start()
    {
        if (colorPalette == null)
        {
            colorPalette = Resources.Load<ColorPalette>("Palette");
        }

        toggle = GetComponent<Toggle>();
        SetSelectedColor();
    }

    void Update()
    {
        SetSelectedColor();
    }


    void SetSelectedColor()
    {
        if (toggle == null || colorPalette == null) return;

        ColorBlock colors = toggle.colors;
        colors.selectedColor = colorPalette.GetColor((int)targetColor);
        toggle.colors = colors;
    }
}

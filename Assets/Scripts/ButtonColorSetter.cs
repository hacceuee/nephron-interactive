using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[ExecuteInEditMode]
public class ButtonColorSetter : MonoBehaviour
{
    private Button button;
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

        button = GetComponent<Button>();
        SetSelectedColor();
    }

    void Update()
    {
        SetSelectedColor();
    }


    void SetSelectedColor()
    {
        if (button == null || colorPalette == null) return;

        ColorBlock colors = button.colors;
        colors.selectedColor = colorPalette.GetColor((int)targetColor);
        button.colors = colors;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

[RequireComponent (typeof(Image))]
[ExecuteInEditMode]
public class ColorSetter : MonoBehaviour
{
    Image image;

    ColorPalette colorPalette; 
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

    // Start is called before the first frame update
    void Start()
    {
        if (colorPalette == null)
        {
            colorPalette = Resources.Load<ColorPalette>("Palette");
        }

        image = GetComponent < Image > ();
        SetColor();
    }

    // Update is called once per frame
    void Update()
    {
        SetColor();
    }

    void SetColor()
    {
        image.color = colorPalette.GetColor((int)targetColor); 
    }
}

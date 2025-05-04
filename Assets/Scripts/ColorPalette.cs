using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Palette", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class ColorPalette : ScriptableObject
{
    //public Dictionary<string, Color> ColorReference;
    public Color colorBackground;
    public Color colorEmphasis;
    public Color colorAccent;
    public Color colorActiveDot;
    public Color colorUnactiveDot;
    public Color colorOutline;
    public Color colorShadow;

// Don't forgor - if you add more colors here, you'll have to add another to the enum object in the ColorSetter

    public Color GetColor (int color) {
        switch (color) {
            case 0: return colorBackground;
            case 1: return colorEmphasis; 
            case 2: return colorAccent;
            case 3: return colorActiveDot;
            case 4: return colorUnactiveDot;
            case 5: return colorOutline;
            case 6: return colorShadow;
            default: return Color.red;
        };
    } 
}


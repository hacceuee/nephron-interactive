using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Palette", menuName = "UI/Color Palette")]
public class ColorPalette : ScriptableObject
{
    [System.Serializable]
    public class NamedColor
    {
        public string name;
        public Color color;
    }

    public List<NamedColor> colors = new List<NamedColor>();

    public Color GetColorByName(string colorName)
    {
        foreach (var nc in colors)
        {
            if (nc.name == colorName)
                return nc.color;
        }

        Debug.LogWarning($"Color '{colorName}' not found in palette.");
        return Color.magenta; // fallback
    }

    public string[] GetColorNames()
    {
        List<string> names = new List<string>();
        foreach (var nc in colors)
            names.Add(nc.name);

        return names.ToArray();
    }
}

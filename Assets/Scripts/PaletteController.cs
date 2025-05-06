using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class PaletteController : MonoBehaviour
{
    public ColorPalette palette;

    [System.Serializable]
    public class ColorApplication
    {
        public string colorName;

        public bool applyToImage = false;
        public bool applyToText = false;
        public bool applyToTextMeshPro = false;
        public bool applyToOutline = false;
        public bool applyToShadow = false;
        public bool applyToToggleSelected = false;
        public bool applyToToggleNormal = false;
    }

    private void OnEnable()
    {
        if (palette == null)
        {
            palette = Resources.Load<ColorPalette>("Palette");
        }
    }

    public ColorApplication[] applications;

    private void Update()
    {
        if (palette == null) return;

        foreach (var app in applications)
        {
            Color c = palette.GetColorByName(app.colorName);

            if (app.applyToImage)
            {
                var img = GetComponent<Image>();
                if (img) img.color = c;
            }

            if (app.applyToText)
            {
                var text = GetComponent<Text>();
                if (text) text.color = c;
            }

            if (app.applyToTextMeshPro)
            {
                var tmp = GetComponent<TextMeshProUGUI>();
                if (tmp) tmp.color = c;
            }

            // Handle Outline (must check before Shadow!)
            if (app.applyToOutline)
            {
                var outline = GetComponent<Outline>();
                if (outline) outline.effectColor = c;
            }


            if (app.applyToShadow)
            {
                // Find all Shadow components, but skip the Outline component
                Shadow[] shadows = GetComponents<Shadow>();

                foreach (var shadow in shadows)
                {
                    if (shadow is Outline) continue; // Skip Outline components

                    shadow.effectColor = c;
                    break;  // Apply to only the first Shadow component
                }
            }

            if (app.applyToToggleNormal)
            {
                var toggle = GetComponent<Toggle>();
                if (toggle)
                {
                    ColorBlock colors = toggle.colors;
                    colors.normalColor = c;
                    toggle.colors = colors;
                }
            }

            if (app.applyToToggleSelected)
            {
                var toggle = GetComponent<Toggle>();
                if (toggle)
                {
                    ColorBlock colors = toggle.colors;
                    colors.selectedColor = c;
                    toggle.colors = colors;
                }
            }
        }
    }
}

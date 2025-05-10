using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Toggle))]
public class ToggleFix : MonoBehaviour
{
    private Toggle toggle;
    private Color originalNormalColor;
    private bool hasStoredOriginalColor = false;

    void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleChanged);

        if (!hasStoredOriginalColor)
        {
            originalNormalColor = toggle.colors.normalColor;
            hasStoredOriginalColor = true;
        }

        UpdateColorBlock(toggle.isOn);
    }

    void OnToggleChanged(bool isOn)
    {
        UpdateColorBlock(isOn);
        if (!isOn && EventSystem.current != null && EventSystem.current.currentSelectedGameObject == gameObject)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    void UpdateColorBlock(bool isOn)
    {
        var toggleColors = toggle.colors;

        if (isOn)
        {
            // Set normalColor to match selectedColor
            toggleColors.normalColor = toggleColors.selectedColor;
        }
        else
        {
            // Restore original normalColor
            toggleColors.normalColor = originalNormalColor;
        }

        toggle.colors = toggleColors;
    }
}

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

        // Store original normalColor
        if (!hasStoredOriginalColor)
        {
            originalNormalColor = toggle.colors.normalColor;
            hasStoredOriginalColor = true;
        }

        // Sync initial state
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
        var cb = toggle.colors;

        if (isOn)
        {
            // Set normalColor to match selectedColor
            cb.normalColor = cb.selectedColor;
        }
        else
        {
            // Restore original normalColor
            cb.normalColor = originalNormalColor;
        }

        toggle.colors = cb;
    }

    void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnToggleChanged);
    }
}

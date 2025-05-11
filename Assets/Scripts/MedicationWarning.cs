using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MedicationWarning : MonoBehaviour
{
    [Header("Warning UI")]
    public GameObject warningIcon;          // The exclamation icon
    public GameObject warningDescription;   // The description panel

    public Button toggleButton;             // The button that will toggle the side effect panel

    private TextMeshProUGUI descriptionText;

    private void Awake()
    {
        if (warningIcon != null)
        {
            Transform descTransform = warningIcon.transform.Find("Side Effect BG Panel/Warning Description");
            if (descTransform != null)
            {
                descriptionText = descTransform.GetComponent<TextMeshProUGUI>();
            }
        }

        warningIcon?.SetActive(false);
        warningDescription?.SetActive(false);

        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleSideEffectPanel);
        }
    }

    public void SetWarning(Medication med)
    {
        if (med != null && med.hasWarning)
        {
            warningIcon?.SetActive(true);
            if (descriptionText != null)
                descriptionText.text = med.warningText;
        }
        else
        {
            warningIcon?.SetActive(false);
            warningDescription?.SetActive(false);
        }

        warningDescription.SetActive(false);
    }

    public void ToggleSideEffectPanel()
    {
        if (warningDescription != null)
        {
            bool isActive = warningDescription.activeSelf;
            warningDescription.SetActive(!isActive);
        }
    }

}

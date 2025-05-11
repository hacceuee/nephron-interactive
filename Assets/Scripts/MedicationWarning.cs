using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class MedicationWarning : MonoBehaviour, IPointerClickHandler
{
    [Header("Warning UI")]
    public GameObject warningIcon;         
    public GameObject warningDescription;      

    private TextMeshProUGUI descriptionText;
    private bool sideEffectVisible = false;

    private void Awake()
    {
        if (warningIcon != null)
        {
            Transform descTransform = warningIcon.transform.Find("Warning Description");
            if (descTransform != null)
            {
                descriptionText = descTransform.GetComponent<TextMeshProUGUI>();
            }
        }

        warningIcon?.SetActive(false);
        warningDescription?.SetActive(false);
    }

    public void SetWarning(Medication med)
    {
        if (med.hasWarning)
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

        sideEffectVisible = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleSideEffectPanel();
    }


    private void ToggleSideEffectPanel()
    {
        if (warningDescription != null)
        {
            sideEffectVisible = !sideEffectVisible;
            warningDescription.SetActive(sideEffectVisible);
        }
    }

    public void HideSideEffectPanel()
    {
        if (warningDescription != null)
        {
            sideEffectVisible = false;
            warningDescription.SetActive(false);
        }
    }
}

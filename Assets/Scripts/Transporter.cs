using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[ExecuteAlways]
public class Transporter : MonoBehaviour
{
    [Header("Visual Sprites")]
    public Sprite affectedSprite;
    private Sprite normalSprite;

    [Header("Medication Interaction")]
    public List<Medication> affectedBy;
    [Tooltip("Same order as medication")]
    public List<string> medicationDescriptions; // One-to-one descriptions matching above

    [Header("UI Info")]
    public Sprite nakedSprite; // Sprite to use in the UI list
    public string transporterTitle; // Title or name to show in the UI
    public int orderInList; // Order this item shows up in the info panel          

    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();

        if (image != null && normalSprite == null)
        {
            normalSprite = image.sprite; // Cache original sprite
        }
    }

    public void UpdateMedicationState(Medication currentMedication)
    {
        if (image == null) return;

        bool isAffected = affectedBy.Contains(currentMedication);
        image.sprite = isAffected ? affectedSprite : normalSprite;
    }

    public string GetEffectDescription(Medication currentMedication)
    {
        int index = affectedBy.IndexOf(currentMedication);
        if (index >= 0 && index < medicationDescriptions.Count)
        {
            return medicationDescriptions[index];
        }
        return "";
    }

    public Sprite GetIcon()
    {
        return nakedSprite != null ? nakedSprite : normalSprite;
    }

    public string GetTitle()
    {
        return string.IsNullOrEmpty(transporterTitle) ? gameObject.name : transporterTitle;
    }
}

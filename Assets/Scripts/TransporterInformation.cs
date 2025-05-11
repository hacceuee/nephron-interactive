using UnityEngine;
using System.Collections.Generic;

[ExecuteAlways]
public class TransporterInformation : MonoBehaviour
{
    [Header("Medication Descriptions")]
    public List<string> medicationDescriptions; // Matches order in Transporter.affectedBy

    [Header("UI Info")]
    public Sprite nakedSprite;
    public string transporterTitle;
    public int orderInList;

    private Transporter transporter;

    void Awake()
    {
        transporter = GetComponent<Transporter>();
    }

    public string GetEffectDescription(Medication currentMedication)
    {
        if (transporter == null) return "";

        int index = transporter.affectedBy.IndexOf(currentMedication);
        if (index >= 0 && index < medicationDescriptions.Count)
        {
            return medicationDescriptions[index];
        }
        return "";
    }

    public Sprite GetIcon()
    {
        if (nakedSprite != null)
        {
            return nakedSprite;
        }

        // fallback to normal sprite
        return transporter != null ? transporter.GetNormalSprite() : null;
    }

    public string GetTitle()
    {
        return string.IsNullOrEmpty(transporterTitle) ? gameObject.name : transporterTitle;
    }
}

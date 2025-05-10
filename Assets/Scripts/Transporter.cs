using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[ExecuteAlways]
public class Transporter : MonoBehaviour
{
    public Sprite affectedSprite;                
    private Sprite normalSprite;                

    public List<Medication> affectedBy;          // Medications that affect this transporter

    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();

        if (image != null && normalSprite == null)
        {
            normalSprite = image.sprite;         // Cache original sprite
        }
    }

    public void UpdateMedicationState(Medication currentMedication)
    {
        if (image == null) return;

        bool isAffected = affectedBy.Contains(currentMedication);
        image.sprite = isAffected ? affectedSprite : normalSprite;
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[ExecuteAlways]
public class Transporter : MonoBehaviour
{
    public List<Sprite> affectedSprites;
    private Sprite normalSprite;

    public List<Medication> affectedBy; // Medications that affect this transporter

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

        int index = affectedBy.IndexOf(currentMedication);
        if (index >= 0)
        {
            if (affectedSprites != null && affectedSprites.Count > 0)
            {
                image.sprite = index < affectedSprites.Count ? affectedSprites[index] : affectedSprites[0];
            }
        }
        else
        {
            image.sprite = normalSprite;
        }
    }

    public Sprite GetNormalSprite()
    {
        return normalSprite;
    }
}
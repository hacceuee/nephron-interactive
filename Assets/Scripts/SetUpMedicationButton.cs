using UnityEngine;
using TMPro;
using UnityEngine.UI; 

[ExecuteInEditMode]
public class SetUpMedicationButton : MonoBehaviour
{
    public Medication medication;  
    private TextMeshProUGUI textfield; 

    private void OnValidate()
    {
        textfield = GetComponentInChildren<TextMeshProUGUI>();  
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        if (medication != null && textfield != null)
        {
            textfield.text = medication.medicationName;
            gameObject.name = medication.medicationName;
        }
    }
}

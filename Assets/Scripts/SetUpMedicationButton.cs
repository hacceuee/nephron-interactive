using UnityEngine;
using TMPro;
using UnityEngine.UI; // To access the Button component

[ExecuteInEditMode]
public class SetUpMedicationButton : MonoBehaviour
{
    public Medication medication;  // Reference to the Medication
    private TextMeshProUGUI textfield;  // Reference to the TextMeshProUGUI component

    private void OnValidate()
    {
        textfield = GetComponentInChildren<TextMeshProUGUI>();  // Get the TextMeshPro component under the button
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        if (medication != null && textfield != null)
        {
            // Set the text of the TextMeshPro to the medication's name
            textfield.text = medication.medicationName;

            // Set the GameObject's name to the medication's name
            gameObject.name = medication.medicationName;
        }
    }
}

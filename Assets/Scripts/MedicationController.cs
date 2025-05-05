using UnityEngine;
using UnityEngine.UI;

public class MedicationController : MonoBehaviour
{
    public GameObject urineSlider;
    public GameObject sodiumSlider;
    public GameObject potassiumSlider;
    public GameObject calciumSlider;
    public GameObject bicarbSlider;

    private Medication currentMedication;
    public GameObject buttonDad;

    public void ApplyMedication(Medication med)
    {
        if (med == currentMedication)
        {
            clearUI();
        }

        else setMed(med);

        // TODO: Add the cell interaction
    }
    private void clearUI()
    {
        // Find all Button components in children of buttonDad
        Button[] buttons = buttonDad.GetComponentsInChildren<Button>();

        foreach (Button btn in buttons)
        {
            btn.interactable = false;
            btn.interactable = true;
        }
            
        urineSlider.GetComponent<ScaleController>()?.SetSliderValue(0f);
        sodiumSlider.GetComponent<ScaleController>()?.SetSliderValue(0f);
        potassiumSlider.GetComponent<ScaleController>()?.SetSliderValue(0f);
        calciumSlider.GetComponent<ScaleController>()?.SetSliderValue(0f);
        bicarbSlider.GetComponent<ScaleController>()?.SetSliderValue(0f);

        currentMedication = null;
    }

    private void setMed(Medication med)
    {
        currentMedication = med;

        urineSlider.GetComponent<ScaleController>()?.SetSliderValue(med.urine);
        sodiumSlider.GetComponent<ScaleController>()?.SetSliderValue(med.sodiumLevel);
        potassiumSlider.GetComponent<ScaleController>()?.SetSliderValue(med.potassiumLevel);
        calciumSlider.GetComponent<ScaleController>()?.SetSliderValue(med.calciumLevel);
        bicarbSlider.GetComponent<ScaleController>()?.SetSliderValue(med.bicarbLevel);
    }
}

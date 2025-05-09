using UnityEngine;
using UnityEngine.UI;

public class MedicationController : MonoBehaviour
{
    public GameObject urineSlider;
    public GameObject pHSlider;
    public GameObject sodiumSlider;
    public GameObject potassiumSlider;
    public GameObject calciumSlider;
    public GameObject bicarbSlider;

    private Medication currentMedication;
    //public GameObject buttonDad;

    private void Start()
    {
        clearUI();
    }

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
        /*Button[] buttons = buttonDad.GetComponentsInChildren<Button>();

        foreach (Button btn in buttons)
        {
            btn.interactable = false;
            btn.interactable = true;
        }*/ //not needed now that they're toggles! 
            
        urineSlider.GetComponent<ScaleController>()?.SetSliderValue(0f);
        pHSlider.GetComponent<ScaleController>()?.SetSliderValue(0f);
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
        pHSlider.GetComponent<ScaleController>()?.SetSliderValue(med.urine);
        sodiumSlider.GetComponent<ScaleController>()?.SetSliderValue(med.sodiumLevel);
        potassiumSlider.GetComponent<ScaleController>()?.SetSliderValue(med.potassiumLevel);
        calciumSlider.GetComponent<ScaleController>()?.SetSliderValue(med.calciumLevel);
        bicarbSlider.GetComponent<ScaleController>()?.SetSliderValue(med.bicarbLevel);
    }
}

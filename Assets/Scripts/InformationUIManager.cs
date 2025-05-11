using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class InformationUIManager : MonoBehaviour
{
    public Transform transporterListParent;
    public GameObject transporterUIPrefab;

    public GameObject noMedication;
    public GameObject noTransporters;
    private TextMeshProUGUI noTransportersText;

    private List<Transporter> transporters = new List<Transporter>();

    void Awake()
    {
        noTransportersText = noTransporters.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void RefreshUI(CellController currentCell, Medication med)
    {
        // Ensure currentCell isn't null
        if (currentCell == null)
        {
            if (noTransportersText != null)
                noTransportersText.text = "No cell selected";

            noTransporters.SetActive(true);
            return;
        }

        transporters.Clear();
        transporters.AddRange(currentCell.GetComponentsInChildren<Transporter>(true));
        print("i found transporters!");
        UpdateUI(med);
        print("updateUI was called");
    }

    private void UpdateUI(Medication med)
    {
        foreach (Transform child in transporterListParent)
        {
            if (child.GetComponent<TransporterEntry>() != null)
                Destroy(child.gameObject);
        }

        noMedication.SetActive(false);
        noTransporters.SetActive(false);

        if (med == null)
        {
            noMedication.SetActive(true);
            return;
        }

        var affected = transporters
            .Where(t => t.affectedBy.Contains(med))
            .OrderBy(t => t.orderInList)
            .ToList();

        if (affected.Count == 0)
        {
            if (noTransportersText != null)
                noTransportersText.text = $"{med.medicationName} does not affect this cell.";

            noTransporters.SetActive(true);
            return;
        }

        foreach (var t in affected)
        {
            GameObject ui = Instantiate(transporterUIPrefab, transporterListParent);

            Image icon = ui.transform.Find("Receptor Image").GetComponent<Image>();
            TextMeshProUGUI name = ui.transform.Find("Receptor Image/Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI description = ui.transform.Find("Description").GetComponent<TextMeshProUGUI>();

            icon.sprite = t.GetIcon();
            name.text = t.GetTitle(); 
            description.text = t.GetEffectDescription(med);

            print("Updated transporter UI");
        }

    }
}

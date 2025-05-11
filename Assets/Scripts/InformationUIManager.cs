using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class InformationUIManager : MonoBehaviour
{
    public Transform transporterListParent;
    public GameObject transporterUIPrefab;

    public GameObject defaultMessagePanel; // This contains the TMP text
    private TextMeshProUGUI defaultMessageText;

    private List<Transporter> transporters = new List<Transporter>();

    void Awake()
    {
        defaultMessageText = defaultMessagePanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void RefreshUI(CellController currentCell, Medication med)
    {
        // Clear existing transporter entries
        foreach (Transform child in transporterListParent)
        {
            if (child.GetComponent<TransporterEntry>() != null)
                Destroy(child.gameObject);
        }

        defaultMessagePanel.SetActive(false);

        // Determine the current UI state
        IUIState state;

        if (currentCell == null)
        {
            state = new NoLocationSelectedState(defaultMessagePanel, defaultMessageText);
            print("no location selected state");
        }
        else
        {
            transporters.Clear();
            transporters.AddRange(currentCell.GetComponentsInChildren<Transporter>(true));

            if (med == null)
            {
                state = new NoMedicationSelectedState(defaultMessagePanel, defaultMessageText);
                print("no medication selected state");
            }
            else
            {
                var affected = transporters
                    .Where(t => t.affectedBy.Contains(med))
                    .OrderBy(t =>
                    {
                        var info = t.GetComponent<TransporterInformation>();
                        return info != null ? info.orderInList : int.MaxValue;
                    })
                    .ToList();

                if (affected.Count == 0)
                {
                    state = new NoTransporterAffectedState(defaultMessagePanel, defaultMessageText, med);
                    print("no transporters founds state");
                }
                else
                {
                    state = new TransportersPresentState(affected, transporterUIPrefab, transporterListParent, med);
                    print("populating transporters list");
                }
            }
        }

        state.Apply();
    }

    // --- Interface for states ---
    private interface IUIState
    {
        void Apply();
    }

    // --- State: No location selected ---
    private class NoLocationSelectedState : IUIState
    {
        private GameObject panel;
        private TextMeshProUGUI text;

        public NoLocationSelectedState(GameObject panel, TextMeshProUGUI text)
        {
            this.panel = panel;
            this.text = text;
        }

        public void Apply()
        {
            text.text = "Please select a location on the nephron.";
            panel.SetActive(true);
        }
    }

    // --- State: Location selected, no medication selected ---
    private class NoMedicationSelectedState : IUIState
    {
        private GameObject panel;
        private TextMeshProUGUI text;

        public NoMedicationSelectedState(GameObject panel, TextMeshProUGUI text)
        {
            this.panel = panel;
            this.text = text;
        }

        public void Apply()
        {
            text.text = "Please select a medication to see its effect.";
            panel.SetActive(true);
        }
    }

    // --- State: Location + Medication, but no affected transporters ---
    private class NoTransporterAffectedState : IUIState
    {
        private GameObject panel;
        private TextMeshProUGUI text;
        private Medication med;

        public NoTransporterAffectedState(GameObject panel, TextMeshProUGUI text, Medication med)
        {
            this.panel = panel;
            this.text = text;
            this.med = med;
        }

        public void Apply()
        {
            text.text = $"{med.medicationName} does not affect this cell.";
            panel.SetActive(true);
        }
    }

    // --- State: Transporters affected and present ---
    private class TransportersPresentState : IUIState
    {
        private List<Transporter> affected;
        private GameObject prefab;
        private Transform parent;
        private Medication med;

        public TransportersPresentState(List<Transporter> affected, GameObject prefab, Transform parent, Medication med)
        {
            this.affected = affected;
            this.prefab = prefab;
            this.parent = parent;
            this.med = med;
        }

        public void Apply()
        {
            foreach (var t in affected)
            {
                var info = t.GetComponent<TransporterInformation>();
                if (info == null) continue;

                GameObject ui = GameObject.Instantiate(prefab, parent);

                Image icon = ui.transform.Find("Receptor Image").GetComponent<Image>();
                TextMeshProUGUI name = ui.transform.Find("Receptor Image/Name").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI description = ui.transform.Find("Description").GetComponent<TextMeshProUGUI>();

                icon.sprite = info.GetIcon();
                name.text = info.GetTitle();
                description.text = info.GetEffectDescription(med);

                print(info.GetTitle()); 
            }
        }
    }
}

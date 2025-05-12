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
        }
        else
        {
            transporters.Clear();
            transporters.AddRange(currentCell.GetComponentsInChildren<Transporter>(true));

            if (med == null)
            {
                state = new NoMedicationSelectedState(defaultMessagePanel, defaultMessageText);
            }
            else
            {
                var affected = transporters
                    .Select(t => new { transporter = t, info = t.GetComponent<TransporterInformation>() })
                    .Where(pair => pair.transporter.affectedBy.Contains(med) && pair.info != null)
                    .OrderBy(pair => pair.info.orderInList)
                    .Select(pair => pair.transporter)
                    .ToList();

                if (affected.Count == 0)
                {
                    state = new NoTransporterAffectedState(defaultMessagePanel, defaultMessageText, med);
                }
                else
                {
                    state = new TransportersPresentState(affected, transporterUIPrefab, transporterListParent, med);
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
            for (int i = 0; i < affected.Count; i++)
            {
                var t = affected[i];
                var info = t.GetComponent<TransporterInformation>();
                if (info == null) continue;

                GameObject ui = GameObject.Instantiate(prefab, parent);

                Image icon = ui.transform.Find("Header/Receptor Image").GetComponent<Image>();
                TextMeshProUGUI name = ui.transform.Find("Header/Name").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI description = ui.transform.Find("Description").GetComponent<TextMeshProUGUI>();

                icon.sprite = info.GetIcon();
                icon.SetNativeSize();

                name.text = info.GetTitle();
                description.text = info.GetEffectDescription(med);

                GameObject divider = ui.transform.Find("Dividing Line")?.gameObject;
                {
                    if (i == affected.Count - 1)
                    {
                        divider.SetActive(false);
                    }
                    else
                    {
                        divider.SetActive(true);
                    }
                }

            }
        }
    }
}

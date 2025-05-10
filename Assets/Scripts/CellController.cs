using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CellController : MonoBehaviour
{
    private Transporter[] transporters;

    void Awake()
    {
        transporters = GetComponentsInChildren<Transporter>(true);
    }

    public void ApplyMedication(Medication med)
    { 
        foreach (var transporter in transporters)
        {
            transporter.UpdateMedicationState(med);
        }
    }
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Medication", menuName = "ScriptableObjects/Medication")]
public class Medication : ScriptableObject
{
    [Header("Name")]
    public string medicationName;

    [Header("Values")]
    public float urine;
    public float sodiumLevel;
    public float potassiumLevel;
    public float calciumLevel;
    public float bicarbLevel;

    [Header("Warnings")]
    public bool hasWarning;
    [TextArea] public string warningText;

    //[Header("Cells")]
    // public Cell cellInteraction; // implementing later
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Medication", menuName = "ScriptableObjects/Medication")]
public class Medication : ScriptableObject
{
    [Header("Name")]
    public string medicationName;

    [Header("Values")]
    public int urine;
    public int sodiumLevel;
    public int potassiumLevel;
    public int calciumLevel;
    public int bicarbLevel;

    [Header("Warnings")]
    public bool hasWarning;
    [TextArea] public string warningText;

    //[Header("Cells")]
    // public Cell cellInteraction; // implementing later
}

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

    /*[Header("Proximal Convoluted Tubule")]
        [Tooltip("Na+/H+ exchanger 3")]
            public bool NHE3;
        [Tooltip("Sodium-dependent glucose cotransporter 2")]
            public bool SGLT2;
        [Tooltip("Electrogenic Na+:HCO3- cotransporter")]
            public bool NBCe1;
        [Tooltip("Sodium-potassium pump; Na⁺/K⁺-ATPase")]
            public bool PCT_NaKATPase;

    [Header("Thick Ascending Limb")]
        [Tooltip("Na/K/2Cl cotransporter")]
            public bool NKCC2;
        [Tooltip("Apical channel for K; Renal outer medullary potassium channel")]
            public bool TAL_ROMK;
        [Tooltip("Sodium-potassium pump; Na⁺/K⁺-ATPase")]
            public bool TAL_NaKATPase;

    [Header("Distal Convoluted Tubule")]
        [Tooltip("Sodium-potassium pump; Na⁺/K⁺-ATPase")]
            public bool DCT_NaKATPase;
        [Tooltip("Thiazide-sensitive Na+-Cl- cotransporter")]
            public bool NCC;

    [Header("Collecting Duct Principal Cell")]
        [Tooltip("Epithelial Na+ channel")]
            public bool ENaC;
        [Tooltip("Sodium-potassium pump; Na⁺/K⁺-ATPase")]
            public bool CDPC_NaKATPase;
        [Tooltip("Apical channel for K; Renal outer medullary potassium channel")]
            public bool CDPC_ROMK;
        [Tooltip("Aquaporin-2")]
          public bool AQP2;

    [Header("Collecting Duct A-Intercalated Cell")]
        [Tooltip("H+/K+-ATPases")]
            public bool HKATPase;
        [Tooltip("H+-ATPase")]
            public bool A_HATPase;
        [Tooltip("Cl-/HCO3- exchanger")]
            public bool AE1;

    [Header("Collecting Duct B-Intercalated Cell")]
        [Tooltip("Apical Cl-/HCO3- exchanger")]
            public bool pendrin;
        [Tooltip("Basolateral H+-ATPase")]
            public bool B_HATPase;
        [Tooltip("Na+-dependent Cl-/HCO3- exchanger; basolateral")]
            public bool NDCBE;

    [Header("Shared")]
        [Tooltip("Anti-diuretic hormone")]
            public bool ADH;
        [Tooltip("Mineralocorticoid Receptor")]
            public bool MR; */

}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Toggle))]
public class LocationActions : MonoBehaviour
{
    [Header("Visual Overlay")]
    public Image activeRegion;              // The active nephron region overlay
    private float fadeDuration = 0.25f;

    [Header("Cell Prefab Settings")]
    public GameObject cellPrefab;           // The cell prefab to instantiate
    private Transform cellParent;           // Where to nest the cell prefab

    public MedicationController medicationController;
    public CellUIManager cellUI;

    private Toggle toggle;
    private Coroutine fadeCoroutine;
    private GameObject currentCellInstance;

    void Awake()
    {
        cellParent = GameObject.Find("CellBackground").transform;
   
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void Start()
    {
        SetOverlayAlpha(toggle.isOn ? 1f : 0f);

        if (toggle.isOn)
            SpawnCell();
    }

    void OnToggleChanged(bool isOn)
    {
        FadeOverlay(isOn);
        HandleCellSpawn(isOn);

        if (isOn)
        {
            cellUI.FadeOut();
            string cellName = gameObject.name; 
            cellUI.UpdateCellName(cellName);
        }
        else
        {
            cellUI.FadeIn();
            cellUI.ResetCellName();
        }
    }

    void FadeOverlay(bool isOn)
    {
        if (activeRegion == null) return;

        float targetAlpha = isOn ? 1f : 0f;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(ImageFadeLERP(targetAlpha));
    }

    IEnumerator ImageFadeLERP(float targetAlpha)
    {
        Color color = activeRegion.color;
        float startAlpha = color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            activeRegion.color = new Color(color.r, color.g, color.b, alpha);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        activeRegion.color = new Color(color.r, color.g, color.b, targetAlpha);
    }

    void SetOverlayAlpha(float alpha)
    {
        if (activeRegion == null) return;

        Color color = activeRegion.color;
        color.a = alpha;
        activeRegion.color = color;
    }

    void HandleCellSpawn(bool isOn)
    {
        if (isOn)
        {
            if (currentCellInstance != null)
                DestroyImmediate(currentCellInstance);

            SpawnCell();
        }
        else if (currentCellInstance != null)
        {
            DestroyImmediate(currentCellInstance);
            currentCellInstance = null;
        }
    }

    void SpawnCell()
    {
        if (cellPrefab != null && cellParent != null)
        {
            currentCellInstance = Instantiate(cellPrefab, cellParent);
            medicationController.currentCell = currentCellInstance.GetComponent<CellController>();

            if (medicationController.getMedication() != null)
            {
                medicationController.ApplyMedicationToCell(medicationController.getMedication());
            }

        }
    }

    void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnToggleChanged);
    }
}

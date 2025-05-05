using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScaleController : MonoBehaviour
{
    [Header("Fill Control")]
    [Range(-5, 5)] public float sliderValue = 0;

    [Header("Settings")]
    public float minValue = -5f;
    public float maxValue = 5f;
    public float lerpDuration = 1.0f;

    [Header("Fill Area")]
    public RectTransform fillArea;

    private float lastSliderValue;
    private float fullWidth;
    private float centerPosition => fullWidth * 0.5f;

    private Coroutine lerpCoroutine;

    private readonly Quaternion negativeRotation = Quaternion.identity;
    private readonly Quaternion positiveRotation = Quaternion.Euler(0, 180, 0);

    private void OnEnable()
    {
        fullWidth = GetComponent<RectTransform>().rect.width;
        lastSliderValue = sliderValue - 1; // Force update on first pass
        UpdateFillArea();
    }

    private void OnValidate()
    {
        if (fillArea == null) return;

        if (fullWidth <= 0)
            fullWidth = GetComponent<RectTransform>().rect.width;

        UpdateFillArea();
    }

    private void UpdateFillArea()
    {
        if (sliderValue == lastSliderValue && Application.isPlaying)
            return;

        lastSliderValue = sliderValue;

        // Always set Y values the same
        float offsetMinY = fillArea.offsetMin.y;
        float offsetMaxY = fillArea.offsetMax.y;

        // Calculate fill and set rotation
        if (sliderValue > 0)
        {
            // Positive values - fill right + flip
            float fillAmount = (sliderValue / maxValue) * centerPosition;
            fillArea.offsetMin = new Vector2(centerPosition, offsetMinY);
            fillArea.offsetMax = new Vector2(-(fullWidth - (centerPosition + fillAmount)), offsetMaxY);
            fillArea.localRotation = positiveRotation;
        }
        else
        {
            // Zero or negative values
            float fillAmount = sliderValue < 0 ? (sliderValue / minValue) * centerPosition : 0;
            fillArea.offsetMin = new Vector2(centerPosition - fillAmount, offsetMinY);
            fillArea.offsetMax = new Vector2(-(fullWidth - centerPosition), offsetMaxY);
            fillArea.localRotation = negativeRotation;
        }
    }

    public void SetSliderValue(float value)
    {
        value = Mathf.Clamp(value, minValue, maxValue);

        if (lerpCoroutine != null)
            StopCoroutine(lerpCoroutine);

        lerpCoroutine = StartCoroutine(LerpToValue(value));
    }

    private IEnumerator LerpToValue(float target)
    {
        float start = sliderValue;
        float elapsed = 0f;

        while (elapsed < lerpDuration)
        {
            sliderValue = Mathf.Lerp(start, target, elapsed / lerpDuration);
            UpdateFillArea();
            elapsed += Time.deltaTime;
            yield return null;
        }

        sliderValue = target;
        UpdateFillArea();
        lerpCoroutine = null;
    }
}
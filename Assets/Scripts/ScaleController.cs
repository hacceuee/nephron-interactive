using UnityEngine;

[ExecuteInEditMode]
public class ScaleController : MonoBehaviour
{
    [Header("Fill Control")]
    [Range(-5, 5)] public float sliderValue = 0;

    [Header("Settings")]
    public float minValue = -5f;
    public float maxValue = 5f;

    [Header("Fill Area")]
    public RectTransform fillArea;

    private float lastSliderValue;
    private float fullWidth;
    private readonly Quaternion negativeRotation = Quaternion.identity;
    private readonly Quaternion positiveRotation = Quaternion.Euler(0, 180, 0);

    private void Start()
    {
        InitializeAtZero();
    }

    private void OnEnable()
    {
        InitializeAtZero();
    }

    private void InitializeAtZero()
    {
        sliderValue = 0;
        fullWidth = GetComponent<RectTransform>().rect.width;
        UpdateFillArea(forceUpdate: true);
    }

    private void OnValidate()
    {
        if (fillArea == null) return;

        if (fullWidth == 0)
            fullWidth = GetComponent<RectTransform>().rect.width;

        if (sliderValue != lastSliderValue || !Application.isPlaying)
        {
            UpdateFillArea();
            lastSliderValue = sliderValue;
        }
    }

    private void UpdateFillArea(bool forceUpdate = false)
    {
        if (!forceUpdate && sliderValue == lastSliderValue && Application.isPlaying)
            return;

        float centerPosition = fullWidth * 0.5f;

        if (sliderValue > 0)
        {
            // Positive values - fill right + flip
            float fillAmount = (sliderValue / maxValue) * centerPosition;
            fillArea.offsetMin = new Vector2(centerPosition, fillArea.offsetMin.y);
            fillArea.offsetMax = new Vector2(-(fullWidth - (centerPosition + fillAmount)), fillArea.offsetMax.y);
            fillArea.localRotation = positiveRotation;
        }
        else if (sliderValue < 0)
        {
            // Negative values - fill left
            float fillAmount = (sliderValue / minValue) * centerPosition;
            fillArea.offsetMin = new Vector2(centerPosition - fillAmount, fillArea.offsetMin.y);
            fillArea.offsetMax = new Vector2(-(fullWidth - centerPosition), fillArea.offsetMax.y);
            fillArea.localRotation = negativeRotation;
        }
        else
        {
            // Zero - centered with no fill
            fillArea.offsetMin = new Vector2(centerPosition, fillArea.offsetMin.y);
            fillArea.offsetMax = new Vector2(-(fullWidth - centerPosition), fillArea.offsetMax.y);
            fillArea.localRotation = negativeRotation;
        }
    }
}
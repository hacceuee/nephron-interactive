using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Toggle))]
public class LocationActions : MonoBehaviour
{
    public Image targetImage;
    private float fadeDuration = 0.1f;

    private Toggle toggle;
    private Coroutine fadeCoroutine;

    void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(UpdateImageAlpha);
    }

    void Start()
    {
        SetAlpha(toggle.isOn ? 1f : 0f); // Initialize opacity
    }

    void UpdateImageAlpha(bool isOn)
    {
        float targetAlpha = isOn ? 1f : 0f;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeAlpha(targetAlpha, fadeDuration));
    }

    IEnumerator FadeAlpha(float targetAlpha, float duration)
    {
        Color color = targetImage.color;
        float startAlpha = color.a;
        float time = 0f;

        while (time < duration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            targetImage.color = new Color(color.r, color.g, color.b, newAlpha);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        targetImage.color = new Color(color.r, color.g, color.b, targetAlpha);
    }

    void SetAlpha(float alpha)
    {
        if (targetImage != null)
        {
            Color color = targetImage.color;
            color.a = alpha;
            targetImage.color = color;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class CellUIManager : MonoBehaviour
{
    private Image whiteOverlay;           
    private float fadeDuration = 0.05f;    
    private Coroutine fadeCoroutine;
    private float initialAlpha;

    private bool locationSelected = false;
    public TextMeshProUGUI cellNameText;

    private string textDefault;
    private Color defaultTextColor;
    private FontStyles defaultFontStyle;

    void Awake()
    {
        whiteOverlay = GetComponent<Image>();

        if (whiteOverlay != null)
        {
            initialAlpha = whiteOverlay.color.a;
        }
        
        if (cellNameText != null)
        {
            textDefault = cellNameText.text;
            defaultTextColor = cellNameText.color;
            defaultFontStyle = cellNameText.fontStyle;
        }
    }

    public void FadeOut()
    {
        if (whiteOverlay == null || locationSelected) return;

        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeToAlpha(0f));
        locationSelected = true;
    }

    public void FadeIn()
    {
        if (whiteOverlay == null || !locationSelected) return;

        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeToAlpha(initialAlpha));
        locationSelected = false;
    }

    private IEnumerator FadeToAlpha(float targetAlpha)
    {
        Color color = whiteOverlay.color;
        float startAlpha = color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            whiteOverlay.color = new Color(color.r, color.g, color.b, alpha);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        whiteOverlay.color = new Color(color.r, color.g, color.b, targetAlpha);
    }

    public void UpdateCellName(string cellName)
    {
        if (cellNameText == null) return;

        cellNameText.text = cellName;

        Color color = cellNameText.color;
        color.a = 1f;
        cellNameText.color = color;

        // Remove italics
        cellNameText.fontStyle &= ~FontStyles.Italic;
    }

    public void ResetCellName()
    {
        if (cellNameText == null) return;

        cellNameText.text = textDefault;
        cellNameText.color = defaultTextColor;
        cellNameText.fontStyle = defaultFontStyle;
    }

}

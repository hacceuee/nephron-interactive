using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ForceUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    void Start()
    {
        if (canvasGroup != null)
            canvasGroup.alpha = 0f;

        StartCoroutine(DelayedForceRebuild());
    }

    IEnumerator DelayedForceRebuild()
    {
        yield return new WaitForEndOfFrame();

        // Force the layout rebuild
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

        // Show UI again
        if (canvasGroup != null)
            canvasGroup.alpha = 1f;
    }
}

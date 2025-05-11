using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ResizeToFitChild : MonoBehaviour
{
    public RectTransform childContainer;

    void Update()
    {
        if (childContainer == null) return;
        float preferredHeight = LayoutUtility.GetPreferredHeight(childContainer);
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 size = rectTransform.sizeDelta;
        size.y = preferredHeight;
        rectTransform.sizeDelta = size;
    }
}

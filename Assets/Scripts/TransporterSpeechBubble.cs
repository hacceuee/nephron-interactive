using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TransporterSpeechBubble : MonoBehaviour, IPointerClickHandler
{
    public GameObject speechBubblePanel;
    //public Button closeButton;

    private void Awake()
    {
        SpeechBubbleManager.Instance.RegisterBubble(this);
        speechBubblePanel.SetActive(false);

        //if (closeButton != null)
           // closeButton.onClick.AddListener(Hide);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SpeechBubbleManager.Instance.ShowOnly(this);
    }

    public void Show()
    {
        speechBubblePanel.SetActive(true);
    }

    public void Hide()
    {
        speechBubblePanel.SetActive(false);
    }

    public bool IsVisible()
    {
        return speechBubblePanel.activeSelf;
    }
}

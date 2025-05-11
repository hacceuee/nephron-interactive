using UnityEngine;
using System.Collections.Generic;

public class SpeechBubbleManager : MonoBehaviour
{
    public static SpeechBubbleManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private List<TransporterSpeechBubble> allBubbles = new List<TransporterSpeechBubble>();

    public void RegisterBubble(TransporterSpeechBubble bubble)
    {
        if (!allBubbles.Contains(bubble))
            allBubbles.Add(bubble);
    }

    public void ShowOnly(TransporterSpeechBubble selected)
    {
        bool isAlreadyActive = selected.IsVisible();

        foreach (var bubble in allBubbles)
        {
            if (bubble != selected)
                bubble.Hide();
        }

        if (isAlreadyActive)
        {
            selected.Hide(); // Toggle off
        }
        else
        {
            selected.Show();
        }
    }


    public void HideAll()
    {
        foreach (var bubble in allBubbles)
        {
            bubble.Hide();
        }
    }
}

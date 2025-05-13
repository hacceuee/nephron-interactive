using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HideInformationPanel : MonoBehaviour, IPointerClickHandler
{
    public GameObject rotatableObject; 
    public GameObject toggleableObject; 
    public TextMeshProUGUI hideText; 

    private bool isToggled = true; 
    private Vector3 defaultRotation = new Vector3(0f, 0f, 180f); 
    private Vector3 toggledRotation = new Vector3(0f, 0f, 0f); 
    private string promptHide = "Hide";
    private string promptShow = "Show";

    public void OnPointerClick(PointerEventData eventData)
    {
        if (rotatableObject != null && toggleableObject != null)
        {
            isToggled = !isToggled;

            rotatableObject.transform.eulerAngles = isToggled ? defaultRotation : toggledRotation;
            toggleableObject.SetActive(isToggled);
            hideText.text = isToggled ? promptHide : promptShow; 

        }
    }
}

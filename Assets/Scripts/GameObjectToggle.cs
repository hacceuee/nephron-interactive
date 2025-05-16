using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GameObjectToggle : MonoBehaviour
{             
    public GameObject toggledGameObject;  

    private void Awake()
    {
        toggledGameObject?.SetActive(false);
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ToggleGameObject);
            
    }

    private void ToggleGameObject()
    {
        if (toggledGameObject != null)
        {
            toggledGameObject.SetActive(!toggledGameObject.activeSelf);
        }
    }
}

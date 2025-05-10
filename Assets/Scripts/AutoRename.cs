using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[ExecuteInEditMode]
[RequireComponent(typeof(Image))]
public class AutoRename : MonoBehaviour
{
    private void OnValidate()
    {
        Image img = GetComponent<Image>();
        if (img != null && img.sprite != null)
        {
            string spriteName = img.sprite.name;

            // Trim "@3x", "@2x", etc.
            int atIndex = spriteName.IndexOf('@');
            if (atIndex > 0)
                spriteName = spriteName.Substring(0, atIndex);

            gameObject.name = spriteName;
        }
    }
}


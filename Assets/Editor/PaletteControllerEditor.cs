#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PaletteController))]
public class PaletteControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var controller = (PaletteController)target;
        DrawDefaultInspector();

        if (controller.palette != null && controller.applications != null)
        {
            var names = controller.palette.GetColorNames();

            SerializedProperty apps = serializedObject.FindProperty("applications");
            for (int i = 0; i < apps.arraySize; i++)
            {
                SerializedProperty app = apps.GetArrayElementAtIndex(i);
                SerializedProperty colorNameProp = app.FindPropertyRelative("colorName");

                int selectedIndex = Mathf.Max(System.Array.IndexOf(names, colorNameProp.stringValue), 0);
                selectedIndex = EditorGUILayout.Popup("Color Name", selectedIndex, names);
                colorNameProp.stringValue = names[selectedIndex];
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif

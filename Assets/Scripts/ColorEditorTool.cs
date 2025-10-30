#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class ColorEditorTool : EditorWindow
{

    [MenuItem("Tools/Color editor tool")]
    public static void ShowColorEditorTool()
    {
        ColorEditorTool wnd = GetWindow<ColorEditorTool>();
        wnd.titleContent = new GUIContent("Color editor tool");
    }

    public static Color cubeColor = Color.white;

    public void OnGUI()
    {
        cubeColor = EditorGUILayout.ColorField(cubeColor);
        if (GUILayout.Button("Submit"))
        {
            string colorString = ColorUtility.ToHtmlStringRGB(cubeColor);
            var request = UnityWebRequest.Put("http://127.0.0.1:8000/api/objects/1/", $"title=cube&color={colorString}");
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            request.SendWebRequest();
        }
    }
}

#endif

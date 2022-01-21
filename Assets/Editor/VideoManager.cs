using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

class VideoManager : EditorWindow
{
    Editor editor;

    [MenuItem("Window/VideoManager")]
    public static void Init() { GetWindow<VideoManager>("Video Manager", true); }

    void OnGUI()
    {
        if (!editor) { editor = Editor.CreateEditor(this); }
        if (editor) { editor.OnInspectorGUI(); }
    }
}

[CustomEditor(typeof(VideoManager), true)]
public class VideoManagerEditorDrawer : Editor
{
    private string folderName = "Assets/Videos";
    private List<GameObject> videos;
    private bool refresh = true;

    [SerializeField] 
    List<UserData> userDataList = new List<UserData>();
    public override void OnInspectorGUI()
    {
        folderName = EditorGUILayout.TextField("Folder", folderName);
        if (GUILayout.Button("Reload"))
        {
            refresh = true;
        }
        if (refresh == true)
        {
            Refresh();
        }
        if (GUILayout.Button("Save"))
        {
            Save();
        }
        var list = serializedObject.FindProperty("userDataList");
        EditorGUILayout.PropertyField(list, new GUIContent("Meta Data"), true);
        serializedObject.CopyFromSerializedProperty(list);
        serializedObject.ApplyModifiedProperties();
        Debug.Log(list.CountInProperty());
    }

    private void Refresh()
    {
       // var dirInfo = new DirectoryInfo(folderName);
       // var metaCollection = dirInfo.GetFiles("*.userData");
       // SerializedObject serializedObject = new UnityEditor.SerializedObject(userDataList);
       // foreach (var meta in metaCollection)
       // {
       //     var list = serializedObject.FindProperty("userDataList");
       //     var streamReader = meta.OpenText();
       //     var deserializedString = streamReader.ReadToEnd();
       //     var userData = JsonUtility.FromJson<UserData>(deserializedString);
       //     list.InsertArrayElementAtIndex(list.CountInProperty()-3);
       //     userDataList.Add(userData);
       //     serializedObject.ApplyModifiedProperties();
       //     Debug.Log(userData);
       // }
       // refresh = false;
    }

    private void Save()
    {

    }
}
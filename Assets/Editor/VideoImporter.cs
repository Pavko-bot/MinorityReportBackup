using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.IO;

public class Importer : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            if (str.EndsWith(".mp4"))
            {
                var window = EditorWindow.CreateInstance<VideoImporterWindow>();
                var userDataFileName = Path.ChangeExtension(str, "userData");
                window.Init(userDataFileName);
                window.Show();
            }
        }
        foreach (string str in deletedAssets)
        {
            if (str.EndsWith(".mp4"))
            {
                var userDataFileName = Path.ChangeExtension(str, "userData");
                File.Delete(userDataFileName);
                File.Delete(userDataFileName + ".meta");
            }
        }
        //Not Implemented
        //for (int i = 0; i < movedAssets.Length; i++)
        //{
        //    if (movedAssets[i].EndsWith(".mp4"))
        //    {
        //        Debug.Log("Moved Asset: " + movedAssets[i] + " from: " + movedFromAssetPaths[i]);
        //    }
        //}
        //
    }
}


class VideoImporterWindow : EditorWindow
{
    private string path;
    private UserData data;

    internal void Init(string _path)
    {
        path = _path;
        data = new UserData();
        var filename = Path.GetFileNameWithoutExtension(_path);
        data.Name = filename;
        data.Video = filename;
    }

    void OnGUI()
    {
        GUILayout.Label("Video Import", EditorStyles.boldLabel);
        Type myObjectType = data.GetType();
        FieldInfo[] fields = myObjectType.GetFields();
        
        foreach (FieldInfo field in fields)
        {
            if (field != null)
            {
                var currentData = field.GetValue(data).ToString() ?? "";
                var textField = EditorGUILayout.TextField(field.Name, currentData);
                try
                { 
                    var newData = Convert.ChangeType(textField, field.FieldType);
                    field.SetValue(data, newData);
                }
                catch (FormatException)
                {
                }
            }
        }
        if (GUILayout.Button("Save UserData"))
        {
            if (File.Exists(path))
            {
                if(EditorUtility.DisplayDialog("File already exists",
                                            "Do you want to override " + path,
                                            "Yes",
                                            "No"))
                {
                    WriteToFile();
                }
            }
            else
            {
                WriteToFile();
            }
        }
        if (GUILayout.Button("Don't add UserData"))
        {
            Close();
        }
    }

    private void WriteToFile()
    {
        string serialized = JsonUtility.ToJson(data);
        File.WriteAllText(path, serialized);
        AssetDatabase.Refresh();
        Close();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraManager : EditorWindow
{
    public List<Camera> cameras = new List<Camera>();

    int columns ;
    int maxColumns = 6;
    int rows ;
    int amount;
    int selectedCamera;
    Vector2 scrollPos;
    [MenuItem("Window/Camera Utilities/Camera Manager")]
    public static void ShowWindow()
    {
        var camManager = GetWindow<CameraManager>("Camera Manager");
        
        camManager.Reload();
    }
    void Reload()
    {
        var _cameras = FindObjectsOfType<Camera>();
        rows = 0;
        cameras.Clear();
        foreach (var item in _cameras)
        {
            var rt = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
            rt.Create();
            item.targetTexture = rt;
            cameras.Add(item);
        }
        amount= cameras.Count;
        columns = amount;
        
        if (columns > maxColumns)
        {
            columns = maxColumns;
            float aux = (float)amount / (float)maxColumns;
            int intConvertion = (int)aux;
            if (aux > intConvertion)
                rows = (int)aux + 1;
            else
                rows = (int)aux;

        }
    }
    public void OnDestroy()
    {
        foreach (var item in cameras)
        {
            item.targetTexture=null;
        }
    }
    private void OnGUI()
    {
        minSize = new Vector3(613,900);
        
        if (GUILayout.Button("Reload")) Reload();
        GUI.BeginGroup(new Rect(10, 25, Screen.width, Screen.height));
        
        int index = 0;
        
        
        if (cameras.Count <= 0) return;

        for (int y = 0; y <= rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {

                if (index < amount)
                {

                    if (cameras[index] == null)
                    {
                        cameras.Clear();
                        Reload();
                        break;
                    }
                    index++;
                    if (GUI.Button(new Rect(x * 100, y * 100, 100, 100), cameras[index - 1].targetTexture))
                    {
                        selectedCamera = index - 1;
                    }
                }
            }

        }
        GUI.BeginGroup(new Rect(0, (rows+1)*100, Screen.width, Screen.height*0.9f));
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(Screen.width*.95f), GUILayout.Height(Screen.height - ((rows + 1) * 100)));
        if(cameras.Count > 0)
        {
            GameObject sel = cameras[selectedCamera].gameObject;
            Camera targetComp = sel.GetComponent<Camera>();
            if (targetComp != null)
            {
                var editor = Editor.CreateEditor(targetComp);
                editor.OnInspectorGUI();
            }
        }
        GUI.EndGroup();
        GUI.EndGroup();
        GUILayout.Space(100);
        EditorGUILayout.EndScrollView();
    }
}

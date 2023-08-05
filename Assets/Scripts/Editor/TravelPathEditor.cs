using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TravelPath))]
[ExecuteAlways]
public class TravelPathEditor : Editor
{
    TravelPath tgt;
    Texture playTex;
    Texture stopTex;
    RenderTexture renderTexture;
    MyPath lastTravelPath;
    
    private void Awake()
    {
        tgt = (TravelPath)target;
        playTex= (Texture)AssetDatabase.LoadAssetAtPath("Assets/Textures/play.png", typeof(Texture));
        stopTex = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Textures/stop.png", typeof(Texture));
        renderTexture = (RenderTexture)AssetDatabase.LoadAssetAtPath("Assets/Textures/renderTexture.renderTexture", typeof(RenderTexture));
        
    }
    private void OnSceneGUI()
    {
        
    }
    public override void OnInspectorGUI()
    {

        tgt.path = (MyPath)EditorGUILayout.ObjectField( "List Container",tgt.path,typeof(MyPath),true);
        if (tgt.path != null)
        {
            if(lastTravelPath==null)
                lastTravelPath = tgt.path;
            if (lastTravelPath != tgt.path)
            {
                Debug.Log(lastTravelPath);
                tgt.SetList();
                tgt.play = false;
                tgt.ResetPosition();
                lastTravelPath=tgt.path;
                OnButton();
            }

        }
        tgt.Speed = EditorGUILayout.Slider("Speed",tgt.Speed, 0, 50);
        tgt.speedRotation = EditorGUILayout.Slider("Speed Rotation", tgt.speedRotation, 0, 5);
        tgt.Threshold = EditorGUILayout.Slider("Threshold", tgt.Threshold, 0, 3);
        tgt.Loop = EditorGUILayout.Toggle("Loop", tgt.Loop);

        GUI.Label(new Rect(15, 85, 80, 50), "Controls:");
        GUI.contentColor = Color.black;
        if (GUI.Button(new Rect(Screen.width*.2f,100,20,20), tgt.play?new GUIContent("||"): new GUIContent(playTex)))
        {
            
            tgt.play = !tgt.play;
        }
        GUI.contentColor = Color.white;
        EditorGUILayout.Space(50);
        
        if (GUI.Button(new Rect(Screen.width*.2f+20, 100, 20, 20), new GUIContent(stopTex)))
        {
            tgt.SetList();
            tgt.play = false;
            tgt.ResetPosition();
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Follow Path");
        GUI.backgroundColor= tgt.FollowPath ? Color.gray:Color.white;
        
        if (GUILayout.Button("On"))
        {
            OnButton();
           
        }
        GUI.backgroundColor = tgt.FollowPath ? Color.white: Color.gray;
        if (GUILayout.Button("Off"))
        {
            tgt.FollowPath = false;
            if (!tgt.gameObject.GetComponent<LookAtScript>())
                tgt.gameObject.AddComponent<LookAtScript>();
        }
        EditorGUILayout.EndHorizontal();


    }
    void OnButton()
    {
        tgt.FollowPath = true;
        if (tgt.gameObject.GetComponent<LookAtScript>() && tgt.FollowPath)
            DestroyImmediate(tgt.gameObject.GetComponent<LookAtScript>());
    }
}

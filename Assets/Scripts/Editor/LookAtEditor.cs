using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(LookAtScript))]
public class LookAtEditor : Editor
{
    LookAtScript tgt;
    TravelPath travelPath;
    int index;
    Texture _gizmoTexture;
    Texture _tickTexture;
    Texture _minusTexture;
    int selectedGizmo;
    bool _buttonPressed;

    //List<bool> _gizmosEnabled = new List<bool>();
    private void Awake()
    {
        tgt = (LookAtScript)target;
        if (travelPath == null) travelPath = tgt.GetComponent<TravelPath>();
        _gizmoTexture = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Textures/gizmo.png", typeof(Texture));
        _tickTexture = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Textures/tick.png", typeof(Texture));
        _minusTexture = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Textures/minus.png", typeof(Texture));
    }
    private void OnSceneGUI()
    {
        for (int i = 0; i < tgt.boolList.Count; i++)
        {
            if (tgt.boolList[i])
            {
                if (selectedGizmo == i && _buttonPressed)
                {
                    if (tgt.PointsToLookAt.Count <= 0) return;

                    Handles.DrawDottedLine(travelPath.points[i], tgt.PointsToLookAt[travelPath.points[i]], 3);
                    tgt.PointsToLookAt[travelPath.points[i]] = Handles.PositionHandle(tgt.PointsToLookAt[travelPath.points[i]], Quaternion.identity);
                    
                    

                }

            }
           

        }
    }
    public override void OnInspectorGUI()
    {

        if (tgt.PointsToLookAt.Count <= 0) Reload();
        GUILayout.BeginHorizontal();
        GUI.Label(new Rect(10, 20, 100, 20), "Rotations");
        if (tgt.boolList.Count <= 0) Reload();
        if (GUI.Button(new Rect(90 + 20, 20, 20, 20), "R"))
        {

            Reload();

        }

        GUI.Label(new Rect(70 + 20, 20, 20, 20), travelPath.points.Count.ToString());
        
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(150, 0, 1000, 50), "(points that camera will be \n targeting at specific moment \n in the waypoints path)");
        GUI.contentColor = Color.white;

        GUILayout.EndHorizontal();
        if (travelPath != null)
        {

            if (travelPath.points.Count > 0)

                for (int i = 0; i < travelPath.points.Count; i++)
                {
                    EditorGUI.DrawRect(new Rect(10, 60 + (40 * i), Screen.width * .9f, 27), travelPath.CurrentWaypoint == i ? Color.green : Color.gray);
                    GUI.color = Color.black;
                    GUI.Label(new Rect(10, 60 + (40 * i), 100, 20), "Rotation " + (i + 1).ToString());
                    GUI.color = Color.white;

                    GUI.backgroundColor = Color.white;
                    if(i != 0)
                    if (GUI.Button(new Rect(Screen.width * .75f, 61 + (40 * i), 25, 25), tgt.boolList[i] ? new GUIContent(_tickTexture) : new GUIContent(_minusTexture)))
                    {
                            if (tgt.boolList.Contains(true) && tgt.boolList.Count > 1)
                            {

                                tgt.boolList[i] = !tgt.boolList[i];
                                if (!tgt.boolList[i])
                                {

                                    if (selectedGizmo == i)
                                        _buttonPressed = false;
                                    
                                    tgt.PointsToLookAt.Remove(travelPath.points[i]);
                                    
                                }
                                else
                                {
                                    tgt.PointsToLookAt.Add(travelPath.points[i], travelPath.points[i]);

                                }


                                travelPath.PointsToLookAt = tgt.PointsToLookAt;
                            }
                          

                    }

                    GUI.backgroundColor = selectedGizmo == i && _buttonPressed ? (Color)new Color32(10, 161, 221, 255) : Color.white;

                    if (tgt.boolList[i])
                        if (GUI.Button(new Rect(Screen.width * .84f, 61 + (40 * i), 25, 25), new GUIContent(_gizmoTexture)))
                        {
                            if (selectedGizmo == i)
                            {
                                _buttonPressed = !_buttonPressed;

                            }
                            else
                            {
                                if (!_buttonPressed) _buttonPressed = true;
                            }
                            selectedGizmo = i;

                        }
                    GUILayout.Space(40);
                    
                }
            GUILayout.Space(80);
        }
    }
    void Reload()
    {
        tgt.boolList.Clear();
        tgt.PointsToLookAt.Clear();

        foreach (var item in travelPath.points)
        {
            tgt.boolList.Add(true);
            tgt.PointsToLookAt.Add(item, item);
        }
        travelPath.PointsToLookAt = tgt.PointsToLookAt;
    }
   
}

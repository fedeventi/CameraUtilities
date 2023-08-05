
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(PathEditor))]
public class MakePathEditor : Editor
{
    List<Vector3> _points = new List<Vector3>();
    bool _uniformScale ;
    string _name;




    private void OnSceneGUI()
    {


        var tgt = (PathEditor)target;

        if(tgt.path != null) 
            MakeBezier(tgt);


    }
    void MakeBezier(PathEditor tgt)
    {

        if (tgt.path.beziers.Count > 0)
        {
            int aux = 0;
            _points.Clear();
            EditorGUI.BeginChangeCheck();

            for (int i = 0; i < tgt.path.beziers.Count; i++)
            {
                if (tgt.path.MyType==Type.Custom)
                {
                    tgt.path.beziers[i].offset = Handles.PositionHandle(tgt.path.beziers[i].offset + tgt.transform.position, Quaternion.identity) - tgt.transform.position;
                    tgt.path.beziers[i].Tangentoffset = Handles.PositionHandle(tgt.path.beziers[i].Tangentoffset + tgt.path.beziers[i].position, Quaternion.identity) - tgt.path.beziers[i].position;
                    Handles.DrawDottedLine(tgt.path.beziers[i].position, tgt.path.beziers[i].tangentPosition, 10);
                }



                if (i != tgt.path.beziers.Count - 1 && i != 0)
                {
                    if (tgt.path.MyType==Type.Custom)
                    {
                        tgt.path.beziers[i].AltTangentoffset = Handles.PositionHandle(tgt.path.beziers[i].AltTangentoffset + tgt.path.beziers[i].position, Quaternion.identity) - tgt.path.beziers[i].position;
                        Handles.DrawDottedLine(tgt.path.beziers[i].position, tgt.path.beziers[i].AlttangentPosition, 10);
                    }


                    Handles.DrawBezier(tgt.path.beziers[i].position, tgt.path.beziers[i + 1].position,
                            tgt.path.beziers[i].AlttangentPosition, tgt.path.beziers[i + 1].tangentPosition,
                            Color.red, EditorGUIUtility.whiteTexture, 2);

                    var index = 0;

                    foreach (var pos in Handles.MakeBezierPoints(tgt.path.beziers[i].position, tgt.path.beziers[i + 1].position,
                     tgt.path.beziers[i].AlttangentPosition, tgt.path.beziers[i + 1].tangentPosition, tgt.path.smoothness))
                    {
                        aux++;
                        Handles.SphereHandleCap(aux, pos, Quaternion.identity, 0.2f, EventType.Repaint);
                        if (index != 0)
                            _points.Add(pos);
                        index++;
                    }


                }
                else if (i == 0)
                {

                    Handles.DrawBezier(tgt.path.beziers[i].position, tgt.path.beziers[i + 1].position,
                          tgt.path.beziers[i].tangentPosition, tgt.path.beziers[i + 1].tangentPosition,
                          Color.red, EditorGUIUtility.whiteTexture, 2);


                    foreach (var pos in Handles.MakeBezierPoints(tgt.path.beziers[i].position, tgt.path.beziers[i + 1].position,
                        tgt.path.beziers[i].tangentPosition, tgt.path.beziers[i + 1].tangentPosition, tgt.path.smoothness))
                    {
                        aux++;

                        Handles.SphereHandleCap(aux, pos, Quaternion.identity, 0.2f, EventType.Repaint);
                        _points.Add(pos);
                    }

                }
            }

            tgt.RecalculatePosition();





            if (EditorGUI.EndChangeCheck())
            {

                
                Reload(tgt);
            }
        }
    }

    void Reload(PathEditor tgt)
    {
        tgt.path.points.Clear();
        foreach (var item in _points)
        {
            tgt.path.points.Add(item);
        }
    }
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        var tgt = (PathEditor)target;
        
        tgt.path = (MyPath)EditorGUILayout.ObjectField("Path",tgt.path, typeof(MyPath), true);
        if (tgt.path == null) return;
        


        
        tgt.path.smoothness = EditorGUILayout.IntSlider("Smoothness", tgt.path.smoothness, 5, 100);
        tgt.path.units = EditorGUILayout.IntSlider("Units", tgt.path.units, 1, 100);
        
        if (tgt.path.MyType != Type.Custom)
        {
            _uniformScale = EditorGUILayout.Toggle("Uniform Scale", _uniformScale);
            
            if (_uniformScale)
            {
                var size = EditorGUILayout.Slider("Size", tgt.path.sizeX, 0, 100);
                tgt.path.SetSize(size);
            
            }
            else
            {
                tgt.path.sizeX = EditorGUILayout.Slider("Size X", tgt.path.sizeX, 0, 50);
                tgt.path.sizeY = EditorGUILayout.Slider("Size Y", tgt.path.sizeY, 0, 50);
                tgt.path.sizeZ = EditorGUILayout.Slider("Size Z", tgt.path.sizeZ, 0, 10);

            }
        }
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
        {
            
            tgt.path.DrawPath(tgt.transform);
        }
    
        if (tgt.path.MyType!=Type.Circle)
            if (GUILayout.Button("Remove"))
            {
                
                if (tgt.path.beziers.Count <= 2)
                {

                    tgt.path.beziers.Clear();
                    return;
                }
                tgt.path.beziers.RemoveAt(tgt.path.beziers.Count - 1);
                _points.RemoveRange(_points.Count - 1 - tgt.path.smoothness, tgt.path.smoothness);
            }
        if (GUILayout.Button("Reload"))
        {
            Reload(tgt);
        }
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Delete"))
        {

            tgt.path.beziers.Clear();
            tgt.path.points.Clear();
        }
       
        if (EditorGUI.EndChangeCheck())
        {
            Repaint();
        }
    }
}
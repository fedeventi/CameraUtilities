using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayMovement))]
public class CreatePrefabTraveler : Editor
{
    PlayMovement _tgt;
    private void OnEnable()
    {

        _tgt = (PlayMovement)target;

        
        


    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (_tgt.Record)
        {

            if (!_tgt.MultipleCameras)
            {
                for (int i = _tgt.allCameras.Count-1; i > 0; i--)
                {
                    _tgt.allCameras.RemoveAt(i);
                }
            }
            else
            {
                if (_tgt.allCameras.Count <= 0) return;
                if (_tgt.allCameras.Count != _tgt.Record.travellers.Count)
                {
                    EditorGUILayout.HelpBox("Necesitas asignar tantas camaras como Travellers hayan ("+_tgt.Record.travellers.Count+")", MessageType.Error);
                }
            }
        }
        if (_tgt.Record == null)
        {
            EditorGUILayout.HelpBox("Necesitas asignar un Record", MessageType.Error);
        }
            
        if (GUILayout.Button("CreateTraveller"))
        {
            var traveller =AssetDatabase.LoadAssetAtPath<TravelPath>("Assets/RecordPrefabs/TravelerDefault.prefab");
            Instantiate(traveller);
        }
        
    }
}

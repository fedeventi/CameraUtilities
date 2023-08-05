using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class RecordMovementWindow :EditorWindow
{

    
    List<TravelPath> _list=new List<TravelPath>();
    TravelPath travelPath;
    int _edit=-1;
    Texture playTex, stopTex;
    bool playing;
    int index;
    string _name="";
    
    private void Awake()
    {
        

    }
    [MenuItem("Window/Camera Utilities/Record Movement")]
    public static void ShowWindow()
    {
        
        GetWindow<RecordMovementWindow>("Record Movement");

    }
    private void OnGUI()
    {
        minSize = new Vector2(400, 155+20*_list.Count);
        if (!playTex || !stopTex)
        {
            playTex = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Textures/play.png", typeof(Texture));
            stopTex = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Textures/stop.png", typeof(Texture));

        }
        EditorGUI.BeginChangeCheck();
        GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
        GUI.Label(new Rect(0, 0, 80, 50), "Controls:");
        //GUI.contentColor = Color.black;

        if (GUI.Button(new Rect(Screen.width * .2f, 15, 20, 20), playing?new GUIContent("||"):new GUIContent(playTex)))
        {
            playing = !playing;
            Repaint();
            if(!playing)
                Pause();
        }
        
        EditorGUILayout.Space(50);
        GUI.contentColor = Color.white;
        if (GUI.Button(new Rect(Screen.width * .2f + 20, 15, 20, 20), new GUIContent(stopTex)))
        {
           Stop();
        }
        GUI.EndGroup();
        GUI.BeginGroup(new Rect(0, 10, Screen.width, Screen.height));
        for (int i = 0; i < _list.Count; i++)
        {
            GUI.color = _edit == i ? Color.gray : Color.white;
            if (GUI.Button(new Rect(130, 50+i * 20, 20, 20), "o"))
            {
                if (_edit == i) _edit = -1;
                else
                _edit = i;
                
            }
            GUI.color = Color.white;
            GUI.enabled = _edit==i?true:false;
            //_list[i].Name = GUI.TextField(new Rect(1,50*i,50,20),_list[i].Name,10);
            _list[i] = (TravelPath)EditorGUILayout.ObjectField($"Element {i}", _list[i], typeof(TravelPath), true);
            GUI.enabled = true;
            
            
        }
        
        if (_list.Count > 0)
            if (GUI.Button(new Rect(130, 50+_list.Count * 20, 20, 20), "-"))
            {
            
                _list.RemoveAt(_list.Count-1);
            
            
            }
        travelPath = (TravelPath)EditorGUILayout.ObjectField("new element", travelPath, typeof(TravelPath), true);
        _name = GUILayout.TextField(_name);
        if (_name == "")
        {
            EditorGUILayout.HelpBox("Introduce un nombre para el ScriptableObject", MessageType.Warning);
            GUI.enabled = false;
        }
        if (GUILayout.Button("Create"))
        {
            Create();

        }
        GUI.enabled = true;
        GUI.EndGroup();
        if (EditorGUI.EndChangeCheck())
        {
            base.SaveChanges();
            
            
        }

        if (travelPath != null)
        {
            if (!_list.Contains(travelPath))
            {
                _list.Add(travelPath);
            }
            travelPath= new TravelPath();
            
        }
        if (playing)
            Play();
      
           
    }
    void Play()
    {
        
        if (_list.Count <= 0) return;
        if (_list.Count > 1)
        {
            _list[index].play = true;
            Repaint();
            if (_list[index].finished)
            {
                
                _list[index].play = false;
                if(index+1<_list.Count)
                    index++;
                else
                    Stop();
            }
        }
        else
            _list[0].play = true;



    }
    void Pause()
    {
        
        index = 0;
        foreach (var item in _list)
        {
            item.play = false;
        }
    }
    void Stop()
    {
        playing = false;
        index = 0;
        foreach (var item in _list)
        {
            item.SetList();
            item.play = false;
            item.ResetPosition();
        }
    }
    private void Create()
    {
        Record SO = CreateInstance<Record>();
        int index=0;
        if (!AssetDatabase.IsValidFolder("Assets/Scripts/Travellers"))
        {
            AssetDatabase.CreateFolder("Assets/Scripts", "Travellers");
            
        }
        List<TravelPath> newList= new List<TravelPath>();
        foreach (var item in _list)
        {
            //var obj =PrefabUtility.SaveAsPrefabAsset(item.gameObject,$"Assets/RecordPrefabs/{item.gameObject.name}.prefab");
            //var obj = AssetDatabase.LoadAssetAtPath<TravelPath>("Assets/RecordPrefabs/TravelerDefault.prefab");
            //newList.Add(obj.Initialize(item).GetComponent<TravelPath>());
            index++;
            Traveller traveller = CreateInstance<Traveller>();
            traveller.Initialize(item);
            AssetDatabase.CreateAsset(traveller, $"Assets/Scripts/Travellers/{_name+index}.asset");
            SO.travelPaths.Add(AssetDatabase.LoadAssetAtPath<TravelPath>("Assets/RecordPrefabs/TravelerDefault.prefab"));
            SO.travellers.Add(traveller);
            DestroyImmediate(item.gameObject);
        }
        

        AssetDatabase.CreateAsset(SO, $"Assets/Scripts/Records/{_name}.asset");
        _list.Clear();
    }
}

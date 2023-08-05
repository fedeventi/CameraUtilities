using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Record : ScriptableObject
{

    public List<TravelPath> travelPaths = new List<TravelPath>();
    public List<Traveller> travellers = new List<Traveller>();
    int index;
    bool _playing;
    bool finished=false;
    public int Index => index;
    public void Play()
    {
        if (finished) return;
        _playing = true;
        if (travelPaths.Count <= 0) return;
        if (travelPaths.Count > 1)
        {
            travelPaths[index].play = true;
            
            if (travelPaths[index].finished)
            {

                travelPaths[index].play = false;
                if (index + 1 < travelPaths.Count)
                    index++;
                else
                {
                    finished = true;
                    Pause();
                }
            }
            
        }
        else
            travelPaths[0].play = true;
    }
    public void Pause()
    {

        
        foreach (var item in travelPaths)
        {
            item.play = false;
        }
    }
    public void Stop()
    {
        _playing = false;
        index = 0;
        foreach (var item in travelPaths)
        {
            item.SetList();
            item.play = false;
            item.ResetPosition();
            
        }
    }
    public void SetList()
    {
        foreach (var item in travelPaths)
        {
            item.SetList();
        }
    }
    public void Create()
    {
        if (travelPaths == null) return;
        finished = false;
        for (int i = 0; i < travelPaths.Count; i++)
        {
            TravelPath obj = AssetDatabase.LoadAssetAtPath<TravelPath>("Assets/RecordPrefabs/TravelerDefault.prefab");
            travelPaths[i]=Instantiate(obj).Initialize(travellers[i]);

        }
    }
}

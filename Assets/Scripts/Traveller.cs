using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traveller : ScriptableObject
{
    public List<Vector3> list=new List<Vector3>();
    public MyPath  path;
    //public Dictionary<Vector3,Vector3> lookingPoints = new Dictionary<Vector3,Vector3>();
    public List<Vector3> lookingPoints = new List<Vector3>();
    public List<Vector3> indexers = new List<Vector3>();
    public float         speed;
    public float         speedRotation;
    public bool          followPath;
   
    public void Initialize(TravelPath travelPath)
    {
        list=travelPath.points;
        path = travelPath.path;
        foreach (var item in travelPath.PointsToLookAt)
        {
            lookingPoints.Add(item.Value);
            indexers.Add(item.Key);
        }
        speed = travelPath.Speed;
        speedRotation = travelPath.speedRotation;
        followPath = travelPath.FollowPath;

    }
}
public class dictionaryElement<T,K> 
{
    public K _value;
    public T _index;
    public dictionaryElement (T index, K value)
    {
        _value = value;
        _index = index;
        
    }

   
}
public class CustomDic<T,K>:IEnumerable<dictionaryElement<T,K>>
{
    List<dictionaryElement<T,K>> list = new List<dictionaryElement<T,K>>();

    public void Add(T index, K element)
    {
        list.Add(new dictionaryElement<T,K>(index, element));
    }
    public void RemoveAt(T index)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i]._index.Equals(index))
            {
                list.Remove(list[i]);
            }
        }
    }
    public void Remove(K element)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i]._value.Equals(element))
            {
                list.Remove(list[i]);
            }
        }
    }
    public int Count => list.Count;
    
    public void Clear()
    {
        list.Clear();
    }
    
    public bool ContainsKey(K element)
    {
        foreach (var item in list)
        {
            if (item._value.Equals(element)) return true;
        }
        return false;
    }
    public K this[T index]
    {
        get
        {
            foreach (var item in list)
            {
                if (item._index.Equals(index)) return item._value;
            }
            return default(K);
        }
        set {

            foreach (var item in list)
            {
                if (item._index.Equals(index)) item._value=value;
            }
        }
    }
    public IEnumerator<dictionaryElement<T,K>> GetEnumerator()
    {
        for (int i = 0; i < list.Count; i++)
        {
            yield return list[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
}

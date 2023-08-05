using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PathEditor : MonoBehaviour
{
    
    public MyPath path;
    public List<Vector3> points { get =>path.points; }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update()
    {
        

    }
    public void RecalculatePosition()
    {
        path.Recalculate(transform);
    }
    private void OnDrawGizmos()
    {
        if(path==null)return;
                
        if (!Selection.Contains(gameObject))
        {
            
            for (int i = 0; i < points.Count-1; i++)
            {
                Debug.DrawRay(points[i],points[i+1]-points[i] );
            }
        }
    }
}

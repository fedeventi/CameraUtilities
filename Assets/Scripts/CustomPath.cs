using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomPath",menuName = "ScriptableObjects/Paths/CustomPath", order = 1)]
public class CustomPath : MyPath
{
    public CustomPath()
    {
        MyType = Type.Custom;
        
    }
    public override void DrawPath(Transform transform)
    {
        
        if (beziers.Count > 0)
            beziers.Add(new Bezier(beziers[beziers.Count - 1].offset + transform.forward * units, Vector3.zero + transform.right * units));

        else
        {
            beziers.Add(new Bezier(Vector3.zero + transform.forward * units, Vector3.zero + transform.right * units));
            beziers.Add(new Bezier(beziers[beziers.Count - 1].offset + transform.forward * units, Vector3.zero + transform.right * units));

        }
    }

    public override void Recalculate(Transform transform)
    {
      
        foreach (var item in beziers)
        {
            item.position = (transform.position + item.offset) ;
            item.tangentPosition = (item.position + item.Tangentoffset) ;
            item.AlttangentPosition = (item.position + item.AltTangentoffset);
           
        }
    }

    public override void SetSize(float _size)
    {
      
    }
}

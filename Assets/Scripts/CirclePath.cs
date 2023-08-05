using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CirclePath",menuName = "ScriptableObjects/Paths/CirclePath", order = 2)]
public class CirclePath : MyPath
{
    public CirclePath()
    {
        MyType = Type.Circle;
    }
    public override void DrawPath(Transform transform)
    {
        if (beziers.Count > 0)
            beziers.Clear();

        
            beziers.Add(new Bezier(Vector3.zero , Vector3.zero + transform.right));
            beziers.Add(new Bezier(beziers[beziers.Count - 1].offset , Vector3.zero + transform.right ));
            beziers.Add(new Bezier(Vector3.zero , Vector3.zero + transform.right ));
            beziers[1].AltTangentoffset = transform.right*-1;
            beziers[2].Tangentoffset = transform.right  * -1;

        
    }

    public override void Recalculate(Transform transform)
    {
        var order = 0;
        foreach (var item in beziers)
        {
            item.position = (transform.position + item.offset) + transform.forward * (order == 1 ? sizeX : 0);
            item.tangentPosition = (item.position + item.Tangentoffset) + transform.right * (order==2?-sizeY+1:sizeY-1);
            item.AlttangentPosition = (item.position + item.AltTangentoffset) + transform.right * (order == 1 ? -sizeY+1: sizeY-1);
            order++;
        }
    }

    public override void SetSize(float _size)
    {
        sizeX = _size;
        sizeY = _size - _size * 0.3f;
        sizeZ = _size;
    }
}

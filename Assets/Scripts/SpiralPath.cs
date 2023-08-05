using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpitalPath", menuName = "ScriptableObjects/Paths/SpiralPath", order = 3)]
public class SpiralPath : MyPath
{
    int amount=0;
    public SpiralPath()
    {
        MyType = Type.Spiral;
        sizeX = 2;
        sizeY = 2;
        sizeZ = .5f;
       
    }
    public override void DrawPath(Transform transform)
    {
        if (beziers.Count <= 0)
        {
            beziers.Add(new Bezier(Vector3.zero, Vector3.zero + transform.right));
            beziers.Add(new Bezier(Vector3.zero, Vector3.zero + transform.right));
            beziers.Add(new Bezier(Vector3.zero, Vector3.zero + transform.right));
            beziers[0].Tangentoffset = transform.right * -sizeY;
            beziers[1].AltTangentoffset = transform.right * -1;
            beziers[2].Tangentoffset = transform.right * -1;
            amount =2;
        }
        else
        {

            Debug.Log(amount);
            amount++;
            beziers.Add(new Bezier(beziers[beziers.Count - 1].offset, Vector3.zero + transform.right));
            if (amount % 2 == 0)
            {
                beziers[amount-1].AltTangentoffset = transform.right * -1;
                beziers[amount].Tangentoffset = transform.right * -1;
            }
        }
            




    }

    public override void Recalculate(Transform transform)
    {
        var order = 0;
        beziers[0].Tangentoffset = transform.right * -sizeY;
        foreach (var item in beziers)
        {

            item.position = (transform.position + item.offset) + transform.forward * (order % 2 == 0 ? sizeX : 0)+transform.up*-sizeZ*(order%2!=0&&order>0?order:order-1)+transform.right*(sizeY);
            item.tangentPosition = (item.position + item.Tangentoffset) + transform.right * (order % 2 != 0 ? 0 -sizeY: -0.5f+sizeY);
            item.AlttangentPosition = (item.position + item.AltTangentoffset) + transform.right * (order % 2 == 0 ? 0 -sizeY : .5f+sizeY);
            order++;
        }
    }

    public override void SetSize(float _size)
    {
        sizeX = _size*1;
        sizeY = _size+_size* 0.5f;
        sizeZ = _size - _size * 0.6f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier 
{
    
    public Vector3 position;
    public Vector3 offset;
    public Quaternion rotation;
    public Vector3 Tangentoffset;
    public Vector3 AltTangentoffset;
    public Vector3 tangentPosition;
    public Vector3 AlttangentPosition;
    public Bezier(Vector3 position, Vector3 tangentPosition)
    {
        this.position = offset + position;
        Tangentoffset = tangentPosition;
        offset = position;
        this.tangentPosition = Tangentoffset + this.position;
        AltTangentoffset = Tangentoffset;
        AlttangentPosition = tangentPosition;
    }
    
}

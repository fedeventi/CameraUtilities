using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class MyPath : ScriptableObject
{
    public int smoothness;
    Type _type;
    public int units;
    float _sizeX = 1;
    float _sizeY = 1;
    float _sizeZ = 0;
    public List<Bezier> beziers = new List<Bezier>();
    public List<Vector3> points = new List<Vector3>();

    public abstract void SetSize(float _size);
    public float sizeX { get { return _sizeX; } set { _sizeX = value; } }
    public float sizeY { get { return _sizeY; } set { _sizeY = value; } }
    public float sizeZ { get { return _sizeZ; } set { _sizeZ = value; } }
    public Type MyType { get { return _type; } set { _type = value; } } 
    public abstract void DrawPath(Transform transform);

    public abstract void Recalculate(Transform transform);
    

    
    
}
public enum Type
{
    Custom,
    Circle,
    Spiral
}

